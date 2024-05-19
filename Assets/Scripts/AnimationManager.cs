using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] AudioSource audioSource;

    bool isPlaying = false;

    void Update()
    {
        // Check if audio is playing and wasn't playing in the previous frame
        if (audioSource.isPlaying && !isPlaying)
        {
            Debug.Log("Audio started playing.");
            isPlaying = true;
            // Add any additional logic you need when audio starts playing
            OnStartTalking();
        }
        // Check if audio was playing in the previous frame and stopped
        else if (!audioSource.isPlaying && isPlaying)
        {
            Debug.Log("Audio stopped playing.");
            isPlaying = false;
            // Add any additional logic you need when audio stops playing
            OnStopTalking();
        }
    }

    void OnStartTalking()
    {
        currentIndex = GetRandomNumberExcluding(1,4,currentIndex);
        //print("######################################### ="+currentIndex);
        animator.SetInteger("Id", currentIndex);
        //animator.SetInteger("Id", 2);
    }

    void OnStartSinging()
    {
        animator.SetInteger("Id", 4);
        //animator.SetInteger("Id", 2);
    }

    public void OnCallBackAudioStarted(UnityEngine.Object obj)
    {
        AnimIndicator ind = (AnimIndicator)obj;
        if (ind.index == 1)
            OnStartTalking();
        else if (ind.index == 2)
            OnStartSinging();
    }

    public void OnStopTalking()
    {
        animator.SetInteger("Id", 0);
    }

    //-----Random from 3
    int currentIndex = 0;

    int GetRandomNumberExcluding(int min, int max, int excludedValue)
    {
        int randomValue;
        do
        {
            randomValue = Random.Range(min, max);
        } while (randomValue == excludedValue);

        return randomValue;
    }
}

//0 => stop, 1 => talk, 2 => song
public class AnimIndicator : MonoBehaviour
{
    public int index;
}
