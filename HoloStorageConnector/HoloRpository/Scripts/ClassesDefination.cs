namespace HoloRepository
{
    public class Patient
    {
        public string pid { get; set; }
        public PersonName name { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string birthDate { get; set; }
        public string pictureUrl { get; set; }
        public Address address { get; set; }
        public string[] imagingStudySeries { get; set; }
        public Hologram[] holograms { get; set; }
    }

    public class Hologram
    {
        public string hid { get; set; }
        public string title { get; set; }
        public Subject subject { get; set; }
        public Author author { get; set; }
        public string createdDate { get; set; }
        public int fileSizeInkb { get; set; }
        public string imagingStudySeriesId { get; set; }
    }

    public class PersonName
    {
        public string title { get; set; }
        public string full { get; set; }
        public string first { get; set; }
        public string last { get; set; }
    }

    public class Address
    {
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int postcode { get; set; }
    }

    public class Subject
    {
        public string pid { get; set; }
        public PersonName name { get; set; }
    }

    public class Author
    {
        public string aid { get; set; }
        public PersonName name { get; set; }
    }
}