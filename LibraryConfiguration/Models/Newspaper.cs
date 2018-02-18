using System;
using LibraryConfiguration.Attributes;

namespace LibraryConfiguration.Models
{
    [Model]
    public class Newspaper : LibraryElement
    {
        public string PublishingHouse { get; set; }
        public string PublishingName { get; set; }
        public int PublishingYear { get; set; }
        [Mandatory]
        public string Number { get; set; }
        public DateTime Date { get; set; }
        [Mandatory]
        public string InternationNumber { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Newspaper newspaperObj))
                return false;

            return InternationNumber.Equals(newspaperObj.InternationNumber);
        }
    }
}