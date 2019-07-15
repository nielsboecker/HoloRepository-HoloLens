public class PatientInfo
{
    public string id;
    public PersonName name;
    public string gender;
    public string email;
    public string phone;
    public string dateOfBirth;
    public string pictureUrl;
    public PersonAddress address;
    public string[] imagingStudySeries;
    public HoloGrams[] holograms;
}

public class HoloGrams
{
    public string gender;
    public string email;
    public string phone;
    public Person subject;
    public Person author;
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

public class PersonAddress
{
    public string street;
    public string city;
    public string state;
    public int postcode;
}

public class Person
{
    public string id;
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