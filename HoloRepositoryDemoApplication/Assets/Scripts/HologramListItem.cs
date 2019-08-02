﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HoloStorageConnector;

public class HologramListItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI HologramInfo = null;
    [SerializeField]
    private Text HologramID = null;

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

    public void LoadHologram()
    {
        HologramInstantiationSettings setting = new HologramInstantiationSettings();
        setting.Name = "Loaded Model";
        setting.Rotation = new Vector3(0, 180, 0);
        setting.Position = new Vector3(0f, 0f, 2f);
        setting.SceneName = "ModelDisplayScene";
        setting.Size = 0.5f;
        HoloStorageClient.LoadHologram("hid", setting);
    }
}
