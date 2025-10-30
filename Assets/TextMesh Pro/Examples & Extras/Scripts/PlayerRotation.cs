using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlayerRotation : MonoBehaviour
{
    
    private Quaternion target;
    public float rotationSpeed = 0.1f;
    void Start()
    {
        target = transform.rotation;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            target *= Quaternion.Euler(0, -90, 0);
            Thread.Sleep(125);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            target *= Quaternion.Euler(0, 90, 0);
            Thread.Sleep(125);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * rotationSpeed);
    }
}