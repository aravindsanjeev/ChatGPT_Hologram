using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateWithMic : MonoBehaviour
{
    [SerializeField] Animator animator;

    
    public void OnStartTalking()
    {
        animator.SetInteger("Id", 1);
    }


    public void OnStopTalking()
    {
        animator.SetInteger("Id", 0);
    }

}
