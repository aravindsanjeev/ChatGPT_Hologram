using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class THHDataManager : MonoBehaviour
{ 
    public static THHDataManager Instance;
    public string DataFileName;
    public THHData Data;
    public Transform QuestionsParent;
    public GameObject QuestionPrefab;
    public Industry SelectedIndustry;
    public GameEvent DataLoadedFromJSonEvent;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SelectedIndustry = new Industry();
        ReadJSONFromFile();
    }

    private void ReadJSONFromFile()
    {
        string filePath = Application.dataPath + "/StreamingAssets/DataFiles/Config.JSON";
        string dataAsJson = File.ReadAllText(filePath);
        THHData loadedData = JsonUtility.FromJson<THHData>(dataAsJson);
        Data = loadedData;
        // Read the JSON data from the file
        //TextAsset jsonTextAsset = Resources.Load<TextAsset>("DataFiles/" + DataFileName);

        /* if (jsonTextAsset != null)
         {
             // Deserialize the JSON data into your data structure
             THHData DataFromFile = JsonUtility.FromJson<THHData>(jsonTextAsset.text);
             Data = DataFromFile;
             //PopulateData(Data);
         }
         else
         {
             Debug.LogError("Failed to load JSON data.");
         }*/
        DataLoadedFromJSonEvent?.InvokeEvent();
    }

    public void IndustrySelected(int index)
    {
        SelectedIndustry = Data.Industries[index - 1];
        //PopulateData(Data.Industries[index-1]);
    }

   /* public void PopulateData(Industry industry)
    {
        foreach(QuestionInfo q in industry.Questions)
        {
            GameObject g = Instantiate(QuestionPrefab);
            g.transform.parent = QuestionsParent;
            g.transform.localScale = new Vector3(1,1,1);
            var info = g.GetComponent<QuestionElement>();
            info.EnglishQusetionLabel = q.QuestionDescriptionEnglish;
            info.ArabicQuestionLabel = q.QuestionDescriptionArabic;
            info.EnglishAnswerName = q.AnswerAudioFileNameEnglish;
            info.ArabicAnswerName = q.AnswerAudioFileNameArabic;
            info.SetQuestion(q.QuestionDescriptionEnglish);
        }
    }*/

}
