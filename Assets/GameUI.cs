using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI cataName;
    public CanvasGroup cataGroup;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI cataTimeText;

    // Start is called before the first frame update
    void Start()
    {
        GameSetter.timeCaster += SetTimeText;
        GameSetter.cataTime += SetCataTimeText;
        GameSetter.cataName += ShowCataName;
    }

    private void OnDestroy()
    {
        GameSetter.timeCaster -= SetTimeText;
        GameSetter.cataTime -= SetCataTimeText;
        GameSetter.cataName -= ShowCataName;
    }

    void SetTimeText(float time)
    {
        TimeSpan _time = TimeSpan.FromSeconds(time);
        timeText.text = _time.ToString(@"mm\:ss");
    }

    void SetCataTimeText(float time)
    {
        TimeSpan _time = TimeSpan.FromSeconds(time);
        cataTimeText.text = _time.ToString(@"mm\:ss");
    }

    void ShowCataName(string newName)
    {
        cataName.text = newName;
        StartCoroutine(CataAesth());
    }

    IEnumerator CataAesth()
    {
        yield return FadeIn();
        yield return new WaitForSeconds(2);
        yield return FadeOut();
    }

    IEnumerator FadeIn()
    {
        float currentTime = 0;
        while (currentTime < 1)
        { 
            cataGroup.alpha = Mathf.Lerp(0, 1, currentTime / 1);
            currentTime += Time.deltaTime;
            yield return null;
        }
        cataGroup.alpha = 1;
    }

    IEnumerator FadeOut()
    {
        float currentTime = 0;
        while (currentTime < 1)
        {
            cataGroup.alpha = Mathf.Lerp(1, 0, currentTime / 1);
            currentTime += Time.deltaTime;
            yield return null;
        }
        cataGroup.alpha = 0;
    }
}
