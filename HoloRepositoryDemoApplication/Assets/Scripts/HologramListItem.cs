using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HoloStorageConnector;

public class HologramListItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI HologramInfo = null;
    private Patient patient;
    private Hologram hologram;

    private void Start()
    {
        HologramInfo = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string HologramInformation)
    {
        HologramInfo.text = HologramInformation;
    }

    public void SetPatient(Patient setPatient)
    {
        patient = setPatient;
    }

    public void SetHologram(Hologram setHologram)
    {
        hologram = setHologram;
    }

    public void LoadHologram()
    {
        HologramList.sceneSwitchFlag = true;
        HologramDisplayScene.patient = patient;
        HologramDisplayScene.hologram = hologram;
        HologramInstantiationSettings setting = new HologramInstantiationSettings
        {
            Name = "Loaded Model",
            Rotation = new Vector3(0, 180, 0),
            Position = new Vector3(0f, 0f, 2f),
            SceneName = "HologramDisplayScene",
            Size = 0.5f
        };
        HoloStorageClient.LoadHologram(hologram.hid, setting);
    }
}