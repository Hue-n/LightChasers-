using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerMovementAlex : MonoBehaviour
{

    bool canInput = true;

    public Transform cam;

    public float speed = 20;
    public float sensitivity = 3;
    
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
            constantForward();
            HandleRotation();
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
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

    
    void constantForward()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector2 userInput = new Vector2(moveX, moveZ).normalized;
        Vector3 forwardRun = transform.forward;
        forwardRun *= speed;

        Vector3 moveDir = new Vector3(forwardRun.x, rb.velocity.y, forwardRun.z);
        
        rb.velocity = moveDir;
    }
    
}
