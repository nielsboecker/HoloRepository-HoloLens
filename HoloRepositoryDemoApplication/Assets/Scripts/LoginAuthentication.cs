using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginAuthentication : MonoBehaviour
{
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
        switch (PIN.text)
        {
            case "03825":
                PatientList.patientIds = "p100,p101,p102,p103,p104,p105,p106";
                PatientList.practitionerName = "Maudie";
                switchScene();
                break;
            case "03826":
                PatientList.patientIds = "p100,p107,p108,p109,p110";
                PatientList.practitionerName = "Erlinda";
                switchScene();
                break;
            case "03827":
                PatientList.patientIds = "p100,p105,p110";
                PatientList.practitionerName = "Jonah";
                switchScene();
                break;
            default:
                WarningInfo.text = "The PIN is wrong!";
                PIN.Select();
                PIN.text = "";
                break;
        }
    }

    public void Clear()
    {
        WarningInfo.text = "";
        PIN.Select();
        PIN.text = "";
    }

    public void switchScene()
    {
        Destroy(PINPad);
        SceneManager.LoadScene("PatientListScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("HologramListScene", LoadSceneMode.Additive);
    }
}
