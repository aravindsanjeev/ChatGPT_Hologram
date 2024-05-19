using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageIK : MonoBehaviour
{

    Animator animator;
    public bool iKActive = false;
    public Transform ObjTarget;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK()
    {
        if(animator)
        {
            if(iKActive)
            {
                if(ObjTarget!=null)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(ObjTarget.position);
                }
            }
            else
            {
                animator.SetLookAtWeight(0);
            }
        }
    }
}
