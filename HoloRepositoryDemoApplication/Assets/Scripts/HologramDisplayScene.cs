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

        string patientBirthDate = patient.birthDate == "" ? "Unknown" : patient.birthDate.Substring(0, 10);
        string hologramDateOfImaging = hologram.dateOfImaging == "" ? "Unknown" : hologram.dateOfImaging.Substring(0, 10);
        string hologramCreationDate = hologram.creationDate == "" ? "Unknown" : hologram.creationDate.Substring(0, 10);
    
        PatientInfo.text = 
            $"<b>Gender: </b>{patient.gender}\n" +
            $"<b>Date of birth: </b>{patientBirthDate}";

        HologramTitle.text = hologram.title;

        HologramInfo.text = 
            $"<b>Description: </b>{hologram.description}\n" +
            $"<b>Content Type: </b>{hologram.contentType}\n" +
            $"<b>File Size: </b>{hologram.fileSizeInKb}KB\n" +
            $"<b>Body Site: </b>{hologram.bodySite}\n" +
            $"<b>Date Of Imaging: </b>{hologramDateOfImaging}\n" +
            $"<b>Creation Date: </b>{hologramCreationDate}\n" +
            $"<b>Creation Mode: </b>{hologram.creationMode}\n" +
            $"<b>Creation Description: </b>{hologram.creationDescription}";
    }
}