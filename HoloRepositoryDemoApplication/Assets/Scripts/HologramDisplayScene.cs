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
        PatientName.text = patient.name.full;

        PatientInfo.text = 
            $"<b>Gender: </b>{patient.gender}\n" +
            $"<b>Date of birth: </b>{patient.birthDate.Substring(0, 10)}";

        HologramTitle.text = hologram.title;

        HologramInfo.text = 
            $"<b>Description: </b>{hologram.description}\n" +
            $"<b>Content Type: </b>{hologram.contentType}\n" +
            $"<b>File Size: </b>{hologram.fileSizeInkb}KB\n" +
            $"<b>Body Site: </b>{hologram.bodySite}\n" +
            $"<b>Date Of Imaging: </b>{hologram.dateOfImaging.Substring(0, 10)}\n" +
            $"<b>Creation Date: </b>{hologram.creationDate.Substring(0, 10)}\n" +
            $"<b>Creation Mode: </b>{hologram.creationMode}\n" +
            $"<b>Creation Description: </b>{hologram.creationDescription}";
    }
}