using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameSetter.rotateMiniMap += StartRotate;
    }

    void StartRotate()
    { 
        
    }

    IEnumerator RotateMiniMap()
    { 
        float currentTime = 0;
        yield return null;
    }
}
