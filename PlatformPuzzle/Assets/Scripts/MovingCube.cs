using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public float speed = 1f;

    private void Update()
    {
        Vector3 myPosition = transform.position;

        myPosition = myPosition + transform.forward * speed * Time.deltaTime;

        transform.position = myPosition;
    }
}
