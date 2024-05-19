using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public GameEvent AudPlayBackStopped_Event;
    public GameEvent AudPlayBackStarted_Event;
    public  AudioSource audioSource;
    bool calledOnce = true;

    [Header("Song")]
    [SerializeField] AudioSource aud_Src_Vc;
    [SerializeField] AudioSource aud_Src_Bg;
    [SerializeField] AudioClip Sng_bg;
    [SerializeField] AudioClip Sng_vc;
    

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
    private void Start()
    {
        
        //audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    public void PlayAudio(string audioFileName)
    {
        // Load the audio clip from the "Audio" folder inside "Resources"
        //AudioClip audioClip = Resources.Load<AudioClip>("AudioFiles/" + audioFileName);

        //var filePath = Path.Combine( Application.streamingAssetsPath ,"/AudioFiles/" + audioFileName);

        /*if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Audio file not found: " + audioFileName);
        }*/

        //string audioFilePath = Path.Combine(Application.streamingAssetsPath, "/AudioFiles/" + audioFileName+".mp3");
        string audioFilePath = Application.streamingAssetsPath.ToString()+"/AudioFiles/" + audioFileName+".mp3";
        Debug.Log("File Path:"+audioFilePath);

        if (!File.Exists(audioFilePath))
        {
            Debug.Log("File does not exist");
            return;
        }

        // Use a coroutine to load the audio clip asynchronously
        StartCoroutine(LoadAudio(audioFilePath));
    }


    IEnumerator LoadAudio(string path)
    {
        using (WWW www = new WWW(path))
        {
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                // Audio clip is loaded successfully
                AudioClip audioClip = www.GetAudioClip();

                if (audioClip != null)
                {
                    //stopping bg if playing
                    if (aud_Src_Bg.isPlaying)
                        aud_Src_Bg.Stop();
                    // Do something with the loaded audio clip
                    audioSource.clip = audioClip;
                    /*//manipulate listner distance
                    aud_Src_Bg.gameObject.transform.position = new Vector3(0, 0, 0);*/
                    audioSource.Play();
                    calledOnce = false;
                    AudioPlayBackTalkStarted();
                }
                else
                {
                    Debug.LogError("Failed to load audio clip.");
                }
            }
            else
            {
                Debug.LogError("Error loading audio: " + www.error);
            }
        }
    }

    bool isSongPlaying = false;
    public void PlaySong()
    {
        if (!isSongPlaying)
        {
            aud_Src_Bg.clip = Sng_bg;
            aud_Src_Vc.clip = Sng_vc;
            aud_Src_Bg.Play();
            /*//manipulate listner distance
            aud_Src_Bg.gameObject.transform.position = new Vector3(1000,1000,1000);*/
            aud_Src_Vc.Play();
            calledOnce = false;
            AudioPlayBackSongStarted();
            isSongPlaying = true;
        }
        else
        {
            aud_Src_Bg.Stop();
            aud_Src_Vc.Stop();
            isSongPlaying = false;
        }
    }

    private void Update()
    {
        if(!audioSource.isPlaying)
        {
            if(!calledOnce)
            {
                calledOnce = true;
                AudioPlayBackStopped();
            }
        }
    }

    void AudioPlayBackStopped()
    {
        AudPlayBackStopped_Event?.InvokeEvent();
        isSongPlaying = false;
    }



    void AudioPlayBackTalkStarted()
    {
        AnimIndicator ind = new AnimIndicator();
        ind.index = 1;
        AudPlayBackStarted_Event?.InvokeEvent(ind);
        isSongPlaying = false;
    }

    void AudioPlayBackSongStarted()
    {
        AnimIndicator ind = new AnimIndicator();
        ind.index = 2;
        AudPlayBackStarted_Event?.InvokeEvent(ind);
    }

    public void BackOrHomeEventPlayback()
    {
        AudioPlayBackStopped();

        if(aud_Src_Bg.isPlaying)
        {
            aud_Src_Bg.Stop();
        }

        if(aud_Src_Vc.isPlaying)
        {
            aud_Src_Vc.Stop();
        }

        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

}



