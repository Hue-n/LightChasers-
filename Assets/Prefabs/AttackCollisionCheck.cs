using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AttackerWin(int winner);
public class AttackCollisionCheck : MonoBehaviour
{
    public static event AttackerWin aWinCaster;

    bool gameOver = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Defender") && !gameOver)
        {
            aWinCaster?.Invoke(1);
            gameOver = true;
        }
    }
}
