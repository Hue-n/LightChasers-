using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderInvertedMovement : MonoBehaviour
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
        Debug.Log("Defender Inverted");

        //call defender script function that inverts their movement
        //player.GetComponent<PlayerControllerHuen>().SetForward(false);

        Destroy(gameObject);
    }
}
