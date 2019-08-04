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
    private Text PatientID = null;
    [SerializeField]
    private TextMeshProUGUI PatientName = null;
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
    public void SetID(string id)
    {
        PatientID.text = id;
    }

    public void TurnToHologramPage()
    {
        HologramList.InitialFlag = false;
        ScenesManager.RefreshScene("HologramListScene");
    }
}