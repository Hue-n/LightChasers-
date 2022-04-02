using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AttackerWin(int winner);
public class AttackCollisionCheck : MonoBehaviour
{
    public static event AttackerWin aWinCaster;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Defender")
        {
            aWinCaster?.Invoke(1);
        }
    }
}
