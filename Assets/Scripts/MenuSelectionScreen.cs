using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuSelectionScreen : MonoBehaviour, IMenu
{
    [Header("Arabic Buttons")]
    [SerializeField] List<Sprite> Cat_Btn_img_Arb = new List<Sprite>();
    [SerializeField] GameObject ALabel;
    [Header("English Buttons")]
    [SerializeField] List<Sprite> Cat_Btn_img_Eng = new List<Sprite>();
    [SerializeField] GameObject ELabel;
    [Header("Buttons")]
    [SerializeField] List<Button> Cat_Buttons = new List<Button>();
    [SerializeField] float  PopTimeIntervel = 0.5f;
    [SerializeField] Button SongButton;
    [SerializeField] Sprite SongButtonArabic;
    [SerializeField] Sprite SongButtonEnglish;
    [SerializeField] Sprite SongButtonPauseArabic;
    [SerializeField] Sprite SongButtonPauseEnglish;


    private void OnEnable()
    {
        ShrinkAndHideAllButtons();
        CheckForLanguage();
    }

    private void OnDisable()
    {
        
    }

    void ShrinkAndHideAllButtons()
    {
        foreach(Button B in Cat_Buttons)
        {
            B.gameObject.SetActive(false);
            B.transform.localScale = Vector3.zero;
        }
    }

    IEnumerator PopButtons(int buttonCount)
    {
        while(buttonCount<=4)
        {
            
            yield return new WaitForSeconds(PopTimeIntervel);
            {
                Cat_Buttons[buttonCount].gameObject.SetActive(true);
                TweenSize(Cat_Buttons[buttonCount].transform);
                buttonCount++;
            }

        }
    }

    void TweenSize(Transform btn)
    {
        btn.DOScale(new Vector3(1.2f, 1.2f, 1.2f),0.5f).OnComplete(() => TweenSizeNormal(btn));
    }

    void TweenSizeNormal(Transform btn)
    {
        btn.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
    }


    void CheckForLanguage()
    {
        ALabel.SetActive(false);
        ELabel.SetActive(false);
        if (UIManager.SelectedLanguage == LanguageData.Arabic)
        {
            ALabel.SetActive(true);
            for(int i = 0; i < Cat_Buttons.Count; i++)
            {
                Cat_Buttons[i].image.sprite = Cat_Btn_img_Arb[i];
            }

            SongButton.image.sprite = SongButtonArabic; 
        }
        else
        {
            ELabel.SetActive(true);
            for (int i = 0; i < Cat_Buttons.Count; i++)
            {
                Cat_Buttons[i].image.sprite = Cat_Btn_img_Eng[i];
            }

            SongButton.image.sprite = SongButtonEnglish;
        }

        StartCoroutine(PopButtons(0));
    }

    public void IMenuAppear()
    {

    }
    public void IMenuDisappear()
    {

    }
    public void IMenuHide()
    {

    }
    public void IMenuShow()
    {

    }

    public void OnSongAudioPlaybackStarted()
    {
        if (UIManager.SelectedLanguage == LanguageData.Arabic)
        {
            SongButton.image.sprite = SongButtonPauseArabic;
        }
        else
        {
            SongButton.image.sprite = SongButtonPauseEnglish;
        }
    }

    public void OnSongAudioPlaybackStopped()
    {
        if (UIManager.SelectedLanguage == LanguageData.Arabic)
        {
            SongButton.image.sprite = SongButtonArabic;
        }
        else
        {
            SongButton.image.sprite = SongButtonEnglish;
        }
    }
}

