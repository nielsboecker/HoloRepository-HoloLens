using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HoloStorageConnector;

public class HologramList : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplates = null;
    [SerializeField]
    private TextMeshProUGUI Title = null;
    [SerializeField]
    private TextMeshProUGUI Message = null;

    public static bool initialFlag = true;
    public static bool sceneSwitchFlag = false;
    public static List<Hologram> hologramList = new List<Hologram>();
    public static Patient patient;

    public void Start()
    {
        if (initialFlag)
        {
            Title.text = "Holograms";
            Message.text = "Please select a patient to browse available holograms";
            return;
        }
        if (sceneSwitchFlag)
        {
            GenerateListView(hologramList);
            Title.text = patient.name.full;
            sceneSwitchFlag = false;
            return;
        }

        StartCoroutine(getAllHolograms(patient.pid));
    }

    IEnumerator getAllHolograms(string patientID)
    {       
        yield return HoloStorageClient.GetMultipleHolograms(hologramList, patientID, QueryType.pids);
        if (hologramList.Count == 0)
        {
            Message.text = "There is no Holograms for this patient";
        }
        else
        {
            GenerateListView(hologramList);
            Title.text = patient.name.full;
        }
    }

    private void GenerateListView(List<Hologram> hologramList)
    {
        foreach (Hologram hologram in hologramList)
        {
            GameObject button = Instantiate(buttonTemplates) as GameObject;
            button.SetActive(true);

            button.GetComponent<HologramListItem>().SetPatient(patient);
            button.GetComponent<HologramListItem>().SetHologram(hologram);
            button.GetComponent<HologramListItem>().SetText($"<b><size=12>{hologram.title}</b></size>\nBody Site: {hologram.bodySite}\nDate of Creation: {hologram.creationDate.Substring(0, 10)}");

            button.transform.SetParent(buttonTemplates.transform.parent, false);
        }
    } 
}
