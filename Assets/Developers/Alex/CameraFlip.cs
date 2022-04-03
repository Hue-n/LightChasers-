using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlip : MonoBehaviour
{

    public GameObject Camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void flipCamera()
    {
        Debug.Log("Camera Flipped");
        Vector3 rotation = new Vector3(0, 0, 180);
        Camera.transform.Rotate(rotation);
    }
}
