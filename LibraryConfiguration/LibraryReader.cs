using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using LibraryConfiguration.Models;
using LibraryConfiguration.Attributes;


namespace LibraryConfiguration
{
    public class LibraryReader : IEnumerable<LibraryElement>
    {
        private readonly XmlReader _reader;
        private readonly List<Type> _models;
        private readonly Stream _stream;

        public LibraryReader(Stream stream)
        {
            ValidateStream(stream);
            _stream = stream;
            _reader = XmlReader.Create(stream, new LibraryValidator().Settings);
            try
            {
                _reader.ReadStartElement();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can't read startElement: {ex.Message}");
                throw new Exception("Can't read startElement");
            }
            _models = SetModels().ToList();
        }

        public void Close()
        {
            _reader.Close();
            _stream.Close();
        }

        public IEnumerator<LibraryElement> GetEnumerator()
        {
            var readerCanRead = true;
            while (readerCanRead)
            {
                try
                {
                    readerCanRead = _reader.Read();
                }
                catch (Exception e)
                {
                    readerCanRead = false;
                    Console.WriteLine($"Reading error! : {e.Message}");
                }
                if (!_models.Select(model => model.Name).Contains(_reader.Name)) continue;

                var objectType = _models.First(model => model.Name == _reader.Name);
                var xElement = XNode.ReadFrom(_reader) as XElement;
                var resultObject = (LibraryElement) Activator.CreateInstance(objectType);

                if (!SetProperiesWithCallBack(resultObject, objectType, xElement))
                    continue;

                yield return resultObject;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ValidateStream(Stream stream)
        {
            if(stream == null)
                throw new NullReferenceException("Stream Can't be null");

            if (!stream.CanRead)
                throw new Exception("Stream Can't read");
        }

        private IEnumerable<Type> SetModels()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly
                .GetTypes()
                .Where(type => type.CustomAttributes
                    .Any(attribute => attribute.AttributeType == typeof(ModelAttribute))
                );
        }

        private bool SetProperiesWithCallBack(LibraryElement resultObject, Type objectType, XContainer xElement)
        {
            foreach (var property in objectType.GetProperties())
            {
                var isMandatoy = property.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(MandatoryAttribute));
                var xElementValue = xElement.Element(property.Name)?.Value;

                if (isMandatoy && xElementValue == null)
                    throw new NullReferenceException("Element can't be null");

                try
                {
                    var convertedType = Convert.ChangeType(xElementValue, property.PropertyType);
                    property.SetValue(resultObject, convertedType);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            return true;
        }
    }
}