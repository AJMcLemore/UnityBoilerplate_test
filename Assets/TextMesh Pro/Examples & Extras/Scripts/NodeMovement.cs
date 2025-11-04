using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMovement : MonoBehaviour
{
    //Node Settings
    [SerializeField] private Transform[] nodePoints; //In case we want to assign nodes manually?(why is this here)
    [SerializeField] private string nodeTag = "NodePoint"; //Tag used to find nodes automatically
    
    //Player movement
    [SerializeField] private KeyCode moveKey = KeyCode.Space; // h o w  m o v e
    
    //Movement settings
    [SerializeField] private bool instantMove = false; //If true, player moves to each node instantly, like a teleport(may speed up debug?(or option?))
    [SerializeField] private float moveSpeed = 100f; //How fest player is(if above option is false)
    public LayerMask Wall;
    //[SerializeField] makes a private variable still editable in the inspector, FYI(Goose didn't know that when adding it LMAO)
    private bool isMoving = false;
    private Vector3 targetPosition;

    public float raycastDistance = 10f;

    void Start()
    {
        if (nodePoints == null || nodePoints.Length == 0)//If we don't manually add nodes(again, why would we??)
        {
            GameObject[] nodeObjects = GameObject.FindGameObjectsWithTag(nodeTag);
            nodePoints = new Transform[nodeObjects.Length];
            for (int i = 0; i < nodeObjects.Length; i++)
            {
                nodePoints[i] = nodeObjects[i].transform;
            }
        }
    }

    void Update()
    {
        PlayerRotation rotationScript = GetComponent<PlayerRotation>(); //Checks if the player is rotating
        Vector3 rayOrigin = transform.position; //Where the player is
        Vector3 rayDirection = transform.forward; //Forward, relative to player

        if (Input.GetKeyDown(moveKey) && !isMoving && !rotationScript.isRotating)
        {
            Transform closestNode = FindClosestNodeInFront();
             
            if (closestNode != null)
            {
                //debug stuff, in case someone breaks something(probably me, Goose.)
                Debug.Log("Found node: " + closestNode.name + " at position: " + closestNode.position);
                if (isClear(transform.position, closestNode.position))
                {
                    if (instantMove)
                    {
                        transform.position = closestNode.position;
                        Debug.Log("Moved to: " + transform.position);
                    }
                    else
                    {
                        targetPosition = closestNode.position;
                        isMoving = true;
                    }
                }
            }
            else
            {
                Debug.Log("No node found in front!");
            }
        }

        if (isMoving) //for shmooth shmovement
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    bool isClear(Vector3 start, Vector3 end)
    {
        Vector3 direction = end - start; //Find the direction
        
        float distance = direction.magnitude; //How far is the player?

        Debug.DrawRay(start, direction.normalized * distance, Color.cyan, 1f); //Debug, you can see where the player would go, whether they can or not
        
        if (Physics.Raycast(start, direction.normalized, distance, Wall)) //Cast a ray, and with this if statement it checks if it hits a wall
        {
            return false;
        }//Either wall or clear!
        return true;
    }

    Transform FindClosestNodeInFront()
    {
        Transform closestNode = null;
        float closestDistance = Mathf.Infinity;
        Vector3 playerPosition = transform.position;
        Vector3 playerForward = transform.forward;

        foreach (Transform node in nodePoints)
        {
            Vector3 directionToNode = node.position - playerPosition; //calculates the directions of each node(relative to player)
            
            float dotProduct = Vector3.Dot(directionToNode.normalized, playerForward); //checks if the node is forward(in front of player)

            if (dotProduct > 0.99f) //checks if the (closest)node is directly in front(no shimmying!!!)
            {
                float distance = directionToNode.magnitude;
                
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestNode = node;
                }
            }
        }

        return closestNode;
    }

    //Visualize nodes and if the player can go to them(In Scene View)
    void OnDrawGizmos()
    {
        if (nodePoints == null) return;

        Vector3 playerPos = transform.position;
        Vector3 playerForward = transform.forward;

        foreach (Transform node in nodePoints)
        {
            if (node == null) continue;

            Vector3 directionToNode = node.position - playerPos;
            float dotProduct = Vector3.Dot(directionToNode.normalized, playerForward);

            // Draw nodes in front as green, behind as red
            Gizmos.color = dotProduct > 0 ? Color.green : Color.red;
            Gizmos.DrawWireSphere(node.position, 0.5f);
            
            // Draw line from player to nodes in front
            if (dotProduct > 0)
            {
                //Gizmos.DrawLine(playerPos, node.position);
            }
        }
        
        // Draw player forward direction
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(playerPos, playerForward * 3f);
    }
}