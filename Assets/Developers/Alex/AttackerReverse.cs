using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerReverse : MonoBehaviour
{
    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Attacker") || other.CompareTag("Defender"))
        {
            Pickup(other);
        }
    }

    void Pickup(Collider player)
    {
        Debug.Log("Attacker reversed");

        //call attacker script function that reverses their movement
        player.GetComponent<AttackerMovementAlex>().SetForward(false);

        Destroy(gameObject);
    }
}
