using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientListComponent : MonoBehaviour
{
    [SerializeField]
    private Text listText;

    public void SetText(string mytext)
    {
        listText.text = mytext;
    }
}
