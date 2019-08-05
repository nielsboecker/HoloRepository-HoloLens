using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloStorageConnector;

public class PatientList : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplates = null;
    public static List<Patient> patientList = new List<Patient>();
    public static bool InitialFlag = true;

    void Start()
    {
        if (InitialFlag)
        {
            InitialFlag = false;
            StartCoroutine(getAllPateints());
        }
        else
        {
            GenerateListView(patientList);
        }      
    }

    IEnumerator getAllPateints()
    {
        yield return HoloStorageClient.GetMultiplePatients(patientList, "p-100,p-101,p-102");
        GenerateListView(patientList);
    }

    private void GenerateListView(List<Patient> patientList)
    {
        foreach (Patient patient in patientList)
        {
            GameObject button = Instantiate(buttonTemplates) as GameObject;
            button.SetActive(true);

            button.GetComponent<PatientListItem>().SetID(patient.pid);
            button.GetComponent<PatientListItem>().SetName($"{patient.name.given} {patient.name.family}");
            string Info = string.Format("Gender: {0}\nDate of birth: {1}", patient.gender, patient.birthDate.Substring(0, 10));
            button.GetComponent<PatientListItem>().SetText(Info);

            button.transform.SetParent(buttonTemplates.transform.parent, false);
        }
    }
}
