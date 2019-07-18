using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HologramList : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplates;

    public void Start()
    {
        foreach (Hologram hologram in PatientListItem.HologramsList)
        {
            GameObject button = Instantiate(buttonTemplates) as GameObject;
            button.SetActive(true);

            button.GetComponent<HologramListItem>().SetID(hologram.hid);
            string Info = string.Format("Hologram name: {0}\nTitle: {1}\nDate of Creation: {2}", hologram.subject.name.full, hologram.title, hologram.createdDate.Substring(0, 10));
            button.GetComponent<HologramListItem>().SetText(Info);

            button.transform.SetParent(buttonTemplates.transform.parent, false);
        }
    }
}
