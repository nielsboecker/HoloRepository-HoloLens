using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PatientListComponent : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI listText;
    [SerializeField]
    private Text PatientID;

    private void Start()
    {
        listText = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string info)
    {
        listText.text = info;
    }
    public void SetID(string id)
    {
        PatientID.text = id;
    }
}
