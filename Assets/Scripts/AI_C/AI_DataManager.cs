using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class AI_DataManager : MonoBehaviour
{
    [SerializeField] string configFileName = "Config.JSON";
    public string configPath;
    [SerializeField] GameEvent Event_DataLoaded;
    public AI_FilesModelClass DataObject = new AI_FilesModelClass();
    //public AI_ChatGPT_Integration chatGPT = new AI_ChatGPT_Integration();
    public IDCard_Manager cardManager;
    public BG_Changer BGChanger;

    #region Singleton
    public static AI_DataManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
    #endregion
    private void OnEnable()
    {
        configPath = Path.Combine(Application.streamingAssetsPath, configFileName);
        ReadFiles();
    }

    private void ReadFiles()
    {
        string jsonData = System.IO.File.ReadAllText(configPath);
        var data = JsonUtility.FromJson<AI_FilesModelClass>(jsonData);

        if (data == null)
        {
            Debug.Log("No Files");
        }
        else
        {
            Event_DataLoaded.InvokeEvent();

            Debug.Log("Found Files : ");
            DataObject = data;
            //DataObject.paths = new path();
            DataObject.paths = data.paths;

            //chatGPT.Initialize();
            cardManager.OnDataLoaded_CallBack();
            BGChanger.ChangeBG();
}
    }

}
