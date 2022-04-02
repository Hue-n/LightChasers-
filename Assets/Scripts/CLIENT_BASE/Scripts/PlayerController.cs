using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates
{ 
    moving = 0,
    menu,
    chat,
    dialogue
}

public class PlayerController : NetworkedPlayer
{
    // Kingdom Hearts Movement V.1

    [Header("References")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Transform lookDirection;
    public GameObject followTransform;

    [Header("Stats")]
    public float jumpForce;
    public bool isGrounded;

    public float sensitivity = 3f;
    public float rotationLerp = 0.5f;
    public float clampAngle = 85f;

    public bool isIdle;

    [Header("Utilities")]
    public Vector3 processedInput;
    public Vector2 look;
    public Vector3 rawInput;

    public Quaternion storedRotation;
    public float inputAngle;
    public Transform storedTransform;

    [SerializeField]
    private float verticalRotation = 0, horizontalRotation = 0;

    //Player Movement Variables
    public PlayerStates playerstate;

    CharacterController charaCon;
    public float initialSpeed;
    private bool sprint = false;
    public float speed = 5f;

    private void Awake()
    {
        storedTransform = followTransform.transform;
        initialSpeed = speed;
    }

    private void Start()
    {
        UIManager.stateCaster += UpdatePlayerState; 
    }

    private void FixedUpdate()
    {
        Move();
        RotateCamera();
    }

    void Move()
    {
        // Output
        rb.velocity = Vector3.Lerp(rb.velocity, processedInput, 0.2f);

        if (rawInput == Vector3.zero)
        {
            transform.rotation = storedRotation;
            storedTransform = lookDirection.transform;

            anim.SetBool("Running", false);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.up * inputAngle), 0.15f);
            storedRotation = transform.rotation;

            anim.SetBool("Running", true);
        }
    }

    void HandleInput()
    {
        followTransform.transform.position = transform.position;
        lookDirection.transform.position = transform.position;

        // Input
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        rawInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * speed;

        Vector3 forwardConversion = (storedTransform.right * rawInput.x) + (storedTransform.forward * rawInput.z);

        inputAngle = Mathf.Atan2(forwardConversion.x, forwardConversion.z) * 180 / Mathf.PI;

        // Process
        isIdle = rawInput.x == 0 && rawInput.y == 0 ? true : false;

        processedInput = new Vector3(forwardConversion.x, rb.velocity.y, forwardConversion.z);
        Debug.DrawRay(transform.position, processedInput, Color.blue, 5);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log(processedInput.y);
        }

        if (isGrounded)
        {
            anim.SetBool("Jump", false);
        }
        else
        {
            anim.SetBool("Jump", true);
        }
    }

    void HandleRotation()
    {
        // Input
        look = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));

        // Process
        verticalRotation += look.y * sensitivity;
        horizontalRotation += look.x * sensitivity;

        verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);
    }

    void RotateCamera()
    {
        followTransform.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
        lookDirection.localRotation = Quaternion.Euler(0, horizontalRotation, 0);
    }

    void ProcessSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprint = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprint = false;
        }

        if (sprint)
        {
            speed = initialSpeed * 2;
        }

        else if (!sprint)
        {
            speed = initialSpeed;
        }
    }

    private void Update()
    {
        switch (playerstate)
        {
            case PlayerStates.moving:
                HandleInput();
                HandleRotation();
                ProcessSprint();
                Cursor.lockState = CursorLockMode.Locked;
                return;
            
            case PlayerStates.menu:
                return;

            case PlayerStates.chat:
                return;
        }
    }

    public void UpdatePlayerState(PlayerStates _newState)
    {
        playerstate = _newState;
    }
}
