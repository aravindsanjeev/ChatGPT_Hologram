using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenAI;

public class AI_ChatGPT_Integration : MonoBehaviour
{
    [SerializeField] Text TranscriptionLabel;
    private OpenAIApi openai = new OpenAIApi();
    public string prompt;
    public string LengthStatement;
    [SerializeField] List<ChatMessage> messages = new List<ChatMessage>();

    #region Singleton
    public static AI_ChatGPT_Integration Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
    #endregion

    public void Initialize()
    {
        InitializeAsync();
    }
    public async void InitializeAsync()
    {
        Debug.Log("Initialized");
        prompt = AI_DataManager.Instance.DataObject.personaOfGPTModel;

        var newMessage = new ChatMessage()
        {
            Role = "user",
            Content = prompt
        };


        messages.Add(newMessage);

        var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "gpt-3.5-turbo-0613",
            Messages = messages
        });

    }

    public GameEvent TrainingCompletedEvent;

    public void TrainTheModel(List<string> data)
    {
        TrainChatGpt(data);
    }

    public void Sender()
    {
        SendToChatGpt(AI_DataManager.Instance.DataObject.Pretext + " " + TranscriptionLabel.text.ToString());
    }

    public async void SendToChatGpt(string msg)
    {
        var newMessage = new ChatMessage()
        {
            Role = "user",
            Content = msg
        };

        messages.Add(newMessage);

        var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "gpt-3.5-turbo-0613",
            Messages = messages
        });

        if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
        {
            var message = completionResponse.Choices[0].Message;
            message.Content = message.Content.Trim();

            messages.Add(message);

            Debug.Log(" Message taken : "+completionResponse.Choices[0].Message.Content);

            foreach(var c in completionResponse.Choices)
            Debug.Log(" Message All : "+c.Message.Content);

            string textToPass = message.Content;

            // pass this to the text to speech class
            AI_VoiceManager.Instance.TextToSpeech_AI(textToPass);
        }
        else
        {
            Debug.LogWarning("No text was generated from this prompt.");
        }
    }

    
    public async void TrainChatGpt(List<string> content)
    {
        int CompletedPages = 0;

        foreach (string msg in content)
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = msg
            };

            newMessage.Content += " Read this and dont repond";

            messages.Add(newMessage);

            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0613",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                //if (completionResponse.Choices[0].Message.Content.Contains("YES"))
                //{
                    CompletedPages++;
               // }
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
                break;
            }
        }

        if(CompletedPages == content.Count)
        {
            //Call The Event of Completed Training
            TrainingCompletedEvent?.InvokeEvent();
            Initialize();
        }
    }
}
