using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryConfiguration;
using LibraryConfiguration.Models;

namespace HomeTask6
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Library reader

            //LibraryReader lreader = new LibraryReader(File.OpenRead($"{Directory.GetCurrentDirectory()}\\library.xml"));
            //foreach (LibraryElement el in lreader)
            //{
            //    Console.WriteLine(el.Name);
            //    Console.WriteLine(el.PagesCount);
            //    Console.WriteLine(el.Note);
            //}

            //var books = lreader.Where(element => element.GetType() == typeof(Book)).Select(element => (Book) element);
            //foreach (var book in books)
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

            FileStream fs = new FileStream($"{Directory.GetCurrentDirectory()}\\MyLibrary.xml", FileMode.Create, FileAccess.Write);
            LibraryWriter lw = new LibraryWriter(fs);
            List<LibraryElement> libraryElements = new List<LibraryElement>()
            {
                new Book
                {
                    Name = "test",
                    Note = "test",
                    PagesCount = 500,
                    Autor = "test",
                    InternationNumber = "test",
                    PublishingHouse = "test",
                    PublishingName = "test",
                    //PublishingYear = 2018
                }
            };
            lw.Write(libraryElements, "TestWriter");
            //lw.Close();

            #endregion

            Console.ReadLine();
        }
    }
}
