using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloStorageConnector;
using TMPro;

public class PatientList : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplates = null;
    [SerializeField]
    private TextMeshProUGUI Message = null;
    [SerializeField]
    private TextMeshProUGUI Name = null;

    public static List<Patient> patientList = new List<Patient>();
    public static bool initialFlag = true;
    public static string patientIds = string.Empty;
    public static string practitionerName = string.Empty;

    void Start()
    {
        Name.text = $"Welcome to HoloRepository, Dr {practitionerName}";
        if (initialFlag)
        {
            initialFlag = false;
            StartCoroutine(getAllPatients());
        }
        else
        {
            GenerateListView(patientList);
        }      
    }

    IEnumerator getAllPatients()
    {
        yield return HoloStorageClient.GetMultiplePatients(patientList, patientIds);
        if (patientList.Count == 0)
        {
            Message.text = "There is no patients for you";
        }
        else
        {
            GenerateListView(patientList);
        }
    }

    private void GenerateListView(List<Patient> patientList)
    {
        foreach (Patient patient in patientList)
        {
            GameObject button = Instantiate(buttonTemplates) as GameObject;
            button.SetActive(true);

            string birthDate = patient.birthDate == "Unknown" ? "Unknown" : patient.birthDate.Substring(0, 10);

            button.GetComponent<PatientListItem>().SetPatient(patient);
            button.GetComponent<PatientListItem>().SetText($"<b><size=12>{patient.name.full}</b></size>\nGender: {patient.gender}\nDate of birth: {birthDate}");

            button.transform.SetParent(buttonTemplates.transform.parent, false);
        }
    }
}
