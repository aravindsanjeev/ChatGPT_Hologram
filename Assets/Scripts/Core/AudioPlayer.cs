using System;
using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using NAudio.Wave;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private const bool deleteCachedFile = true;

    private void OnEnable()
    {
        if (!audioSource) this.audioSource = GetComponent<AudioSource>();
    }

    private void OnValidate() => OnEnable();

    public void ProcessAudioBytes(byte[] audioData)
    {
        //string filePath = Path.Combine(Application.persistentDataPath, "audio.mp3");
        string filePath = Application.persistentDataPath+"/audio.mp3";

        Debug.Log("File path For Audio File: " + filePath);

        try
        {
            File.WriteAllBytes(filePath, audioData);
            Debug.Log("File written successfully. Size: " + new FileInfo(filePath).Length + " bytes");

            // Optionally, validate the file after writing
            if (ValidateAudioFile(filePath))
            {
                //StartCoroutine(LoadAndPlayAudio(filePath));
                StartCoroutine(LoadAndPlayAudioWithNAudio(filePath));
            }
            else
            {
                Debug.LogError("Invalid audio file format.");
                if (deleteCachedFile) File.Delete(filePath);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to write audio file: " + ex.Message);
        }
    }

    private bool ValidateAudioFile(string filePath)
    {
        // Perform additional checks on the file to ensure it is a valid audio file
        // This example checks the file size, but you can add more sophisticated validation
        FileInfo fileInfo = new FileInfo(filePath);
        return fileInfo.Length > 0; // Basic check: ensure file is not empty
    }

    private IEnumerator LoadAndPlayAudioWithNAudio(string filePath)
    {
        Debug.Log("Attempting to load audio from: " + filePath);

        yield return new WaitForEndOfFrame(); // Just to keep the coroutine signature

        try
        {
            using (var mp3Reader = new Mp3FileReader(filePath))
            {
                var waveFormat = mp3Reader.WaveFormat;
                int sampleCount = (int)(mp3Reader.Length / waveFormat.BlockAlign);
                float[] audioData = new float[sampleCount];

                int i = 0;
                byte[] buffer = new byte[waveFormat.BlockAlign];
                int bytesRead;

                while ((bytesRead = mp3Reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int j = 0; j < bytesRead / 2; j++)
                    {
                        audioData[i++] = BitConverter.ToInt16(buffer, j * 2) / 32768f;
                    }
                }

                AudioClip audioClip = AudioClip.Create("audioClip", audioData.Length, waveFormat.Channels, waveFormat.SampleRate, false);
                audioClip.SetData(audioData, 0);

                audioSource.clip = audioClip;

                if(!Samples.Whisper.WhisperAndAI.IsWhisperInAction)
                audioSource.Play();

                Debug.Log("%%% NAudio clip successfully loaded and played.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error while loading audio clip: " + ex.Message);
        }

        if (deleteCachedFile)
        {
            Debug.Log("Deleting cached audio file: " + filePath);
            File.Delete(filePath);
        }
    }

    private IEnumerator LoadAndPlayAudio(string filePath)
    {
        /*Debug.Log("Attempting to load audio from: " + filePath);

        using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.MPEG);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Successfully loaded audio file.");
            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
                
                if (audioClip != null)
                {
                    audioSource.clip = audioClip;
                    audioSource.Play();
                }
           
        }
        else Debug.LogError("Audio file loading error: " + www.error);*/

        string url = "file://" + filePath;
        Debug.Log("Attempting to load audio from: " + filePath);

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                try
                {
                    AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
                    if (audioClip != null)
                    {
                        audioSource.clip = audioClip;
                        audioSource.Play();
                        Debug.Log("Audio clip successfully loaded and played.");
                    }
                    else
                    {
                        Debug.LogError("Audio clip is null after loading.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error while loading audio clip: " + ex.Message);
                }
            }
            else
            {
                Debug.LogError("Audio file loading error: " + www.error);
            }
        }

            if (deleteCachedFile) File.Delete(filePath);
    }
}