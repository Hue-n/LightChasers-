using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource musicSource;
    AudioSource sfxSource;

    public AudioClip explosion;

    private void Start()
    {
        musicSource = GetComponents<AudioSource>()[0];
        sfxSource = GetComponents<AudioSource>()[1];

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            MainMenu.startCaster += FadeOutSource;
            SceneManager.sceneLoaded += StartFadeInSource;
            AttackCollisionCheck.aWinCaster += PlayExplosion;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void FadeOutSource()
    {
        StartCoroutine(FadeOutSource(musicSource, 1));
    }

    void StartFadeInSource(Scene scene, LoadSceneMode mode)
    {
        FadeInSource();
    }

    void FadeInSource()
    {
        StartCoroutine(FadeInSource(musicSource, 1));
    }

    void ChangeClip(AudioSource source, AudioClip newClip)
    {
        source.Stop();
        source.clip = newClip;
        source.Play();
    }

    void PlayExplosion(int winner)
    {
        PlayOneShot(explosion);
    }

    void PlayOneShot(AudioClip newClip)
    {
        sfxSource.PlayOneShot(newClip);
    }


    IEnumerator FadeOutSource(AudioSource source, float duration)
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            source.volume = Mathf.Lerp(1, 0, currentTime/duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        source.volume = 0;
    }

    IEnumerator FadeInSource(AudioSource source, float duration)
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            source.volume = Mathf.Lerp(0, 1, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        source.volume = 1;
    }
}
