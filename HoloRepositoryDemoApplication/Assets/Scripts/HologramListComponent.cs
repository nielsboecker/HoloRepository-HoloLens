using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HologramListComponent : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI HologramInfo;
    [SerializeField]
    private Text HologramID;

    private void Start()
    {
        HologramInfo = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string HologramInformation)
    {
        HologramInfo.text = HologramInformation;
    }
    public void SetID(string id)
    {
        HologramID.text = id;
    }
}
