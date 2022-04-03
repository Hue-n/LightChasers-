using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerHuen : MonoBehaviour
{
    bool canInput = true;
    bool inverted = false;

    public Transform cam;

    public float speed = 3;
    public float sensitivity = 3;
    public float jumpForce = 3;
    float rotation;
    private Rigidbody rb;

    float currentTime;
    float timer;
    float duration = 5f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    //bool doubleJump;

    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation.y;
        rb = GetComponent<Rigidbody>();
        currentTime = duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (canInput && !inverted)
        {
            CalculateMovement();
            HandleRotation();
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (canInput && inverted)
        {
            InvertControls();
            CalculateMovement();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        /*
        if (isGrounded())
        {
            doubleJump = false;
        }
        */
    }

    public void SetInverted(bool val)
    {
        inverted = val;
    }

    void CalculateMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector2 userInput = new Vector2(moveX, moveZ).normalized;
        Vector3 forwardConversion = (transform.right * userInput.x) + (transform.forward * userInput.y);
        forwardConversion *= speed;

        Vector3 moveDir = new Vector3(forwardConversion.x, rb.velocity.y, forwardConversion.z);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            moveDir += transform.up * jumpForce;
        }

        /*
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded() && doubleJump == false)
        {
            moveDir += transform.up * jumpForce;
            doubleJump = true;
            Debug.Log("Double Jumped? " + doubleJump);
        }
        */

        rb.velocity = moveDir;
    }

    void HandleRotation()
    {
        if(Input.GetKey(KeyCode.D))
        {
            rotation += Time.deltaTime * sensitivity;
        }

        if(Input.GetKey(KeyCode.A))
        {
            rotation -= Time.deltaTime * sensitivity;
        }

        transform.localRotation = Quaternion.Euler(0, rotation, 0);
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }

    public void InvertControls()
    {
        if(Input.GetKey(KeyCode.A))
        {
            rotation += Time.deltaTime * sensitivity;
        }

        if(Input.GetKey(KeyCode.D))
        {
            rotation -= Time.deltaTime * sensitivity;
        }

        transform.localRotation = Quaternion.Euler(0, rotation, 0);

        currentTime -= Time.deltaTime;

        if(currentTime < 0)
        {
            inverted = false;
            currentTime = duration;
        }
    }


}
