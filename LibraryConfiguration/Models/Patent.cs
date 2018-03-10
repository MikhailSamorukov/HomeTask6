using System;
using LibraryConfiguration.Attributes;

namespace LibraryConfiguration.Models
{
    [Model]
    public class Patent : LibraryElement
    {
        [Mandatory]
        public string Inventor { get; set; }
        public string Country { get; set; }
        [Mandatory]
        public double RegisterNumber { get; set; }
        public DateTime ApplicationSubmissionDate { get; set; }
        public DateTime PublishDate { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Patent newspaperObj))
                return false;

            return RegisterNumber.Equals(newspaperObj.RegisterNumber)
                   && Inventor == newspaperObj.Inventor
                   && Country == newspaperObj.Country
                   && ApplicationSubmissionDate == newspaperObj.ApplicationSubmissionDate
                   && PublishDate == newspaperObj.ApplicationSubmissionDate;
        }
    }
}