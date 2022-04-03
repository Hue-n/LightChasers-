using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerReverse : MonoBehaviour
{

    public GameObject Attacker;
    //public script AttackerMovementAlex;
    //AttackerMovementAlex test = new AttackerMovementAlex();
    //AttackerMovementAlex AttackerMovementAlex = Attacker.GetComponent<AttackerMovementAlex>();

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

        //player.GetComponent<AttackerMovementAlex>().SetForward(false);
        //Attacker.SetForward(false);
        //AttackerMovementAlex.SetForward(false);
        //test.constantBackward();
        //AttackerMovementAlex.SetForward(false);
        Attacker.GetComponent<AttackerMovementAlex>().SetForward(false);

        Destroy(gameObject);
    }
}
