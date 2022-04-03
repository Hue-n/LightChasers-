using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public CanvasGroup quitButton;
    float quitTime = 3;
    float currentTime = 0;
    bool quitting = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitting = true;        
        }

        if (quitting)
        {
            quitButton.alpha = Mathf.Lerp(0, 1, currentTime/quitTime);
            currentTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            quitting = false;
            currentTime = 0;
            quitButton.alpha = 0;
        }

        if (currentTime >= quitTime)
        {
            Application.Quit();
        }
    }
}
