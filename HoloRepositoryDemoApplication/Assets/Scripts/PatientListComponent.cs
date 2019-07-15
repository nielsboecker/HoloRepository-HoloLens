using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PatientListComponent : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI PatientInfo;
    [SerializeField]
    private Text PatientID;

    private void Start()
    {
        PatientInfo = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string patientInfo)
    {
        PatientInfo.text = patientInfo;
    }
    public void SetID(string id)
    {
        PatientID.text = id;
    }
}
