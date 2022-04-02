using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        GameSetter.timeCaster += SetTimeText;   
    }

    void SetTimeText(float time)
    {
        TimeSpan _time = TimeSpan.FromSeconds(time);
        timeText.text = _time.ToString(@"mm\:ss");
    }
}
