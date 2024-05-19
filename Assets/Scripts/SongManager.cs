using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    [SerializeField] AudioSource aud_Src_Vc;
    [SerializeField] AudioSource aud_Src_Bg;
    [SerializeField] AudioClip Sng_bg;
    [SerializeField] AudioClip Sng_vc;
    [SerializeField] GameEvent SongStarted;
    [SerializeField] GameEvent SongStopped;

    public static SongManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

    }

    bool calledOnce = true;

    public void PlaySong()
    {
        aud_Src_Bg.clip = Sng_bg;
        aud_Src_Vc.clip = Sng_vc;
        aud_Src_Bg.Play();
        aud_Src_Vc.Play();
        calledOnce = false;
        AudioPlayBackStarted();
    }


    private void Update()
    {
        if (!aud_Src_Vc.isPlaying)
        {
            if (!calledOnce)
            {
                calledOnce = true;
                AudioPlayBackStopped();
            }
        }
    }

    void AudioPlayBackStopped()
    {
        SongStarted?.InvokeEvent();
    }

    void AudioPlayBackStarted()
    {
        SongStopped?.InvokeEvent();
    }
}
