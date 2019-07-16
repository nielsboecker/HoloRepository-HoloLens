using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientListComponent : MonoBehaviour
{
    [SerializeField]
    private Text PatientInfo;

    public void SetText(string patientInfo)
    {
        PatientInfo.text = patientInfo;
    }
}
