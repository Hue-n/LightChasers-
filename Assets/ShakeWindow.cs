using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RECT
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;
}

public class ShakeWindow : MonoBehaviour
{
    public float duration;
    public float magnitude;
    public float falloff;

    RECT rect;
    public bool isShaking;
    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    IntPtr window = GetForegroundWindow();

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isShaking)
        {
            StartCoroutine(Impulse(window, duration, magnitude, falloff));
        }
    }

    IEnumerator Impulse(IntPtr wndw, float _duration, float _magnitude, float _falloff)
    {
        isShaking = true;
        float currentTime = 0;
        float tempMag = _magnitude;

        while (currentTime < _duration)
        {
            GetWindowRect(wndw, ref rect);

            // set a random offset and every iteration, have the offset get closer and closer to zero.
            float randomOffset = UnityEngine.Random.Range(-tempMag, tempMag);
            tempMag = Mathf.Lerp(_magnitude, 0, currentTime / _duration);

            rect.Left += (int)randomOffset;
            rect.Left %= Screen.currentResolution.width;
            rect.Top += (int)randomOffset;
            rect.Top %= Screen.currentResolution.height;

            SetWindowPos(wndw, IntPtr.Zero, (int)rect.Left, (int)rect.Top, 780, 780, uFlags: 0);
            currentTime += Time.deltaTime;
            yield return null;
        }

        isShaking = false;
    }
}
