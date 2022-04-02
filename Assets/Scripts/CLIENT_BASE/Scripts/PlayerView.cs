using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerManager))]
public class PlayerView : MonoBehaviour
{
    public PlayerManager pM;
    public Animator anim;
    public TextMeshPro username;

    // Start is called before the first frame update
    void Start()
    {
        pM = GetComponent<PlayerManager>();
        anim = GetComponentInChildren<Animator>();
        //username.text = pM.username;
    }

    private void FixedUpdate()
    {
        VelocityInterpreter(pM.velocity);
    }

    void VelocityInterpreter(Vector3 velocity)
    {
        //if (velocity == Vector3.zero)
        //{
        //    anim.SetBool("Running", false);
        //    anim.SetBool("Jump", false);
        //}

        //else if (velocity.y > 0)
        //{
        //    anim.SetBool("Jump", true);
        //    anim.SetBool("Running", false);
        //}

        //else if (velocity != Vector3.zero && Mathf.Approximately(velocity.y, 0))
        //{
        //    anim.SetBool("Running", true);
        //    anim.SetBool("Jump", false);
        //}
    }
}
