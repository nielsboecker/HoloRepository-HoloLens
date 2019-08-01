using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginAuthentication : MonoBehaviour
{
    [SerializeField]
    private bool DevelopMode = false;
    [SerializeField]
    private TMP_InputField PIN = null;
    [SerializeField]
    private TextMeshProUGUI WarningInfo = null;
    [SerializeField]
    private GameObject PINPad = null;

    private void Start()
    {
        TMP_InputField PIN = GetComponent<TMP_InputField>();
    }

    public void Auth()
    {
        if (DevelopMode)
        {
            Destroy(PINPad);
            SceneManager.LoadScene("PatientListScene", LoadSceneMode.Additive);
            SceneManager.LoadScene("HologramListScene", LoadSceneMode.Additive);
        }
        else
        {
            if (PIN.text == "3825")
            {
                Destroy(PINPad);
                SceneManager.LoadScene("PatientListScene", LoadSceneMode.Additive);
                SceneManager.LoadScene("HologramListScene", LoadSceneMode.Additive);
            }
            else
            {
                WarningInfo.text = "The PIN is wrong!";
                PIN.Select();
                PIN.text = "";
            }
        }
    }

    public void Clear()
    {
        WarningInfo.text = "";
        PIN.Select();
        PIN.text = "";
    }
}
