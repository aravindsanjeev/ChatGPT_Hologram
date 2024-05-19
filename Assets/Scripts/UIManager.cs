using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum LanguageData {English,Arabic};
public class UIManager : MonoBehaviour
{
    public static LanguageData SelectedLanguage;
    [Header("Canvases")]
    [SerializeField]GameObject StartCanvas;
    [SerializeField]GameObject LanguageCanvas;
    [SerializeField]GameObject IndustryPanel;
    [SerializeField]GameObject QuestionPanel;
    [SerializeField]GameEvent BackOrHomeEventForAudioToStop;

    public GameEvent ChangeLanguageEvent;
    LanguageData l_Data;
    private void OnEnable()
    {
        SelectedLanguage = LanguageData.English;

        GoToStartPage();
    }

    private void OnDisable()
    {
    }

    void HideAll()
    {
        StartCanvas.SetActive(false);
        LanguageCanvas.SetActive(false);
        IndustryPanel.SetActive(false);
        QuestionPanel.SetActive(false);
    }

    public void BackButton(GameObject obj)
    {
        HideAll();
        obj.SetActive(true);
        BackOrHomeEventForAudioToStop?.InvokeEvent();
    }

    
    public void StartButtonAction()
    {
        HideAll();
        LanguageCanvas.SetActive(true);
    }

    public void LanguageButtonAction(string language)
    {
        if(language.Equals( LanguageData.English.ToString()))
        {
            l_Data = LanguageData.English;
        }
        else
        {
            l_Data = LanguageData.Arabic;
        }
        SelectedLanguage = l_Data;
        LanguageObject obj = new LanguageObject();
        obj.ObjectLanguageData = l_Data;
        ChangeLanguageEvent.InvokeEvent(obj);
        HideAll();
        IndustryPanel.SetActive(true);
    }

    public void IndustrySelection(int index)
    {
        HideAll();
        //populate ui with index
        THHDataManager.Instance.IndustrySelected(index);
        QuestionPanel.SetActive(true);
    }

    public void GoToHome()
    {
        HideAll();
        StartCanvas.SetActive(true);
    }
    public void GoToStartPage()
    {
        HideAll();
        StartCanvas.SetActive(true);

    }

    public void OnEventCallBackScreenSaver()
    {

        if (StartCanvas.activeInHierarchy)
        {
            return;
        }


        print("Called Screen Saver from UI");
        if (!StartCanvas.activeInHierarchy)
        {
            print("SS - TO HOME");
            GoToHome();
        }
        

    }

    public void SongButtonAction()
    {
        AudioManager.Instance.PlaySong();
    }
}

public class LanguageObject: MonoBehaviour
{
    public LanguageData ObjectLanguageData;
}