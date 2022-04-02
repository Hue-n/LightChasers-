using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void FadeOutComplete();
public delegate void FadeInComplete();

public class NewGameManager : MonoBehaviour
{
    public static event FadeOutComplete fadeOutComplete;
    public static event FadeInComplete fadeInComplete;

    public static NewGameManager instance;
    public CanvasGroup fadeOut;
    public float maxGameTime;
    public float fadeTime;
    int winner;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += FadeIntoNew;
            GameSetter.winnerCaster += SetWinner;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void SetWinner(int id)
    {
        winner = id;
        StartCoroutine(SwitchScenes(2));
    }

    public void FadeIntoNew(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeIn(fadeTime));
    }

    public void StartGame()
    {
        StartCoroutine(SwitchScenes(1));
    }

    IEnumerator SwitchScenes(int scene)
    {
        yield return FadeOut(fadeTime);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
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
        fadeOut.alpha = 0;
        fadeInComplete?.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
