using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LibraryConfiguration;
using LibraryConfiguration.Models;
using NUnit.Framework;

namespace LibraryConfigurationTests
{
    [TestFixture]
    public class LibraryWriterTest
    {
        private LibraryWriter _libraryWriter;
        private string _path;

        [SetUp]
        public void Init()
        {
            var dir = Path.GetDirectoryName(typeof(LibraryWriterTest).Assembly.Location);
            Environment.CurrentDirectory = dir ?? throw new InvalidOperationException("Directory can't be null");
            _path = $"{Directory.GetCurrentDirectory()}\\MyLibrary.xml";
            _libraryWriter = new LibraryWriter(File.OpenWrite(_path));
        }

        [Test]
        [Category("Integrations")]
        public void Write_should_right_write_all_elements_from_stream()
        {
            //Arange 
            var sBuilder = new StringBuilder();
            //Act
            _libraryWriter.Write(GetMockedCorectLibraryElements(),"MyLibrary");
            _libraryWriter.Close();

            sBuilder.Append(File.ReadAllText(_path));

            var dynamicDataPosition = sBuilder.ToString().IndexOf("UnLoadTime", StringComparison.Ordinal);

            sBuilder.Remove(dynamicDataPosition, 32);

            var resultString = Regex.Replace(sBuilder.ToString(), @"\t|\n|\r", "").Replace(" ", "");
            var mockedString = Regex.Replace(GetMockedXmlString(), @"\t|\n|\r", "").Replace(" ", "");
            //Assert
            Assert.AreEqual(resultString, mockedString);
        }

        [Test]
        [Category("Integrations")]
        public void Write_should_throw_exception_when_check_mandatority_property()
        {
            //Act
            var exception =  Assert.Throws<NullReferenceException>(() => _libraryWriter.Write(GetMockedUnCorectLibraryElements(), "MyLibrary"));
            //Assert
            Assert.That(exception.Message, Is.EqualTo("Element can't be null"));
        }

        public IEnumerable<LibraryElement> GetMockedCorectLibraryElements()
        {
            return new List<LibraryElement>()
            {
                new Book
                {
                    Name = "Lord of the rings",
                    Note = "Cult book",
                    PagesCount = 1589,
                    Autor = "J. R. R. Tolkien",
                    InternationNumber = "0-395-08254-4",
                    PublishingHouse = "London",
                    PublishingName = "Allen and Unwin",
                    PublishingYear = 1954
                },
                new Newspaper
                {
                    Name = "The Wall Street Journal",
                    PublishingHouse = "New York City",
                    PublishingName = "Dow Jones and Company",
                    PublishingYear = 2016,
                    PagesCount = 100,
                    Note = "The Wall Street Journal is an American business-focused, English-language international daily newspaper based in New York City.",
                    Number = "45",
                    Date = new DateTime(2016,12,12),
                    InternationNumber = "0099-9660",
                }
            };
        }

        public IEnumerable<LibraryElement> GetMockedUnCorectLibraryElements()
        {
            return new List<LibraryElement>()
            {
                new Book
                {
                    Name = "Lord of the rings",
                    Note = "Cult book",
                    PagesCount = 1589,
                    InternationNumber = "0-395-08254-4",
                    PublishingHouse = "London",
                    PublishingName = "Allen and Unwin",
                    PublishingYear = 1954
                },
            };
        }

        public string GetMockedXmlString()
        {
          return @"<?xml version=""1.0"" encoding=""utf - 8""?>
                < catalog Description = ""MyLibrary"" >
                < Book >
                < Autor > J.R.R.Tolkien </ Autor >
                < PublishingHouse > London </ PublishingHouse >
                < PublishingName > Allen and Unwin</ PublishingName >
                < PublishingYear > 1954 </ PublishingYear >
                < InternationNumber > 0 - 395 - 08254 - 4 </ InternationNumber >
                < Name > Lord of the rings </ Name >
                < PagesCount > 1589 </ PagesCount >
                < Note > Cult book </ Note >
                </ Book >
                < Newspaper >
                < PublishingHouse > New York City</ PublishingHouse >
                < PublishingName > Dow Jones and Company </ PublishingName >
                < PublishingYear > 2016 </ PublishingYear >
                < Number > 45 </ Number >
                < Date > 2016 - 12 - 12T00: 00:00 </ Date >
                < InternationNumber > 0099 - 9660 </ InternationNumber >
                < Name > The Wall Street Journal </ Name >
                < PagesCount > 100 </ PagesCount >
                < Note > The Wall Street Journal is an American business - focused, English - language international daily newspaper based in New York City.</ Note >
                </ Newspaper >
                </ catalog > ";
        }
    }
}
