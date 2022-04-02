using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerHuen : MonoBehaviour
{
    bool canInput = true;

    public Transform cam;

    public float speed = 3;
    public float sensitivity = 3;
    public float jumpForce = 3;

    private Rigidbody rb;


    float verticalRotation = 0;
    float horizontalRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInput)
        {
            CalculateMovement();
            HandleRotation();
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //void InterpretGameManager(GameCommands command)
    //{
    //    switch (command)
    //    {
    //        case GameCommands.dialogue:
    //            canInput = false;
    //            break;
    //        case GameCommands.exitDialogue:
    //            canInput = true;
    //            break;
    //        case GameCommands.transition:
    //            break;
    //    }
    //}

    void CalculateMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector2 userInput = new Vector2(moveX, moveZ).normalized;
        Vector3 forwardConversion = (transform.right * userInput.x) + (transform.forward * userInput.y);
        forwardConversion *= speed;

        Vector3 moveDir = new Vector3(forwardConversion.x, rb.velocity.y, forwardConversion.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveDir += transform.up * jumpForce;
        }

        rb.velocity = moveDir;
    }

    void HandleRotation()
    {
        Vector2 lookDir = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        verticalRotation += (-lookDir.y) * sensitivity;
        horizontalRotation += lookDir.x * sensitivity;

        verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(0, horizontalRotation, 0);
        cam.localRotation = Quaternion.Euler(verticalRotation, cam.localRotation.y, cam.localRotation.z);
    }
}
