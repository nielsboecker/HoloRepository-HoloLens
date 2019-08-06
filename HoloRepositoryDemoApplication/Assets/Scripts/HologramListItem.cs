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
        HologramList.SceneSwitchFlag = true;
        HologramInstantiationSettings setting = new HologramInstantiationSettings
        {
            Name = "Loaded Model",
            Rotation = new Vector3(0, 180, 0),
            Position = new Vector3(0f, 0f, 2f),
            SceneName = "HologramDisplayScene",
            Size = 0.5f
        };
        HoloStorageClient.LoadHologram("hid", setting);
    }
}
