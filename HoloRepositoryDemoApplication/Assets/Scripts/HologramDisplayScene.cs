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

    public static Patient patient;
    public static Hologram hologram;

    void Start()
    {
        PatientName.text = $"{patient.name.given} {patient.name.family}";

        PatientInfo.text = 
            $"<b><size=15>Gender: </size></b>{patient.gender}\n" +
            $"<b><size=15>Date of birth: </size></b>{patient.birthDate.Substring(0, 10)}";

        HologramTitle.text = hologram.title;

        HologramInfo.text = 
            $"<b><size=15>Description:</size></b>\n{hologram.description}\n" +
            $"<b><size=15>Content Type: </size></b>{hologram.contentType}\n" +
            $"<b><size=15>File Size: </size></b>{hologram.fileSizeInkb}\n" +
            $"<b><size=15>Body Site: </size></b>{hologram.bodySite}\n" +
            $"<b><size=15>Date Of Imaging: </size></b>{hologram.dateOfImaging.Substring(0, 10)}\n" +
            $"<b><size=15>Creation Date: </size></b>{hologram.creationDate.Substring(0, 10)}\n" +
            $"<b><size=15>Creation Mode: </size></b>\n{hologram.creationMode}\n" +
            $"<b><size=15>Creation Description:</size></b>\n{hologram.creationDescription}";
    }
}