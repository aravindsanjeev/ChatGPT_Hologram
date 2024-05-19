using System;
using UnityEngine;

public class ScreenSaverManager : MonoBehaviour
{
    [SerializeField] GameEvent ScreenSaverTriggered;
    [SerializeField] int TimeGapForScreenSaver;
    [SerializeField] int TimeGapForScreenSaverOnQuestion;
    TimeSpan TimeGapInSeconds;
    TimeSpan TimeGapInSecondsForQuestion;
    DateTime LastActivity;
    bool CanCountIdleTime = true;
    bool isDataLoaded = false;
    bool isAudioPlaying = false;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        TimeGapInSeconds = TimeSpan.FromSeconds(TimeGapForScreenSaver);
        TimeGapInSecondsForQuestion = TimeSpan.FromSeconds(TimeGapForScreenSaverOnQuestion);
        LastActivity = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {

        if (isDataLoaded)
        {
            if (Input.anyKey || isAudioPlaying)
            {
                LastActivity = DateTime.Now;
                CanCountIdleTime = true;
            }

            else
            {
                if (DateTime.Now - LastActivity >= TimeGapInSeconds/* && CanCountIdleTime*/)
                {
                    print("Screen saver Normal");
                    ScreenSaverTriggered?.InvokeEvent();
                    LastActivity = DateTime.Now;
                    CanCountIdleTime = false;
                }
            }
        }

    }

    public void OnDataLoadedFromJsonEventCallBack()
    {
        TimeGapForScreenSaver = THHDataManager.Instance.Data.ScreenSaverTime;
        TimeGapForScreenSaverOnQuestion = THHDataManager.Instance.Data.ScreenSaverTimeQuestion;
        isDataLoaded = true;
    }

    public void AudioPlayBackStarted_Callback()
    {
        isAudioPlaying = true;
    }
    public void AudioPlayBackStopped_Callback()
    {
        isAudioPlaying = false;
    }
}
