using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginAuthentication : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField PIN;
    [SerializeField]
    private TextMeshProUGUI WarningInfo;
    [SerializeField]
    private GameObject PINPad;

    private void Start()
    {
        TMP_InputField PIN = GetComponent<TMP_InputField>();
    }

    public void Auth()
    {
        if (PIN.text == "3825")
        {
            Destroy(PINPad);
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
        else
        {
            WarningInfo.text = "The PIN is wrong!";
            PIN.Select();
            PIN.text = "";
        }
    }

    public void Clear()
    {
        WarningInfo.text = "";
        PIN.Select();
        PIN.text = "";
    }
}
