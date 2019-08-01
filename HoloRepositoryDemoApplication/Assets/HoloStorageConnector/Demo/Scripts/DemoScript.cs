﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloStorageConnector;
using TMPro;
using System;

public class DemoScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI SinglePatientInfo = null;
    [SerializeField]
    private TextMeshProUGUI buttonText = null;
    [SerializeField]
    private GameObject buttonTemplates = null;
    [SerializeField]
    private GameObject All = null;
    [SerializeField]
    private GameObject Single = null;

    public void GetAllPatients()
    {
        StartCoroutine(getAllPateints());
    }

    IEnumerator getAllPateints()
    {
        List<Patient> patientList = new List<Patient>();
        yield return HoloStorageClient.GetMultiplePatients(patientList,"IDs");
        All.SetActive(true);
        foreach (Patient patient in patientList)
        {
            GameObject button = Instantiate(buttonTemplates) as GameObject;
            button.SetActive(true);
            button.GetComponent<DemoScript>().SetText(patient.name.full);
            button.transform.SetParent(buttonTemplates.transform.parent, false);
        }
    }

    public void GetPatientByID()
    {
        StartCoroutine(getPatientByID());
    }

    IEnumerator getPatientByID()
    {
        Patient patient = new Patient();
        yield return HoloStorageClient.GetPatient(patient, "666da72f-1dfa-427a-96a9-c9fb30bf7296");
        Single.SetActive(true);
        try
        {
            SinglePatientInfo.text = string.Format("Patient name: \n{0}\nGender: {1}\nDate of Birth: \n{2}", patient.name.full, patient.gender, patient.birthDate.Substring(0, 10));
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to set the text! \n[Error message]" + e.Message);
        }    
    }

    public void LoadModel()
    {
        ModelSetting.SetManipulable(true);
        ModelSetting.SetRotation(new Vector3(0, 180, 0));
        ModelSetting.SetPostition(new Vector3(0.22f, -0.2f, 0.8f));
        ModelSetting.SetSize(0.12f);
        HoloStorageClient.LoadHologram("hid");
    }

    public void SetText(string name)
    {
        buttonText.text = name;
    }
}