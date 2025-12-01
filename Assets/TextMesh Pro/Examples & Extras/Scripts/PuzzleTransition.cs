using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTransition : MonoBehaviour
{
    // Start is called before the first frame update
    private Quaternion table; //To look at and away from the table
    bool lookAtTable = false;
    public PlayerRotation Rotation;
    public GameObject Puzzle;

    void Start()
    {
        Puzzle.SetActive(false);
    }
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("PuzzleTest")){
            Debug.Log("Trigger Entered");
            tableView();
        }
    }
    private void tableView(){
        lookAtTable = true;
        table = transform.rotation * Quaternion.Euler(22.5f, 0f, 0f);
    }
    public void tableViewAway(){
        lookAtTable = false;
        table = transform.rotation * Quaternion.Euler(-22.5f, 0f, 0f);
    }


    // Update is called once per frame
    void Update()
    {
        if(lookAtTable){
            transform.rotation = Quaternion.RotateTowards(transform.rotation, table, 180f * Time.deltaTime);
            
            //Checks if we lookin'
            if(Quaternion.Angle(transform.rotation, table) < 0.1f)
            {
                transform.rotation = table; //Snap the rest of the way(to stop the rotating)
                Puzzle.SetActive(true);
            }
        }
    }
}
