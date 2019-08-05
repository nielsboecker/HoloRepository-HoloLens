/// <summary>
/// This script is used to define the related HoloStorage entities, make it easier to map the information from json data. 
/// Currentlly, it is based on HoloStorage Accessor API version 0.1.0
/// With the development of the package, this script will be modified to map HoloStorage Accessor API 1.0
/// </summary>
namespace HoloStorageConnector
{
    public class Patient
    {
        public string pid { get; set; }
        public string gender { get; set; }
        public string birthDate { get; set; }
        public PersonName name { get; set; }
    }

    public class Hologram
    {
        public string hid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string contentType { get; set; }
        public int fileSizeInkb { get; set; }
        public string bodySite { get; set; }
        public string dateOfImaging { get; set; }
        public string creationDate { get; set; }
        public string creationMode { get; set; }
        public string creationDescription { get; set; }
        public string aid { get; set; }
        public string pid { get; set; }
    }

    public class PersonName
    {
        public string title { get; set; }
        public string full { get; set; }
        public string given { get; set; }
        public string family { get; set; }
    }

    public class Author
    {
        public string aid { get; set; }
        public PersonName name { get; set; }
    }
}