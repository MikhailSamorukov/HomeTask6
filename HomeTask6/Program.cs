using System;
using System.Collections.Generic;
using System.IO;
using LibraryConfiguration;
using LibraryConfiguration.Models;
using LibraryConfiguration.Services;

namespace HomeTask6
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Library reader
            //LibraryReader lreader = new LibraryReader(File.OpenRead($"{Directory.GetCurrentDirectory()}\\library.xml"));
            //LibraryLocator llocator = new LibraryLocator(lreader);
            //foreach (LibraryElement el in lreader)
            //{
            //    Console.WriteLine(el.Name);
            //    Console.WriteLine(el.PagesCount);
            //    Console.WriteLine(el.Note);
            //};

            //foreach (var book in llocator.Books)
            //{
            //    Console.WriteLine(book.Name);
            //    Console.WriteLine(book.PagesCount);
            //    Console.WriteLine(book.Note);
            //    Console.WriteLine(book.InternationNumber);
            //    Console.WriteLine(book.Autor);
            //    Console.WriteLine(book.PublishingYear);
            //}

            //lreader.Close();

            #endregion

            #region Library writer
            //LibraryWriter lw = new LibraryWriter(File.OpenWrite($"{Directory.GetCurrentDirectory()}\\MyLibrary.xml"));
            //List<LibraryElement> libraryElements = new List<LibraryElement>()
            //{
            //    new Book
            //    {
            //        Name = "test",
            //        Note = "test",
            //        PagesCount = 500,
            //        Autor = "test",
            //        InternationNumber = "test",
            //        PublishingHouse = "test",
            //        PublishingName = "test",
            //        PublishingYear = 2018
            //    }
            //};
            //lw.Write(libraryElements, "TestWriter");
            //lw.Close();
            #endregion

            Console.ReadLine();
        }
    }
}
