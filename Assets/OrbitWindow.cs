using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitWindow : MonoBehaviour
{
    public float distance;
    public float speed;
    public float frequency;
    public int xOffset;
    public int yOffset;

    public int newWidth;
    public int newHeight;

    //[DllImport("user32.dll")]
    //static extern IntPtr GetForegroundWindow();

    //IntPtr window = GetForegroundWindow();

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    // Start is called before the first frame update
    void Start()
    {
        yOffset = (Display.main.systemHeight / 2) - (newHeight / 2);
        xOffset = (Display.main.systemWidth / 2) + (newHeight / 2);
    }

    void OrbitForTime(float time)
    {
        StartCoroutine(OrbitExplorer(NewGameManager.window, time));
    }

    void TranslateForTime(float time)
    {
        StartCoroutine(TranslateForTimeCorout(NewGameManager.window, time));
    }

    IEnumerator TranslateForTimeCorout(IntPtr window, float duration)
    {
        Screen.fullScreen = false;

        float currentTime = 0;
        Vector2 newPos;

        while (currentTime < duration)
        {
            if (window != IntPtr.Zero)
            {
                newPos.x = ((currentTime * speed) + xOffset);
                newPos.y = yOffset + Mathf.Sin(currentTime * frequency) * distance;

                SetWindowPos(window, IntPtr.Zero, (int)newPos.x, (int)newPos.y, newWidth, newHeight, uFlags: 0);
                currentTime += Time.deltaTime;
                yield return null;
            }

            yield return null;
        }

        Screen.fullScreen = true;
        Screen.SetResolution(1920, 1080, true);
    }

    IEnumerator OrbitExplorer(IntPtr window, float duration)
    {
        Screen.fullScreen = false;

        float currentTime = 0;
        Vector2 newPos;
        
        while (currentTime < duration)
        {
            if (window != IntPtr.Zero)
            {
                newPos.x = -(Mathf.Sin(currentTime * frequency) * distance) + xOffset;
                newPos.y = -(Mathf.Cos(currentTime * frequency) * distance) + yOffset;
                //newPos.x = ((currentTime * speed) + xOffset);
                //newPos.y = yOffset + Mathf.Sin(currentTime * frequency) * distance;

                SetWindowPos(window, IntPtr.Zero, (int)newPos.x, (int)newPos.y, newWidth, newHeight, uFlags: 0);
                currentTime += Time.deltaTime;
                yield return null;
            }

            yield return null;
        }

        Screen.fullScreen = true;
        Screen.SetResolution(1920, 1080, true);
    }
}

