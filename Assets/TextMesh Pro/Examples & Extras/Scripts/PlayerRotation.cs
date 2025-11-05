using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Quaternion target;
    public bool isRotating = false;
    public float rotationSpeed = 180f;


    public void Rotate(){//Just moved part of the script up here, used as a function(important)
        if(isRotating)
        {
            float degreesPerSecond = 90f / rotationSpeed;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotationSpeed * Time.deltaTime);
            
            //Checks if we lookin'
            if(Quaternion.Angle(transform.rotation, target) < 0.1f)
            {
                transform.rotation = target; //Snap the rest of the way(to stop the rotating)
                isRotating = false;
            }
        }
    }

    public void StartRotation(float degrees) //This is for MM
    {
        target *= Quaternion.Euler(0, degrees, 0);
        isRotating = true;
    }

    void Start()
    {
        target = transform.rotation; //target is where you want to be looking
    }
    
    void Update()
    {
        

        //You can turn, if you aren't already turning
        if(!isRotating)
        {
            if(Input.GetKeyDown(InputManagement.instance.GetKey("Left")))
            {
                target *= Quaternion.Euler(0, -90, 0);
                isRotating = true;
            }

            if(Input.GetKeyDown(InputManagement.instance.GetKey("Right")))
            {
                target *= Quaternion.Euler(0, 90, 0);
                isRotating = true;
            }
        }
        Rotate();
    }
}