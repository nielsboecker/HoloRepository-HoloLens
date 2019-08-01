using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keyboard : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField InputField = null;

    public void Number(string number)
    {
        InputField.text = InputField.text + number;
    }
    public void Delete()
    {
        if (InputField.text.Length > 0)
        {
            InputField.text = InputField.text.Substring(0, InputField.text.Length-1);
        }       
    }
    public void Appear()
    {
        gameObject.SetActive(true);
    }
    public void Disappear()
    {
        gameObject.SetActive(false);
    }
}
