using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CanvasGroup fadeGroup; // this is the black thing on screen 
    public float fadeDuration = 1.5f; // how fast it fades

    void Start()
    {
        // make sure it's black at start
        if (fadeGroup != null)
        {
            fadeGroup.alpha = 1f;
            fadeGroup.blocksRaycasts = true;

            StartCoroutine(FadeOutNow());
        }
        else
        {
            Debug.Log("no fade group found oops");
        }
    }

    IEnumerator FadeOutNow()
    {
        float timer = 0f;

        // fading loop 
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float a = 1f - (timer / fadeDuration); 
            if (fadeGroup != null)
            {
                fadeGroup.alpha = a;
            }

            yield return null; // wait
        }

        // just to make sure it's totally invisible
        if (fadeGroup != null)
        {
            fadeGroup.alpha = 0f;
            fadeGroup.blocksRaycasts = false;
        }
    }
}
