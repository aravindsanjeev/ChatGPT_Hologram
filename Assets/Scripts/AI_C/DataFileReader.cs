using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class DataFileReader : MonoBehaviour
{
    List<string> AllLines = new List<string>();

    [SerializeField]
    int MaxLinesInOneGo = 50;

    [SerializeField]
    List<string> Lines = new List<string>();

    [SerializeField]
    AI_ChatGPT_Integration GPT;

    private void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Data.txt");
        StartCoroutine(ReadFile(filePath));
    }

    private System.Collections.IEnumerator ReadFile(string filePath)
    {
        string fileContents = "";

        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            using (WWW www = new WWW(filePath))
            {
                yield return www;
                fileContents = www.text;
            }
        }
        else
        {
            fileContents = File.ReadAllText(filePath);
        }

        AllLines = new List<string>(fileContents.Split('\n'));


        // Process lines
        for (int i = 0; i < AllLines.Count; i += MaxLinesInOneGo)
        {
            string chunk = "";
            for (int j = 0; j < MaxLinesInOneGo && i + j < AllLines.Count; j++)
            {
                chunk += AllLines[i + j];
            }
            Lines.Add(chunk);
        }

        //Debug.Log(Lines[2]);

        GPT.TrainTheModel(Lines);
    }
}
