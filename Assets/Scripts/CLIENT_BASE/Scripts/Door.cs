using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string sceneToTransition;
    public BoxCollider bC;

    // Start is called before the first frame update
    void Start()
    {
        bC = GetComponent<BoxCollider>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { 
            Debug.Log("Triggered");
        }
    }

    public IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(2f);
    }
}
