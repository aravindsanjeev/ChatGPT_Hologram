using System.Collections;
using System;
using UnityEngine;

[Serializable]
public class THHData 
{
    public int ScreenSaverTime;
    public int ScreenSaverTimeQuestion;
    public string ScreenSaverAudioEn;
    public string ScreenSaverAudioAr;
    public string SeaSongName;
    public int PassCode;
    public Industry[] Industries;
}

[Serializable]
public class Industry
{
    public int Index;
    public string IndustryName;
    public QuestionInfo[] Questions;
}

[Serializable]
public class QuestionInfo
{
    public int QuestionNumber;
    public string QuestionDescriptionEnglish;

    [SerializeField, TextArea(3, 5)]
    public string QuestionDescriptionArabic;
    public string AnswerAudioFileNameEnglish;
    public string AnswerAudioFileNameArabic;
}

[Serializable]
public class PasscodeData
{
    public int lic;
}
