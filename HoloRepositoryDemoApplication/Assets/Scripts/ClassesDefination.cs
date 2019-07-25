public class Patient
{
    public string pid;
    public PersonName name;
    public string gender;
    public string email;
    public string phone;
    public string birthDate;
    public string pictureUrl;
    public Address address;
    public string[] imagingStudySeries;
    public Hologram[] holograms;
}

public class Hologram
{
    public string hid;
    public string title;
    public Subject subject;
    public Author author;
    public string createdDate;
    public int fileSizeInkb;
    public string imagingStudySeriesId;
}

public class PersonName
{
    public string title;
    public string full;
    public string first;
    public string last;
}

public class Address
{
    public string street;
    public string city;
    public string state;
    public int postcode;
}

public class Subject
{
    public string pid;
    public PersonName name;
}

public class Author
{
    public string aid;
    public PersonName name;
}