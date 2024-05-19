using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EyesBlinker : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer mesh;

    bool canClose = false;
    bool canOpen = false;
    bool canOpenClose = false;
    public float OpenSpeed = 20;
    public float CloseSpeed = 20;
    float incVal = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Blink());
    }
    
    IEnumerator Blink()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(3,8));
            {
                BlinkClose();
            }
        }
    }
    
    void BlinkClose()
    {
        incVal = 0;
        canOpenClose = true;
        canClose = true;
        
    }


    private void Update()
    {
        if(canOpenClose)
        {
            if (canClose)
            {
                if (mesh.GetBlendShapeWeight(15) <= 100)
                {
                    incVal += CloseSpeed;
                    mesh.SetBlendShapeWeight(15, incVal);
                }
                else
                {
                    canClose = false;
                    canOpen = true;
                }
            }
            else if(canOpen)
            {
                if (mesh.GetBlendShapeWeight(15) >= 10)
                {
                    incVal -= OpenSpeed;
                    mesh.SetBlendShapeWeight(15, incVal);
                }
                else
                {
                    canClose = false;
                    canOpen = false;
                    canOpenClose = false;
                }
            }
        }
    }
}
