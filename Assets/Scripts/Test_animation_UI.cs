using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_animation_UI : MonoBehaviour
{
    [SerializeField] Animator animator;
    int animIndex = 0;

    public void ChangeAnim(int val)
    {
        print("called value ");
        if(animIndex>0 && animIndex < 3)
        {
            animIndex = animIndex + val;
            
        }
        else if(animIndex == 0 && val == 1)
        {
            animIndex = animIndex + val;
        }
        else if (animIndex == 3 && val == -1)
        {
            animIndex = animIndex + val;
        }
        animator.SetInteger("Id", animIndex);
    }
   
}
