using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using OpenAI;
namespace Samples.Whisper
{
    public class WhisperAndAI : MonoBehaviour
    {
        public static bool IsWhisperInAction = false;
        [SerializeField] public Text message;
        [SerializeField] private GameEvent EventStartRecording;
        [SerializeField] private GameEvent EventStopRecording;
        [SerializeField] private int RecordingLength = 5;
        [SerializeField] GameObject LoadingBlocker;

        private readonly string fileName = "output.wav";
        private AudioClip clip;
        private OpenAIApi openai = new OpenAIApi();

        private void Update()
        {
            if(LoadingBlocker.activeInHierarchy)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartRecording();
            }
            else if (Input.GetKeyUp(KeyCode.R))
            {
                EndRecording();
            }
        }


        public void OnCallBackDataLoadedFromJson()
        {
            RecordingLength = AI_DataManager.Instance.DataObject.VoiceRecorderLength;
        }
        
        public void OnCallBack_TrainingCompleted()
        {
            LoadingBlocker.SetActive(false);
        }



        public void StartRecording()
        {
            AI_VoiceManager.Instance.StopSpeech();

            IsWhisperInAction = true;

            var index = PlayerPrefs.GetInt("user-mic-device-index");

            #if !UNITY_WEBGL
            clip = Microphone.Start(Microphone.devices[0], false, RecordingLength, 44100);
#endif

            EventStartRecording?.InvokeEvent();
        }

        public async void EndRecording()
        {
            message.text = "Transcripting...";

            IsWhisperInAction = false;

            #if !UNITY_WEBGL
            Microphone.End(Microphone.devices[0]);
            #endif

            byte[] data = SaveWav.Save(fileName, clip);

            var req = new CreateAudioTranscriptionsRequest
            {
                FileData = new FileData() { Data = data, Name = "audio.wav" },
                // File = Application.persistentDataPath + "/" + fileName,
                Model = "whisper-1",
                Language = "en"
            };
            var res = await openai.CreateAudioTranscription(req);

            message.text = res.Text;

            EventStopRecording?.InvokeEvent();
        }
    }
}
