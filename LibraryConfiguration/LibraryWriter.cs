using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using LibraryConfiguration.Attributes;
using LibraryConfiguration.Models;

namespace LibraryConfiguration
{
    public class LibraryWriter
    {
        private readonly XmlWriter _writer;
        private readonly Stream _stream;

        public LibraryWriter(Stream stream)
        {
            ValidateStream(stream);
            _stream = stream;
            _writer = XmlWriter.Create(stream, GetWriterSettings());
        }

        public void Close()
        {
            _writer.Close();
            _stream.Close();
        }

        public void Write(IEnumerable<LibraryElement> libraryElements, string libraryDescription)
        {
            using (_writer)
            {
                WriteStartElement(libraryDescription);
                foreach (var libraryElement in libraryElements)
                {

                    var type = libraryElement.GetType();
                    var properties = GetSerializedProperies(libraryElement);

                    if(properties == null) continue;

                    var element = new XElement(type.Name, properties);

                    element.WriteTo(_writer);
                }
                _writer.WriteEndElement();
            }
        }

        private XmlWriterSettings GetWriterSettings()
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = ("\t"),
            };
            return settings;
        }

        private void WriteStartElement(string libraryDescription)
        {
            _writer.WriteStartDocument();
            _writer.WriteStartElement("catalog");
            _writer.WriteAttributeString("UnLoadTime", DateTime.Now.ToString("dd-MM-yyyy"));
            _writer.WriteAttributeString("Description", libraryDescription);
        }

        private IEnumerable<XElement> GetSerializedProperies(LibraryElement libraryElement)
        {
            if (!ValidatePropertiesWithCallBack(libraryElement))
                return null;

            return libraryElement
                    .GetType()
                    .GetProperties()
                    .Select(property => new XElement(property.Name, property.GetValue(libraryElement)));
        }

        private bool ValidatePropertiesWithCallBack(LibraryElement libraryElement)
        {
            var isHasEmptyMandatoryProperty = libraryElement
                .GetType()
                .GetProperties()
                .Any(property =>
                    property.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(MandatoryAttribute))
                    && IsTypeEqualDefault(property.GetValue(libraryElement), property.PropertyType));
            try
            {
                if (isHasEmptyMandatoryProperty)
                {
                    throw new NullReferenceException("Element can't be null");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        private void ValidateStream(Stream stream)
        {
            if (stream == null)
                throw new NullReferenceException("Stream Can't be null");

            if (!stream.CanWrite)
                throw new Exception("Stream Can't write");
        }

        public bool IsTypeEqualDefault(object propertyValue, Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type).Equals(propertyValue);

            return propertyValue == null;
        }
    }
}