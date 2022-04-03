using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerHuen : MonoBehaviour
{
    bool canInput = true;
    bool inverted = false;

    public Transform cam;

    public GameObject invertedText;
    public GameObject SlowdownText;

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
        
        float invmoveX = -Input.GetAxisRaw("Horizontal");
        float invmoveZ = -Input.GetAxisRaw("Vertical");
        Vector2 invuserInput = new Vector2(-invmoveX, -invmoveZ).normalized;
        Vector3 invertedForward = transform.forward * invuserInput.y;
        invertedForward *= speed;

        Vector3 invertedmoveDir = new Vector3(invertedForward.x, rb.velocity.y, invertedForward.z);
        
        rb.velocity = invertedmoveDir;

        invertedText.SetActive(true);

        if(Input.GetKey(KeyCode.A))
        {
            rotation += Time.deltaTime * sensitivity;
        }

        if(Input.GetKey(KeyCode.D))
        {
            rotation -= Time.deltaTime * sensitivity;
        }

        if(Input.GetKey(KeyCode.W))
        {
            Debug.Log("W pressed");
            //rb.velocity = invertedmoveDir;
        }

        if(Input.GetKey(KeyCode.S))
        {
            Debug.Log("S pressed");
            //rb.velocity = invertedmoveDir;
        }

        
        transform.localRotation = Quaternion.Euler(0, rotation, 0);
        /*
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector2 userInput = new Vector2(moveX, moveZ).normalized;
        Vector3 invertedConversion = ((-transform.right) * userInput.x) + ((-transform.forward) * userInput.y);
        invertedConversion *= speed;

        Vector3 moveDir = new Vector3(invertedConversion.x, rb.velocity.y, invertedConversion.z);
                
        rb.velocity = moveDir;
        */

        currentTime -= Time.deltaTime;

        if(currentTime < 0)
        {
            inverted = false;
            currentTime = duration;
            invertedText.SetActive(false);
        }
    }


    public void slowdown()
    {
        speed = speed / 2;

        SlowdownText.SetActive(true);

        currentTime -= Time.deltaTime;

        if(currentTime < 0)
        {
            speed = 20;
            currentTime = duration;
            SlowdownText.SetActive(false);

        }


    }


}
