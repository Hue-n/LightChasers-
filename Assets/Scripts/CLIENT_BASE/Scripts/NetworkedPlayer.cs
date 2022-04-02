using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public abstract class NetworkedPlayer : MonoBehaviour
{
    protected Rigidbody rb;
    protected Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        SendPlayerStateToServer();
    }

    //Step 2: Set the Player Data to be sent.
    private void SendPlayerStateToServer()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        ClientSend.PlayerMovement(position, rotation, rb.velocity);
    }
}
