using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloStorageConnector;
using TMPro;

public enum MetaDataType
{
    Patient,
    Hologram,
}

public class InformationList : MonoBehaviour
{
    public MetaDataType type;

    [Header("Patient Information Setting")] 
    public string PatientsId = "";
    public bool patientName = true;
    public bool patientID = false;    
    public bool birthDate = true;
    public bool gender = true;

    [Header("Holgram Information Setting")]
    public string HologramsId = "";
    public bool title = true;
    public bool hologramID = false;
    public bool description = true;
    public bool contentType = true;
    public bool fileSizeInkb = false;
    public bool bodySite = false;
    public bool dateOfImaging = false;
    public bool creationDate = true;
    public bool creationMode = false;
    public bool creationDescription = false;

    [Header("Hierarchy Components")]
    [SerializeField]
    private GameObject buttonTemplates = null;
    [SerializeField]
    private TextMeshProUGUI listTitle = null;

    public static List<Patient> patientList = new List<Patient>();
    public static List<Hologram> hologramList = new List<Hologram>();

    void Start()
    {
        switch (type)
        {
            case MetaDataType.Patient:
                listTitle.text = "Patient List";
                StartCoroutine(getPatients());
                break;
            case MetaDataType.Hologram:
                listTitle.text = "Hologram List";
                StartCoroutine(getHolograms());
                break;
        }       
    }

    IEnumerator getPatients()
    {       
        yield return HoloStorageClient.GetMultiplePatients(patientList, PatientsId);
        foreach (Patient patient in patientList)
        {
            GameObject button = Instantiate(buttonTemplates) as GameObject;
            button.SetActive(true);

            string information =
                $"{(patientName ? $"<b>{patient.name.full}</b>\n" : "")}" +
                $"{(patientID ? $"ID: {patient.pid}\n" : "")}" +               
                $"{(birthDate ? $"Date of birth: {patient.birthDate.Substring(0, 10)}\n" : "")}" +
                $"{(gender ? $"Gender: {patient.gender}" : "")}";

            button.GetComponent<ListItem>().SetText(information);

            button.transform.SetParent(buttonTemplates.transform.parent, false);
        }
    }

    IEnumerator getHolograms()
    {
        yield return HoloStorageClient.GetMultipleHolograms(hologramList, HologramsId);
        foreach (Hologram hologram in hologramList)
        {
            GameObject button = Instantiate(buttonTemplates) as GameObject;
            button.SetActive(true);

            string information =
                $"{(title ? $"<b>{hologram.title}</b>\n" : "")}" +
                $"{(hologramID ? $"ID: {hologram.hid}\n":"")}" +
                $"{(description ? $"Description: {hologram.description}\n" : "")}" +               
                $"{(contentType ? $"Content Type: {hologram.contentType}\n" : "")}" +
                $"{(fileSizeInkb ? $"File Size: {hologram.fileSizeInKb}\n" : "")}" +
                $"{(bodySite ? $"Body Site: {hologram.bodySite}\n" : "")}" +
                $"{(dateOfImaging ? $"Date Of Imaging: {hologram.dateOfImaging.Substring(0, 10)}\n" : "")}" +
                $"{(creationDate ? $"Creation Date: {hologram.creationDate.Substring(0, 10)}\n" : "")}" +
                $"{(creationMode ? $"Creation Mode: {hologram.creationMode}\n" : "")}" +
                $"{(creationDescription ? $"Creation Description: {hologram.creationDescription}" : "")}";

            button.GetComponent<ListItem>().SetText(information);

            button.transform.SetParent(buttonTemplates.transform.parent, false);
        }
    }
}