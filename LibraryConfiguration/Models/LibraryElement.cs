namespace LibraryConfiguration.Models
{
    public abstract class LibraryElement
    {
        public string Name { get; set; }
        public int PagesCount { get; set; }
        public string Note { get; set; }
    }
}
