using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AI_FilesModelClass
{
    public path paths;
    public int VoiceRecorderLength;
    public string api_key;
    public string organization;
    public string personaOfGPTModel;
    public string Pretext;
}

[Serializable]
public class path
{
    public string CharacterBG;
    public string Screen2BG;
    public string IDCardTexture;
    public string IDCardIcon;
}

[Serializable]
public class TrainedData
{
    public List<string> pages;
}


