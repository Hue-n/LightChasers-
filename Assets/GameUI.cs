using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI cataTimeText;

    // Start is called before the first frame update
    void Start()
    {
        GameSetter.timeCaster += SetTimeText;
        GameSetter.cataTime += SetCataTimeText;
    }

    private void OnDestroy()
    {
        GameSetter.timeCaster -= SetTimeText;
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
}
