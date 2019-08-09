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

    public static bool InitialFlag = true;
    public static bool SceneSwitchFlag = false;
    public static List<Hologram> hologramList = new List<Hologram>();
    public static Patient Patient;

    public void Start()
    {
        if (InitialFlag)
        {
            Title.text = "Hologram List";
            Message.text = "Please select a patient to check the Hologram";
            return;
        }
        if (SceneSwitchFlag)
        {
            GenerateListView(hologramList);
            Title.text = $"{Patient.name.given} {Patient.name.family}";
            SceneSwitchFlag = false;
            return;
        }

        StartCoroutine(getAllHolograms(Patient.pid));
    }

    IEnumerator getAllHolograms(string patientID)
    {       
        yield return HoloStorageClient.GetMultipleHolograms(hologramList, patientID, QueryType.pid);
        if (hologramList.Count == 0)
        {
            Message.text = "There is no Holograms for this patient";
        }
        else
        {
            GenerateListView(hologramList);
            Title.text = $"{Patient.name.given} {Patient.name.family}";
        }
    }

    private void GenerateListView(List<Hologram> hologramList)
    {
        foreach (Hologram hologram in hologramList)
        {
            GameObject button = Instantiate(buttonTemplates) as GameObject;
            button.SetActive(true);

            button.GetComponent<HologramListItem>().SetPatient(Patient);
            button.GetComponent<HologramListItem>().SetHologram(hologram);
            button.GetComponent<HologramListItem>().SetText($"<b><size=12>{hologram.title}</b></size>\nBody Site: {hologram.bodySite}\nDate of Creation: {hologram.creationDate.Substring(0, 10)}");

            button.transform.SetParent(buttonTemplates.transform.parent, false);
        }
    } 
}
