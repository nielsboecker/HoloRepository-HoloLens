using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenerateHologramList : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplates;

    public void Start()
    {
        foreach (HoloGrams hologram in PatientListComponent.HologramsList)
        {
            GameObject button = Instantiate(buttonTemplates) as GameObject;
            button.SetActive(true);

            button.GetComponent<HologramListComponent>().SetID(hologram.hid);
            button.GetComponent<HologramListComponent>().SetText("Hologram name: " + hologram.subject.name.full + "\nTitle: " + hologram.title + "\nDate of Creation: " + hologram.createdDate.Substring(0, 10));

            button.transform.SetParent(buttonTemplates.transform.parent, false);
        }
    }
}
