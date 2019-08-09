using UnityEngine;
using TMPro;

public class ListItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI buttonText = null;

    private void Start()
    {
        buttonText = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string Information)
    {
        buttonText.text = Information;
    }
}
