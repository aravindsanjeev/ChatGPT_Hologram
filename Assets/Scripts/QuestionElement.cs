using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ArabicSupport;
public class QuestionElement : MonoBehaviour
{
    public Button QuestionButton;
    public TextMeshProUGUI textElement;
    public TextMeshProUGUI textElement1Arabic;
    public TextMeshProUGUI textElement2Arabic;
    public string EnglishQusetionLabel;
    public string ArabicQuestionLabel;
    public string EnglishAnswerName;
    public string ArabicAnswerName;
    public GameEvent QusBtnPrsd_Event;
    public GameObject StaticFingerImg;
    public GameObject DynamicFingerImg;

    //public LanguageObject obj = new LanguageObject();
    private void OnEnable()
    {
        //obj.ObjectLanguageData = LanguageData.English;
        QuestionButton.onClick.AddListener(QuestionButtonAction);

        MakeAllImageStatic_CallBack();
    }
    private void OnDisable()
    {
        QuestionButton.onClick.RemoveListener(QuestionButtonAction);
    }

    void QuestionButtonAction()
    {
        QusBtnPrsd_Event?.InvokeEvent();
        

        if (UIManager.SelectedLanguage == LanguageData.English)
        {
            AudioManager.Instance.PlayAudio(EnglishAnswerName);
            print("English Audio");
        }
        else if (UIManager.SelectedLanguage == LanguageData.Arabic)
        {
            AudioManager.Instance.PlayAudio(ArabicAnswerName);
            print("Arabic Audio");
        }

        MakeMyImageDynamic_CallBack();
    }

    public void MakeAllImageStatic_CallBack()
    {
        // is active then hide and activate the other
        if(DynamicFingerImg.activeInHierarchy)
        {
            DynamicFingerImg.SetActive(false);
            StaticFingerImg.SetActive(true);
        }
    }

    void MakeMyImageDynamic_CallBack()
    {
        if (!DynamicFingerImg.activeInHierarchy)
        {
            DynamicFingerImg.SetActive(true);
            StaticFingerImg.SetActive(false);
        }
    }

    public void OnLanguageChangedCallBack(Object ob)
    {

        /* obj = (LanguageObject)ob;
         if (obj.ObjectLanguageData == LanguageData.English)
         {
             textElement.isRightToLeftText = false;
             textElement.text = EnglishQusetionLabel;
         }
         else if (obj.ObjectLanguageData == LanguageData.Arabic)
         {
             textElement.isRightToLeftText = true;
             textElement.text = ArabicQuestionLabel;
         }*/
    }

    public void SetQuestion(string que)
    {

        textElement.text = que;

        if (UIManager.SelectedLanguage == LanguageData.Arabic)
        {
            string newText = ArabicFixer.Fix(que, true, true);

            if (newText.Length > 100)
            {
                var strArray = SplitSentence(newText);
                textElement1Arabic.text = strArray[0];
                textElement2Arabic.text = strArray[1];
                textElement.text = "";

            }
            else
            {
                textElement.text = newText;
            }
        }

        string[] SplitSentence(string input)
        {
            // Check if the input string is null or empty
            if (string.IsNullOrEmpty(input))
            {
                return new string[0]; // Return an empty array
            }
            int totalLength = input.Length;
            int RemainingLineLength = totalLength - 100;
            int NearestSpacePosition = FindNearestSpace(input, RemainingLineLength);
            print("Length = "+totalLength + " Target pos ="+NearestSpacePosition);
            // Split the input into two parts, with the first part containing up to 81 characters
            string part1 = input.Substring(0, NearestSpacePosition);
            string part2 = input.Substring(Mathf.Min(input.Length, NearestSpacePosition));

            // Remove leading or trailing whitespace from part2
            part2 = part2.Trim();

            // Create an array to hold the split sentences
            string[] result = { part2, part1 };

            return result;
        }

        int FindNearestSpace(string input, int targetPosition)
        {
            int newTargetPos = targetPosition;
            for(int i = targetPosition; i<input.Length-1; i++)
            if(char.IsWhiteSpace(input[i]))
            {   
                    return i;
            }

            return newTargetPos; // No whitespace found near the target position
        }


    }
}
