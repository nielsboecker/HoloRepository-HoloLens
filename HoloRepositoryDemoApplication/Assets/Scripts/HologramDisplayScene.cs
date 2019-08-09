using UnityEngine;
using HoloStorageConnector;
using TMPro;

public class HologramDisplayScene : MonoBehaviour
{   
    [SerializeField]
    private TextMeshProUGUI PatientName = null;
    [SerializeField]
    private TextMeshProUGUI PatientInfo = null;
    [SerializeField]
    private TextMeshProUGUI HologramTitle= null;
    [SerializeField]
    private TextMeshProUGUI HologramInfo = null;

    public static Patient Patient;
    public static Hologram Hologram;

    void Start()
    {
        PatientName.text = $"{Patient.name.given} {Patient.name.family}";

        PatientInfo.text = 
            $"<b><size=15>Gender: </size></b>{Patient.gender}\n" +
            $"<b><size=15>Date of birth: </size></b>{Patient.birthDate.Substring(0, 10)}";

        HologramTitle.text = Hologram.title;

        HologramInfo.text = 
            $"<b><size=15>Description:</size></b>\n{Hologram.description}\n" +
            $"<b><size=15>Content Type: </size></b>{Hologram.contentType}\n" +
            $"<b><size=15>File Size: </size></b>{Hologram.fileSizeInkb}\n" +
            $"<b><size=15>Body Site: </size></b>{Hologram.bodySite}\n" +
            $"<b><size=15>Date Of Imaging: </size></b>{Hologram.dateOfImaging.Substring(0, 10)}\n" +
            $"<b><size=15>Creation Date: </size></b>{Hologram.creationDate.Substring(0, 10)}\n" +
            $"<b><size=15>Creation Mode: </size></b>\n{Hologram.creationMode}\n" +
            $"<b><size=15>Creation Description:</size></b>\n{Hologram.creationDescription}";
    }
}