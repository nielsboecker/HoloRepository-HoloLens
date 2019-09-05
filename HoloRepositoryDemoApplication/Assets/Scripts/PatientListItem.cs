using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HoloStorageConnector;

public class PatientListItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI PatientInfo = null;
    private Patient patient;

    public static List<Hologram> HologramsList = new List<Hologram>();

    private void Start()
    {
        PatientInfo = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string Patientinformation)
    {
        PatientInfo.text = Patientinformation;
    }
    public void SetPatient(Patient setPatient)
    {
        patient = setPatient;
    }

    public void TurnToHologramPage()
    {
        HologramList.initialFlag = false;
        HologramList.patient = patient;
        ScenesManager.RefreshScene("HologramListScene");
    }
}