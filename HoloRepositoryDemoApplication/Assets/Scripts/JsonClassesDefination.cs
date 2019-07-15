public class PatientInfo
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
    public HoloGrams[] holograms;
}

public class HoloGrams
{
    public string hid;
    public string title;
    public Subject subject;
    public Author author;
    public string createDate;
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

public class employee
{
    public string id;
    public string employee_name;
    public string emloyee_salary;
    public string employee_age;
    public string profile_image;
}