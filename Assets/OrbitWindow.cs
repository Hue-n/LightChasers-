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

    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    IntPtr window = GetForegroundWindow();

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OrbitExplorer(window));
    }

    IEnumerator OrbitExplorer(IntPtr window)
    {
        Screen.fullScreen = false;

        float currentTime = 0;
        Vector2 newPos;

        while (true)
        {
            if (window != IntPtr.Zero)
            {
                newPos.x = -(Mathf.Sin(currentTime * frequency) * distance) + xOffset;
                newPos.y = -(Mathf.Cos(currentTime * frequency) * distance) + yOffset;
                //newPos.x = ((currentTime * speed) % Screen.currentResolution.width) + xOffset;
                //newPos.y = yOffset + Mathf.Sin(currentTime * frequency) * distance;

                SetWindowPos(window, IntPtr.Zero, (int)newPos.x, (int)newPos.y, newWidth, newHeight, uFlags: 0);
                currentTime += Time.deltaTime;
                yield return null;
            }

            yield return null;
        }
    }
}

