using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;
using HoloRepository;
using TMPro;
using UnityEngine.Networking;

public class PatientList : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplates = null;

    public static List<Patient> patientList = new List<Patient>();

    public void ReadJsonFile()
    {
        StreamReader reader = new StreamReader("./Assets/Sample/samplePatientsWithHolograms.json");
        string json = reader.ReadToEnd();

        patientList.Clear();

        JsonData jsonData = JsonMapper.ToObject(json);
        for (int i = 0; i < jsonData.Count; i++)
        {
            Patient patient = JsonMapper.ToObject<Patient>(jsonData[i].ToJson());
            patientList.Add(patient);
        }
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
        ReadJsonFile();  
    }
}
