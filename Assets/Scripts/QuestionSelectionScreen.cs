using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuestionSelectionScreen : MonoBehaviour, IMenu
{
    [Header("Arabic")]
    [SerializeField] List<Sprite> Cat_Btn_img_Arb = new List<Sprite>();
    [SerializeField] Transform ArabicQuestionsParent;
    [SerializeField] GameObject ArabicQuestionPrefab;
    [SerializeField] GameObject ArabicQuestionPanel;
    [SerializeField] Image IndustryImageArabic;
    [SerializeField] ScrollRect Arb_Que_Vw;
   // [SerializeField] GameObject Arb_Scrl_Ind;

    [Header("English")]
    [SerializeField] List<Sprite> Cat_Btn_img_Eng = new List<Sprite>();
    [SerializeField] Transform EnglishQuestionsParent;
    [SerializeField] GameObject EnglishQuestionPrefab;
    [SerializeField] GameObject EnglishQuestionPanel;
    [SerializeField] Image IndustryImageEnglish;
    [SerializeField] ScrollRect Eng_Que_Vw;
   // [SerializeField] GameObject Eng_Scrl_Ind;

    [SerializeField] GameEvent ScreenSaverQuestionsTriggered;
    [SerializeField] GameEvent ScreenSaverTriggered;

    TimeSpan TimeGapInSecondsForQuestion = TimeSpan.FromSeconds(60);

    private List<QuestionElement> Questions = new List<QuestionElement>();
    private float previousScrollPosition = 0f;
    private ScrollRect Active_Scroll;
    private GameObject Active_Scroll_indicator;
    private float endThreshold = 0.015f;
    Industry selectedIndustry;
    bool canScroll;
    bool ScreenSaverCalledOnce = false;
    bool isAudioPlaying = false;
    DateTime LastActivity;
    void OnEnable()
    {
        
        ScreenSaverCalledOnce = false;
        LastActivity = DateTime.Now;
        TimeGapInSecondsForQuestion = TimeSpan.FromSeconds(THHDataManager.Instance.Data.ScreenSaverTimeQuestion);

        //clear if already populated
        if (Questions.Count != 0)
        {
            DeleteAllQuestions();
        }

        HideAllPanel();

        selectedIndustry = THHDataManager.Instance.SelectedIndustry;

        if (UIManager.SelectedLanguage == LanguageData.English)
        {
            EnglishQuestionPanel.SetActive(true);

            PopulateDataEnglish(selectedIndustry);

            previousScrollPosition = 0f;
            Active_Scroll = Eng_Que_Vw;
            //Active_Scroll_indicator = Eng_Scrl_Ind.gameObject;

            IndustryImageEnglish.sprite = Cat_Btn_img_Eng[selectedIndustry.Index-1];

        }
        else
        {
            ArabicQuestionPanel.SetActive(true);

            PopulateDataArabic(selectedIndustry);

            previousScrollPosition = 0f;
            Active_Scroll = Arb_Que_Vw;
            //Active_Scroll_indicator = Arb_Scrl_Ind.gameObject;

            IndustryImageArabic.sprite = Cat_Btn_img_Arb[selectedIndustry.Index - 1];
        }

        Active_Scroll.verticalNormalizedPosition = 1;

        if (selectedIndustry.Questions.Length > 3)
        {
            Active_Scroll.movementType = ScrollRect.MovementType.Elastic;
            Active_Scroll.content.gameObject.GetComponent<VerticalLayoutGroup>().padding.bottom = 40;
            //Active_Scroll_indicator.SetActive(true);
            canScroll = true;
        }
        else
        {
            Active_Scroll.movementType = ScrollRect.MovementType.Clamped;
            Active_Scroll.content.gameObject.GetComponent<VerticalLayoutGroup>().padding.bottom = 0;
            //Active_Scroll_indicator.SetActive(false);
            canScroll = false;
        }




    }
    void OnDisable()
    {

    }

    void HideAllPanel()
    {
        EnglishQuestionPanel.SetActive(false);
        ArabicQuestionPanel.SetActive(false);
    }

    void DeleteAllQuestions()
    {
       foreach (QuestionElement q in Questions)
        {
            Destroy(q.gameObject);
        }
        Questions.Clear();
    }

    void Update()
    {
        /*if(canScroll)
        DetecTheEndOfScroll();*/

       
        //ScreenSaverOnQuestionPage
        if (Input.anyKey)
        {
            LastActivity = DateTime.Now;
            ScreenSaverCalledOnce = false;
        }
        else if(isAudioPlaying)
        {
            LastActivity = DateTime.Now;

        }
        else
        {
            //print("conunting idle time : " + (DateTime.Now - LastActivity));
            if (DateTime.Now - LastActivity >= TimeGapInSecondsForQuestion/* && CanCountIdleTime*/)
            {
                print("Screen saver Normal");
                LastActivity = DateTime.Now;
                if (!ScreenSaverCalledOnce)
                {
                    ScreenSaverQuestionsTriggered?.InvokeEvent();
                    ScreenSaverCalledOnce = true;
                }
                else
                {
                    ScreenSaverTriggered?.InvokeEvent();
                }
            }
        }
    }

    /*void DetecTheEndOfScroll()
    {
        float currentScrollPosition = Active_Scroll.normalizedPosition.y;

         if (currentScrollPosition <= endThreshold)
        {
            
            if(Active_Scroll_indicator.activeInHierarchy)
                Active_Scroll_indicator.SetActive(false);
        }
        else if (currentScrollPosition > endThreshold)
        {
            
            if (!Active_Scroll_indicator.activeInHierarchy)
                Active_Scroll_indicator.SetActive(true);
        }
        
    }*/

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

    public void PopulateDataEnglish(Industry industry)
    {
        
        foreach (QuestionInfo q in industry.Questions)
        {
            GameObject g = Instantiate(EnglishQuestionPrefab);
            g.transform.parent = EnglishQuestionsParent;
            g.transform.localScale = new Vector3(1, 1, 1);
            var info = g.GetComponent<QuestionElement>();
            info.EnglishQusetionLabel = q.QuestionDescriptionEnglish;
            info.EnglishAnswerName = q.AnswerAudioFileNameEnglish;
            info.SetQuestion(q.QuestionDescriptionEnglish);
            Questions.Add(info);
        }
        
    }

    public void PopulateDataArabic(Industry industry)
    {
        
        foreach (QuestionInfo q in industry.Questions)
        {
            GameObject g = Instantiate(ArabicQuestionPrefab);
            g.transform.parent = ArabicQuestionsParent;
            g.transform.localScale = new Vector3(1, 1, 1);
            var info = g.GetComponent<QuestionElement>();
            info.ArabicQuestionLabel = q.QuestionDescriptionArabic;
            info.ArabicAnswerName = q.AnswerAudioFileNameArabic;
            info.SetQuestion(q.QuestionDescriptionArabic);
            Questions.Add(info);
        }
        //setImage

    }

    public void AudioPlayBackStarted_Callback()
    {
        isAudioPlaying = true;
    }
    public void AudioPlayBackStopped_Callback()
    {
        isAudioPlaying = false;
    }

}
