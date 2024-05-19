using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(GameEventListener))]
public class UiElement : MonoBehaviour
{
    private TextMeshProUGUI textElement;
    public string EnglishLabel;
    public string ArabicLabel;
    private void Awake()
    {
        
    }

    private void Start()
    {
        textElement = GetComponent<TextMeshProUGUI>();
        //ErrorHandler.Instance.LoadErrorMessage("this is a test error");
    }

    

    public void OnLanguageChangedCallBack(Object ob )
    {
        var obj = new LanguageObject();
        obj = (LanguageObject)ob;
        if(obj.ObjectLanguageData == LanguageData.English)
        {
            textElement.isRightToLeftText = false;
            textElement.text = EnglishLabel;
        }
        else if(obj.ObjectLanguageData == LanguageData.Arabic)
        {
            textElement.isRightToLeftText = true;
            textElement.text = ArabicLabel;
        }
    }
}
