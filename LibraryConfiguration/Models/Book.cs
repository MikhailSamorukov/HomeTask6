using LibraryConfiguration.Attributes;

namespace LibraryConfiguration.Models
{
    [Model]
    public class Book: LibraryElement
    {
        [Mandatory]
        public string Autor { get; set; }
        public string PublishingHouse { get; set; }
        public string PublishingName { get; set; }
        public int PublishingYear { get; set; }
        [Mandatory]
        public string InternationNumber { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Book bookObj))
                return false;

                return InternationNumber == bookObj.InternationNumber
                        && Autor == bookObj.Autor
                        && PublishingHouse == bookObj.PublishingHouse
                        && PublishingName == bookObj.PublishingName
                        && PublishingYear == bookObj.PublishingYear;
        }
    }
}
