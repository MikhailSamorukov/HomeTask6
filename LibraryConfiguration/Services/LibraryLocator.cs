using System.Collections.Generic;
using System.Linq;
using LibraryConfiguration.Interfaces;
using LibraryConfiguration.Models;

namespace LibraryConfiguration.Services
{
    public class LibraryLocator
    {
        private readonly ILibraryReader _libraryReader;
        public LibraryLocator(ILibraryReader libraryReader)
        {
            _libraryReader = libraryReader;
        }

        public IEnumerable<Book> Books => _libraryReader.Where(element => element.GetType() == typeof(Book)).Select(element => (Book) element);
        public IEnumerable<Newspaper> Newspapers => _libraryReader.Where(element => element.GetType() == typeof(Newspaper)).Select(element => (Newspaper)element);
        public IEnumerable<Patent> Patents => _libraryReader.Where(element => element.GetType() == typeof(Patent)).Select(element => (Patent)element);
    }
}