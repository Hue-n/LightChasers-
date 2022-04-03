using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public delegate void ResetGame();
public class GameEnd : MonoBehaviour
{
    public static event ResetGame resetGameCaster;

    public TextMeshProUGUI winText;

    // Start is called before the first frame update
    void Start()
    {
        string winner;
        winner = NewGameManager.instance.GetWinner() == 1 ? "Attacker Wins!" : "Defender Wins!";
        winText.text = winner;
        Cursor.lockState = CursorLockMode.None;

        Screen.fullScreen = true;
        Screen.SetResolution(1920, 1080, true);
    }

    public void ReturnToMain()
    {
        resetGameCaster?.Invoke();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
