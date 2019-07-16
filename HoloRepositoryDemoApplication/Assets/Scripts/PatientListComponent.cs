using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PatientListComponent : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI PatientInfo;
    [SerializeField]
    private Text PatientID;
    public static List<HoloGrams> HologramsList = new List<HoloGrams>();

    private void Start()
    {
        PatientInfo = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string Patientinformation)
    {
        PatientInfo.text = Patientinformation;
    }
    public void SetID(string id)
    {
        PatientID.text = id;
    }

    public void TurnToHologramPage()
    {
        HologramsList.Clear();
        foreach (PatientInfo patient in GeneratePatientList.patientList)
        {
            if (patient.pid == PatientID.text)
            {
                foreach (HoloGrams hologram in patient.holograms)
                {
                    HologramsList.Add(hologram);
                }
            }
        }
        SceneManager.UnloadSceneAsync(2);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }
}
