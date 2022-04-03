using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerMovementAlex : MonoBehaviour
{

    bool canInput = true;

    public Transform cam;
    public GameObject camera;

    public float speed = 20;
    public float sensitivity = 3;
    public float duration = 5f;
    
    float currentTime;
    private Rigidbody rb;

    float verticalRotation = 0;
    float horizontalRotation = 0;

    bool forward = true;
    bool cameraInverted = false;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentTime = duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (canInput && forward || canInput && !cameraInverted)
        {
            constantForward();
            HandleRotation();
            Cursor.lockState = CursorLockMode.Locked;
        }

        else if (canInput && !forward)
        {
            constantBackward();
            HandleRotation();
        }

        else if (canInput && cameraInverted)
        {
            constantForward();
            InvertedHandleRotation();
        }

        

        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SetForward(bool val)
    {
        forward = val;
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

    void InvertedHandleRotation()
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

    public void constantBackward()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector2 userInput = new Vector2(moveX, moveZ).normalized;
        Vector3 backwardRun = -transform.forward;
        backwardRun *= speed;

        Vector3 moveDir = new Vector3(backwardRun.x, rb.velocity.y, backwardRun.z);
        
        rb.velocity = moveDir;

        currentTime -= Time.deltaTime;

        if(currentTime < 0)
        {
            forward = true;
            currentTime = duration;
        }
    }

    public void flipCamera()
    {
        Debug.Log("Camera Flipped");
        Vector3 rotation = new Vector3(0, 0, 180);
        camera.transform.Rotate(rotation);

        cameraInverted = true;


    }
    
}
