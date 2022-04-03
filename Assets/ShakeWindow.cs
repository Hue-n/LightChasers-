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
    public static event CatastropheDone cataDoneCaster;

    public float duration;
    public float magnitude;
    public float falloff;
    public float xOffset;
    public float yOffset;

    RECT rect;
    public bool isShaking;

    //[DllImport("user32.dll")]
    //static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

    //IntPtr window = GetForegroundWindow();

    private void Start()
    {
        yOffset = (Display.main.systemHeight / 2) - (780 / 2);
        xOffset = (Display.main.systemWidth / 2) + (780 / 2);
        GameSetter.shakeScreenCaster += StartImpulse;
    }

    private void OnDestroy()
    {
        GameSetter.shakeScreenCaster -= StartImpulse;
    }

    void StartImpulse(float _duration, float _magnitude, float _falloff)
    {
        if (!isShaking)
        { 
            StartCoroutine(Impulse(NewGameManager.window, _duration, _magnitude, _falloff));
        }
    }

    IEnumerator Impulse(IntPtr wndw, float _duration, float _magnitude, float _falloff)
    {
        Screen.fullScreen = false;
        isShaking = true;
        float currentTime = 0;
        float tempMag = _magnitude;

        rect.Left = (int)xOffset;
        rect.Top = (int)yOffset;
        SetWindowPos(wndw, IntPtr.Zero, (int)rect.Left, (int)rect.Top, 780, 780, uFlags: 0);

        while (currentTime < _duration)
        {
            GetWindowRect(wndw, ref rect);

            // set a random offset and every iteration, have the offset get closer and closer to zero.
            float randomOffset = UnityEngine.Random.Range(-tempMag, tempMag);
            tempMag = Mathf.Lerp(_magnitude, 0, currentTime / _duration);

            rect.Left += (int)randomOffset;
            rect.Top += (int)randomOffset;

            SetWindowPos(wndw, IntPtr.Zero, (int)rect.Left, (int)rect.Top, 780, 780, uFlags: 0);
            currentTime += Time.deltaTime;
            yield return null;
        }

        isShaking = false;
        Screen.fullScreen = true;
        Screen.SetResolution(1920, 1080, true);
        cataDoneCaster?.Invoke();
    }
}
