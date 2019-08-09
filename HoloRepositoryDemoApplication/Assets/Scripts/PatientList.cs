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

    public static List<Patient> patientList = new List<Patient>();
    public static bool InitialFlag = true;

    void Start()
    {
        if (InitialFlag)
        {
            InitialFlag = false;
            StartCoroutine(getAllPatients());
        }
        else
        {
            GenerateListView(patientList);
        }      
    }

    IEnumerator getAllPatients()
    {
        yield return HoloStorageClient.GetMultiplePatients(patientList, "p-100,p-101,p-102");
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

            button.GetComponent<PatientListItem>().SetPatient(patient);
            button.GetComponent<PatientListItem>().SetImage(patient.gender);
            button.GetComponent<PatientListItem>().SetText($"<b><size=12>{patient.name.given} {patient.name.family}</b></size>\nGender: {patient.gender}\nDate of birth: {patient.birthDate.Substring(0, 10)}");

            button.transform.SetParent(buttonTemplates.transform.parent, false);
        }
    }
}
