using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HoloStorageConnector;

public class PatientListItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI PatientInfo = null;
    [SerializeField]
    private TextMeshProUGUI PatientName = null;
    private Patient Patient;

    public static List<Hologram> HologramsList = new List<Hologram>();

    private void Start()
    {
        PatientInfo = GetComponent<TextMeshProUGUI>();
    }

    public void SetName(string name)
    {
        PatientName.text = name;
    }

    public void SetText(string Patientinformation)
    {
        PatientInfo.text = Patientinformation;
    }
    public void SetPatient(Patient patient)
    {
        Patient = patient;
    }

    public void TurnToHologramPage()
    {
        HologramList.InitialFlag = false;
        HologramList.Patient = Patient;
        ScenesManager.RefreshScene("HologramListScene");
    }
}