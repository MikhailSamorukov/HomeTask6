using System;
using System.Collections.Generic;
using System.IO;
using LibraryConfiguration;
using LibraryConfiguration.Models;

namespace HomeTask6
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Library reader
            //LibraryReader lr = new LibraryReader(File.OpenRead($"{Directory.GetCurrentDirectory()}\\library.xml"));
            //foreach (LibraryElement el in lr)
            //{
            //    Console.WriteLine(el.Name);
            //    Console.WriteLine(el.PagesCount);
            //    Console.WriteLine(el.Note);
            //};
            //lr.Close();
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
