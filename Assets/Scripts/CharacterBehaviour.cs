using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField] GameObject QuestionPage;
    public void OnEventCallBackScreenSaver()
    {
        if(QuestionPage.activeInHierarchy)
        {
            PlayerOnScreenSaver();
        }
    }

    void PlayerOnScreenSaver()
    {
        //player animation
        //player call a song
        print(" Screen Saver from Character");
        if(UIManager.SelectedLanguage == LanguageData.Arabic)
        AudioManager.Instance.PlayAudio(THHDataManager.Instance.Data.ScreenSaverAudioAr);
        else if(UIManager.SelectedLanguage == LanguageData.English)
        AudioManager.Instance.PlayAudio(THHDataManager.Instance.Data.ScreenSaverAudioEn);

    }
}
