
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Meta.WitAi.TTS.Utilities;
using Meta.Voice.Audio;
using OpenAI;
using SpeechLib;
using System.Runtime.InteropServices;
using System.IO;
using UnityEngine.Networking;

public class AI_VoiceManager : MonoBehaviour
    {
    #region MicrosoftTTS
    public AudioSource TTSAudioSource;
    SpVoice voice = new SpVoice();
    private SpFileStream fileStream;
    private IEnumerator PlayAudio(string filePath)
    {
        // Wait for the file to be written to disk
        yield return new WaitForSeconds(1);

        // Load the audio file as a Unity AudioClip
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.WAV))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
                TTSAudioSource.clip = audioClip;
                TTSAudioSource.Play();
            }
        }
    }
    #endregion

    bool speakingStarted = false;
    //bool LastFile = false;
    int count = 0;
    int Totalcount = 0;
    string[] msgs;

    public TTSSpeaker voiceInput;

        #region singleton
        public static AI_VoiceManager Instance;

        private void Awake()
        {
            Instance = this;
        }
    #endregion

    /*private void Update()
    {
        if(voiceInput.AudioPlayer.IsPlaying)
        {
            speakingStarted = true;
        }

        if (speakingStarted == true)
        {
            if (!voiceInput.AudioPlayer.IsPlaying)
            {
                Debug.Log("/0/0/0/0/0");
                voiceInput.waitFromOutSide();
                speakingStarted = false;
               *//* voiceInput.StopSpeaking();
                count++;
                if(count>Totalcount)
                {
                    StopSpeech();
                    speakingStarted = false;
                }*//*
                //Debug.Log("clip speaking ## : "+voiceInput.SpeakingClip.textToSpeak);
            }


        }

        *//*if (LastFile)
        {
            if (!voiceInput.AudioPlayer.IsPlaying)
            {
                LastFile = false;
                StopSpeech();
            }
        }*//*
    }*/

    public void TextToSpeech_AI(string str)
    {
        /*
                //----------------------------------------
                *//*if(speakingStarted)
                 {
                 return;
                 }*//*
                count = 1;
                Debug.Log("received str for Voice Over: " + str);

                    //msgs = str.Split(".");



                    voiceInput.StartCoroutine(voiceInput.SpeakAsync(str));
                    //speakingStarted = true;
                    //LastFile = false;
                    Totalcount = voiceInput.QueuedClips.Count;
                    Debug.Log(Totalcount+" = *************************");

                //int count = 1;
                //voiceInput.SpeakingClip.clipID;

                // StartCoroutine(SpeakAsync(str, true));
                //voiceInput.Speak(str);

                //----------------------------------------
        */
        // Initialize the SpVoice and SpFileStream objects
        voice = new SpVoice();
        fileStream = new SpFileStream();

        // Set the output to a WAV file
        string tempFilePath = Path.Combine(Application.persistentDataPath, "tts_output.wav");
        fileStream.Open(tempFilePath, SpeechStreamFileMode.SSFMCreateForWrite, false);
        voice.AudioOutputStream = fileStream;

        // Speak text to the file
        voice.Speak(str, SpeechVoiceSpeakFlags.SVSFDefault);

        // Close the file stream
        fileStream.Close();

        // Load and play the audio in Unity
        StartCoroutine(PlayAudio(tempFilePath));
    }
        
        public void StopSpeech()
        {
            /*if(voiceInput.AudioPlayer.IsPlaying)
            {
                voiceInput.Stop();
                speakingStarted = false;
            }*/

            if(TTSAudioSource.isPlaying)
            {
                if (voice != null)
                {
                // Stop the current speech immediately
                voice.Speak(string.Empty, SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
                }
                TTSAudioSource.Stop();
            }
        }

        public void StartTestSpeech()
        {
            //string str = "Hi, How are you?";
            string str = "1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11, 12, 13, 14, 15, 16, 17, 18, 19, 20,21, 22, 23, 24, 25, 26, 27, 28, 29, 30,31, 32, 33, 34, 35, 36, 37, 38, 39, 40,41, 42, 43, 44, 45, 46, 47, 48, 49, 50. 51, 52, 53, 54, 55, 56, 57, 58, 59, 60,61, 62, 63, 64, 65, 66, 67, 68, 69, 70,71, 72, 73, 74, 75, 76, 77, 78, 79, 80,81, 82, 83, 84, 85, 86, 87, 88, 89, 90,91, 92, 93, 94, 95, 96, 97, 98, 99, 100.101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150.";
            //string str = "1, 2, 3, 4, 5. 6, 7, 8, 9, 10. 11, 12, 13, 14, 15.";
            TextToSpeech_AI(str);
        }
}