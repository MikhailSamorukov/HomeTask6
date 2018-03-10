using System;
using System.Xml;
using System.Xml.Schema;

namespace LibraryConfiguration
{
    public class LibraryValidator
    {
        public XmlReaderSettings Settings { get; }

        public LibraryValidator()
        {
            var settings = new XmlReaderSettings();
            SetSettings(settings);
            settings.ValidationEventHandler += ValidationCallBack;
            Settings = settings;
        }

        private static void SetSettings(XmlReaderSettings settings)
        {
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
        }

        private void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity != XmlSeverityType.Warning)
                Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
            else
                Console.WriteLine("\tValidation error: " + args.Message);
        }
    }
}