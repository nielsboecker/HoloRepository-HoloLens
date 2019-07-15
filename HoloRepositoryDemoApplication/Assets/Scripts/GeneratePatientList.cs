using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;
using UnityEngine.Networking;

public class GeneratePatientList : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplates;

    private List<PatientInfo> patientList = new List<PatientInfo>();
    private List<employee> employeeList = new List<employee>();



    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log("Web Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":Received: " + webRequest.downloadHandler.text);
            }
            string json = webRequest.downloadHandler.text;

            JsonData jsonData = JsonMapper.ToObject(json);
            for (int i = 0; i < jsonData.Count; i++)
            {
                employee employe = JsonMapper.ToObject<employee>(jsonData[i].ToJson());
                employeeList.Add(employe);
            }
            foreach (employee patient in employeeList)
            {
                Debug.Log(patient.id);
                GameObject button = Instantiate(buttonTemplates) as GameObject;
                button.SetActive(true);

                button.GetComponent<PatientListComponent>().SetID(patient.id);
                button.GetComponent<PatientListComponent>().SetText("Patient name: " + patient.employee_name + "\nAge: " + patient.employee_age);

                button.transform.SetParent(buttonTemplates.transform.parent, false);
            }
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
        foreach (PatientInfo patient in patientList)
        {
            GameObject button = Instantiate(buttonTemplates) as GameObject;
            button.SetActive(true);

            button.GetComponent<PatientListComponent>().SetID(patient.id);
            button.GetComponent<PatientListComponent>().SetText("Patient name: " + patient.name.first + " " + patient.name.last + "\nGender: " + patient.gender + "\nDate of birth: " + patient.dateOfBirth.Substring(0, 10));

            button.transform.SetParent(buttonTemplates.transform.parent, false);
        }
    }

    void Start()
    {
        ReadJsonFile();
        //StartCoroutine(GetRequest("http://dummy.restapiexample.com/api/v1/employees"));   
    }
}
