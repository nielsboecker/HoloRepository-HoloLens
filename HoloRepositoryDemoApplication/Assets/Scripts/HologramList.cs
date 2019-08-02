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
    private TextMeshProUGUI Message = null;
    public static bool InitialFlag = true;
    public static bool SceneSwitchFlag = false;
    public static List<Hologram> hologramList = new List<Hologram>();

    public void Start()
    {
        if (InitialFlag)
        {
            Message.text = "Please select a patient to check the Hologram";
            return;
        }
        if (SceneSwitchFlag)
        {
            GenerateListView(hologramList);
            SceneSwitchFlag = false;
            return;
        }

        StartCoroutine(getAllHolograms());
    }

    IEnumerator getAllHolograms()
    {       
        yield return HoloStorageClient.GetMultipleHolograms(hologramList, "IDs");
        if (hologramList.Count == 0)
        {
            Message.text = "There is no Holograms for this patient";
        }
        else
        {
            GenerateListView(hologramList);
        }
    }

    private void GenerateListView(List<Hologram> hologramList)
    {
        foreach (Hologram hologram in hologramList)
        {
            GameObject button = Instantiate(buttonTemplates) as GameObject;
            button.SetActive(true);

            button.GetComponent<HologramListItem>().SetID(hologram.hid);
            //string Info = string.Format("Hologram name: {0}\nTitle: {1}\nDate of Creation: {2}", hologram.subject.name.full, hologram.title, hologram.createdDate.Substring(0, 10));
            button.GetComponent<HologramListItem>().SetText($"Subject patient ID: {hologram.subject.pid}");

            button.transform.SetParent(buttonTemplates.transform.parent, false);
        }
    } 
}
