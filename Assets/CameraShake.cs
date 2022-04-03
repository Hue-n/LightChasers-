using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = 0;
    public float magnitude = 0;
    public float falloff = 0;

    //public float orbitSpeed = 2;
    //public float orbitDistance = 2;

    bool isShaking;

    float currentTime = 0;

    private void Start()
    {
        isShaking = false;
        AttackCollisionCheck.aWinCaster += Shake;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && isShaking == false)
        //{
            
        //}

        //Vector3 newPos = new Vector3(Mathf.Sin(currentTime * orbitSpeed) * orbitDistance, 
        //    transform.position.y, Mathf.Cos(currentTime * orbitSpeed) * orbitDistance);

        //// position needs to be set before the rotation or else it will be shaky
        //transform.position = newPos;
        //transform.rotation = Quaternion.LookRotation(targetPoint.position - transform.position, Vector3.up);

        //currentTime += Time.deltaTime;
    }

    void Shake(int a)
    {
        var ShakeCorout = StartCoroutine(Impulse(duration, magnitude, falloff));
    }

    IEnumerator Impulse(float _duration, float _magnitude, float _falloff)
    {
        isShaking = true;
        float currentTime = 0;
        float tempMag = _magnitude;

        while (currentTime < _duration)
        {
            // set a random offset and every iteration, have the offset get closer and closer to zero.
            float randomOffset = Random.Range(-tempMag, tempMag);
            tempMag = Mathf.Lerp(_magnitude, 0, currentTime / _duration);

            Quaternion newRot = new Quaternion();
            newRot = Quaternion.Euler(randomOffset, transform.rotation.y, transform.rotation.z);

            // i don't understand why multiplying this makes it work but it does???
            transform.rotation *= newRot;

            currentTime += Time.deltaTime;

            yield return null;
        }

        isShaking = false;
    }
}
