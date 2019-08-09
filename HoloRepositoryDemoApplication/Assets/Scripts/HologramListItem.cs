using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HoloStorageConnector;

public class HologramListItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI HologramInfo = null;
    private Patient Patient;
    private Hologram Hologram;

    private void Start()
    {
        HologramInfo = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string HologramInformation)
    {
        HologramInfo.text = HologramInformation;
    }

    public void SetPatient(Patient patient)
    {
        Patient = patient;
    }

    public void SetHologram(Hologram hologram)
    {
        Hologram = hologram;
    }

    public void LoadHologram()
    {
        HologramList.SceneSwitchFlag = true;
        HologramDisplayScene.Patient = Patient;
        HologramDisplayScene.Hologram = Hologram;
        HologramInstantiationSettings setting = new HologramInstantiationSettings
        {
            Name = "Loaded Model",
            Rotation = new Vector3(0, 180, 0),
            Position = new Vector3(0f, 0f, 2f),
            SceneName = "HologramDisplayScene",
            Size = 0.5f
        };
        HoloStorageClient.LoadHologram("ef051fa3-ccfd-4a3e-8bf6-3cd4c1c8dc23", setting);
    }
}