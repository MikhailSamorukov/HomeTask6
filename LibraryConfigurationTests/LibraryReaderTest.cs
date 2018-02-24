using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryConfiguration;
using LibraryConfiguration.Models;
using NUnit.Framework;

namespace LibraryConfigurationTests
{
    [TestFixture]
    public class LibraryReaderTest
    {
        private LibraryReader _libraryReader;
        private string _path;

        [SetUp]
        public void Init()
        {
            var dir = Path.GetDirectoryName(typeof(LibraryReaderTest).Assembly.Location);
            Environment.CurrentDirectory = dir ?? throw new InvalidOperationException("Directory can't be null");
            _path = $"{Directory.GetCurrentDirectory()}\\library.xml";
        }

        [Test]
        [Category("Integrations")]
        public void Read_should_be_equal_mocked_collection()
        {
            //Arrange
            _libraryReader = new LibraryReader(File.OpenRead(_path));
            var mockedCollection = GetMockedCorectLibraryElements();
            //Act
            var result = _libraryReader.ToList();
            _libraryReader.Close();
            //Assert
            CollectionAssert.AreEqual(mockedCollection, result);
        }
        [Test]
        [Category("Integrations")]
        public void Read_should_throw_exception_cannot_read_stream()
        {
            //Arrange
            //Act
            var exception = Assert.Throws<Exception>(() => new LibraryReader(File.OpenWrite(_path)));
            //Assert
            Assert.That(exception.Message, Is.EqualTo("Stream Can't read"));
        }
        [Test]
        [Category("Integrations")]
        public void Read_should_throw_exception_cannot_read_start_element()
        {
            //Arrange
            //Act
            var exception = Assert.Throws<Exception>(() => new LibraryReader(new MemoryStream()));
            //Assert
            Assert.That(exception.Message, Is.EqualTo("Can't read startElement"));
        }
        public List<LibraryElement> GetMockedCorectLibraryElements()
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
    }
}