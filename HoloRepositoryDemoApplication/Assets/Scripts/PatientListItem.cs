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
    private RawImage Avatar = null;
    [SerializeField]
    private Texture2D Boy = null;
    [SerializeField]
    private Texture2D Girl = null;
    private Patient Patient;

    public static List<Hologram> HologramsList = new List<Hologram>();

    private void Start()
    {
        PatientInfo = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string Patientinformation)
    {
        PatientInfo.text = Patientinformation;
    }
    public void SetPatient(Patient patient)
    {
        Patient = patient;
    }

    public void SetImage(string gender)
    {
        if (gender == "male")
        {
            Avatar.texture = Boy;
            return;
        }
        Avatar.texture = Girl;
    }

    public void TurnToHologramPage()
    {
        HologramList.InitialFlag = false;
        HologramList.Patient = Patient;
        ScenesManager.RefreshScene("HologramListScene");
    }
}