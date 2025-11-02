using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Quaternion target;
    public float rotationSpeed = 90f;
    public bool isRotating = false;
    
    void Start()
    {
        target = transform.rotation; //target is where you want to be looking
    }
    
    void Update()
    {
        //You can turn, if you aren't already turning
        if (!isRotating)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                target *= Quaternion.Euler(0, -90, 0);
                isRotating = true;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                target *= Quaternion.Euler(0, 90, 0);
                isRotating = true;
            }
        }
        
        //Smoothly rotate, becuase we don't need whiplash playing a game :]
        if (isRotating)
        {
            float degreesPerSecond = 90f / rotationSpeed;
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                target, 
                30f * Time.deltaTime //Degrees per second
            );
            
            //Checks if we lookin'
            if (Quaternion.Angle(transform.rotation, target) < 0.1f)
            {
                transform.rotation = target; //Snap the rest of the way(to stop the rotating)
                isRotating = false;
            }
        }
    }
}