using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class GeneratePatientList : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplates;

    private List<PatientInfo> patientList = new List<PatientInfo>();

    [System.Serializable]
    public class PatientInfo
    {
        public string name;
        public string dateOfBirth;
        public string gender;

        public static PatientInfo CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<PatientInfo>(jsonString);
        }
    }

    public void ReadJsonFile()
    {
        StreamReader reader = new StreamReader("./Assets/Sample/samplePatients.json");
        string json = reader.ReadToEnd();

        JsonData jsonData = JsonMapper.ToObject(json);
        for (int i = 0; i < jsonData.Count; i++)
        {
            PatientInfo patient = JsonMapper.ToObject<PatientInfo>(jsonData[i].ToJson());
            patientList.Add(patient);
        }
    }

    void Start()
    {
        ReadJsonFile();
        foreach (PatientInfo patient in patientList)
        {
            GameObject button = Instantiate(buttonTemplates) as GameObject;
            button.SetActive(true);

            button.GetComponent<PatientListComponent>().SetText("Patient name: " + patient.name + "\nGender: " + patient.gender);

            button.transform.SetParent(buttonTemplates.transform.parent, false);
        }
    }
}
