using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerFlipCamera : MonoBehaviour
{

    public GameObject Attacker;

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Attacker") || other.CompareTag("Defender"))
        {
            Pickup(other);
        }
    }

    void Pickup(Collider player)
    {
        Debug.Log("Attacker camera flipped");

        //call defender script function that inverts their movement
        Attacker.GetComponent<AttackerMovementAlex>().flipCamera();

        Destroy(gameObject);
    }
}
