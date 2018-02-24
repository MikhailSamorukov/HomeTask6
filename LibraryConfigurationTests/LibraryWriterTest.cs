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
            FileStream fs = new FileStream(_path, FileMode.Create, FileAccess.Write);
            _libraryWriter = new LibraryWriter(fs);
        }

        [Test]
        [Category("Integrations")]
        public void Write_should_right_write_all_elements_from_stream()
        {
            //Arange 
            //Act
            _libraryWriter.Write(GetMockedCorectLibraryElements(),"MyLibrary");
            _libraryWriter.Close();

            var resultString = File.ReadAllText(_path);
            var mockedString = GetMockedXmlString();
            //Assert
            Assert.AreEqual(resultString, mockedString);
        }

        [Test]
        [Category("Integrations")]
        public void Write_should_write_empty_file_without_elements()
        {
            //Arange
            var path = $"{Directory.GetCurrentDirectory()}\\BrokenLibrary.xml";
            var streamWrite = new FileStream(path, FileMode.Create, FileAccess.Write);
            //Act
            var libraryWriter = new LibraryWriter(streamWrite);
            libraryWriter.Write(GetMockedUnCorectLibraryElements(), "BrokenLibrary");
            libraryWriter.Close();
            var streamRead = new FileStream(path, FileMode.Open, FileAccess.Read);
            var libraryReader = new LibraryReader(streamRead);
            libraryReader.Close();
            //Assert
            Assert.That(!libraryReader.Any());
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
          return $@"<?xml version=""1.0"" encoding=""utf-8""?>
<catalog UnLoadTime=""{DateTime.Now:dd-MM-yyyy}"" Description=""MyLibrary"">
	<Book>
		<Autor>J. R. R. Tolkien</Autor>
		<PublishingHouse>London</PublishingHouse>
		<PublishingName>Allen and Unwin</PublishingName>
		<PublishingYear>1954</PublishingYear>
		<InternationNumber>0-395-08254-4</InternationNumber>
		<Name>Lord of the rings</Name>
		<PagesCount>1589</PagesCount>
		<Note>Cult book</Note>
	</Book>
	<Newspaper>
		<PublishingHouse>New York City</PublishingHouse>
		<PublishingName>Dow Jones and Company</PublishingName>
		<PublishingYear>2016</PublishingYear>
		<Number>45</Number>
		<Date>2016-12-12T00:00:00</Date>
		<InternationNumber>0099-9660</InternationNumber>
		<Name>The Wall Street Journal</Name>
		<PagesCount>100</PagesCount>
		<Note>The Wall Street Journal is an American business-focused, English-language international daily newspaper based in New York City.</Note>
	</Newspaper>
</catalog>";
        }
    }
}
