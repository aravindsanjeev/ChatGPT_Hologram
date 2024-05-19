using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class AIC_UIManager : MonoBehaviour
{

    //[SerializeField] GameEvent Event_Start_Record;
    //[SerializeField] GameEvent Event_Stop_Record;
    [SerializeField] GameObject LoadingBlocking;
    
    private void OnEnable()
    {
            //.onStartRecording;
    }
    
    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(LoadingBlocking.activeInHierarchy)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            StartRecording();
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            StopRecording();
        }*/
    }

    /*public void StartRecording()
    {
        Debug.Log("Started Recording");
        Event_Start_Record?.InvokeEvent();
    }

    public void StopRecording()
    {
        Debug.Log("Stopped Recording");
        Event_Stop_Record?.InvokeEvent();

    }*/

    public void CallBack_TrainingCompleted()
    {
        HideBlockerLoading();
    }

    void HideBlockerLoading()
    {
        LoadingBlocking.SetActive(false);
    }
}
