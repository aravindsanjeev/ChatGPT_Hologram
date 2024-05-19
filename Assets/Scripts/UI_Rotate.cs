using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UI_Rotate : MonoBehaviour
{
    public RectTransform uiElement; // Reference to your UI element (e.g., image's RectTransform)
    public float rotationSpeed = 90.0f; // Desired rotation angle

    private void OnEnable()
    {
        StartContinuousRotation();
    }

    private void StartContinuousRotation()
    {
        StartCoroutine(ContinuousRotationCoroutine());
    }

    private IEnumerator ContinuousRotationCoroutine()
    {
        while (true) // Continue rotating indefinitely
        {
            // Rotate the UI element by the specified speed over a short time interval
            uiElement.DORotate(new Vector3(0, 0, uiElement.eulerAngles.z + rotationSpeed * Time.deltaTime), 0.0f);

            yield return null;
        }
    }
}
