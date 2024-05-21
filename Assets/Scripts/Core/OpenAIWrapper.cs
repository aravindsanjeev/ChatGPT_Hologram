using System.Text;
using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using Samples.Whisper;

public class OpenAIWrapper : MonoBehaviour
{
    [SerializeField, Tooltip(("Your OpenAI API key. If you use a restricted key, please ensure that it has permissions for /v1/audio."))] private string openAIKey = "api-key";
    private readonly string outputFormat = "mp3";
    [SerializeField] WhisperAndAI WhisperClass; 
    [System.Serializable]
    private class TTSPayload
    {
        public string model;
        public string input;
        public string voice;
        public string response_format;
        public float speed;
    }

    public async Task<byte[]> RequestTextToSpeech(string text, TTSModel model = TTSModel.TTS_1, TTSVoice voice = TTSVoice.Alloy, float speed = 1f)
    {
        Debug.Log("Sending new request to OpenAI TTS.");
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openAIKey);

        TTSPayload payload = new TTSPayload
        {
            model = model.EnumToString(),
            input = text,
            voice = voice.ToString().ToLower(),
            response_format = this.outputFormat,
            speed = speed
        };

        string jsonPayload = JsonUtility.ToJson(payload);

        var txt = WhisperClass.message.text;
        WhisperClass.message.text = "Preparing Audio...";

        var httpResponse = await httpClient.PostAsync(
            "https://api.openai.com/v1/audio/speech", 
            new StringContent(jsonPayload, Encoding.UTF8, "application/json")
        );

        

        byte[] response = await httpResponse.Content.ReadAsByteArrayAsync();

        WhisperClass.message.text = "Response For: " + txt;

        if (httpResponse.IsSuccessStatusCode) return response;
        

        Debug.Log("Error: " + httpResponse.StatusCode);
        return null;
    }

    public void OnCallBack_TrainingCompleted()
    {
        openAIKey = AI_DataManager.Instance.DataObject.api_key;
        Debug.Log("api added");
    }

    public void SetAPIKey(string openAIKey) => this.openAIKey = openAIKey;
}