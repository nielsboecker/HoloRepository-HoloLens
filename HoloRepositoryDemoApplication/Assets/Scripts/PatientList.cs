using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;
using HoloStorageConnector;
using TMPro;
using UnityEngine.Networking;

public class PatientList : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplates = null;

    public static List<Patient> patientList = new List<Patient>();

    IEnumerator getAllPateints()
    {
        List<Patient> patientList = new List<Patient>();
        yield return HoloStorageClient.GetMultiplePatients(patientList, "IDs");
        foreach (Patient patient in patientList)
        {
            GameObject button = Instantiate(buttonTemplates) as GameObject;
            button.SetActive(true);

            button.GetComponent<PatientListItem>().SetID(patient.pid);
            button.GetComponent<PatientListItem>().SetName(patient.name.full);
            string Info = string.Format("Gender: {0}\nDate of birth: {1}", patient.gender, patient.birthDate.Substring(0, 10));
            button.GetComponent<PatientListItem>().SetText(Info);

            button.transform.SetParent(buttonTemplates.transform.parent, false);
        }
    }

    void Start()
    {
        StartCoroutine(getAllPateints());
    }
}
