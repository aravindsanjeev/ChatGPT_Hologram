using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorHandler : MonoBehaviour
{
    public static ErrorHandler Instance;
    public GameObject ErrorPanel;
    public TextMeshProUGUI ErrorText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void LoadErrorMessage(string message)
    {
        ErrorText.text = message;
        ErrorPanel.SetActive(true);
    }

    public void CloseErrorMessage()
    {
        ErrorText.text = "";
        ErrorPanel.SetActive(false);
    }
}
