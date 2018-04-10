using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{

    public float FadeOutAfter = 4.0f;
    public float FadeOutDuration = 2.0f;

    private float displayTime;

    private void Start()
    {
        displayTime = Time.time;
    }

    void Update()
    {
        if (Time.time > displayTime + FadeOutAfter + FadeOutDuration)
        {
            Destroy(gameObject);
        }
        else if (Time.time > displayTime + FadeOutAfter)
        {
            GetComponentInChildren<CanvasGroup>().alpha = 1 - ((Time.time - displayTime - FadeOutAfter) / FadeOutDuration);
        }
    }
}
