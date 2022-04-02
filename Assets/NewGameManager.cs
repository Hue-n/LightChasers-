using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void StartGame();
public delegate void FadeOutComplete();
public delegate void FadeInComplete();

public class NewGameManager : MonoBehaviour
{
    public static event StartGame startCaster;
    public static event FadeOutComplete fadeOutComplete;
    public static event FadeInComplete fadeInComplete;

    public static NewGameManager instance;
    public float maxGameTime;
    public float fadeTime;
    public CanvasGroup fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        StartCoroutine(SwitchScenes());
    }

    IEnumerator SwitchScenes()
    {
        yield return FadeOut(fadeTime);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    IEnumerator FadeOut(float _fadeOutTime)
    {
        float currentTime = 0;
        while (currentTime < _fadeOutTime)
        {
            fadeOut.alpha = Mathf.Lerp(0, 1, currentTime / _fadeOutTime);
            currentTime += Time.deltaTime;
            yield return null;
        }
        fadeOut.alpha = 1;
        fadeOutComplete?.Invoke();
    }

    IEnumerator FadeIn(float _fadeOutTime)
    {
        float currentTime = 0;
        while (currentTime < _fadeOutTime)
        {
            fadeOut.alpha = Mathf.Lerp(1, 0, currentTime / _fadeOutTime);
            currentTime += Time.deltaTime;
            yield return null;
        }
        fadeOut.alpha = 1;
        fadeInComplete?.Invoke();
    }
}
