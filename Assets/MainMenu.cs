using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StartGame();

public class MainMenu : MonoBehaviour
{
    public static event StartGame startCaster;

    public void StartGame()
    {
        startCaster?.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
