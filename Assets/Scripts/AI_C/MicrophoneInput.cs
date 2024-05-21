using UnityEngine;
using System.Collections.Generic;

public class MicrophoneInput : MonoBehaviour
{
    public int sampleRate = 44100; // Sample rate for audio recording
    [SerializeField]private AudioSource audioSource;
    private AudioClip microphoneClip;

    [Header("Lip Sync Detection")]
    public Animator animator;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public List<int> blendShapeIndices; // List of indices for blendshapes to monitor
    public float threshold = 10f; // Threshold value to detect speaking (adjust as needed)
    private bool isLipSyncing = false;


    void Start()
    {
        StartMicrophone();
    }


    void Update()
    {
        if (skinnedMeshRenderer != null && blendShapeIndices != null && blendShapeIndices.Count > 0)
        {
            bool isSpeaking = false;

            foreach (int index in blendShapeIndices)
            {
                float blendShapeValue = skinnedMeshRenderer.GetBlendShapeWeight(index);

                // If any blendshape value exceeds the threshold, consider the character as speaking
                if (blendShapeValue > threshold)
                {
                    isSpeaking = true;
                    break;
                }
            }

            if (isSpeaking && !isLipSyncing)
            {
                OnStartTalking();
                isLipSyncing = true;
            }
            else if (!isSpeaking && isLipSyncing)
            {
                OnStopTalking();
                isLipSyncing = false;
            }
        }

    }

    public void OnStartTalking()
    {
        animator.SetInteger("Id", 1);
    }


    public void OnStopTalking()
    {
        animator.SetInteger("Id", 0);
    }

    void StartMicrophone()
    {
        if (Microphone.devices.Length > 0)
        {
            string micName = Microphone.devices[0];
            microphoneClip = Microphone.Start(micName, true, 10, sampleRate);
            audioSource.clip = microphoneClip;
            audioSource.loop = true;

            // Wait until the microphone starts recording
            while (!(Microphone.GetPosition(null) > 0)) { }
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No microphone connected!");
        }
    }

    void OnApplicationQuit()
    {
        if (Microphone.IsRecording(null))
        {
            Microphone.End(null);
        }
    }
}
