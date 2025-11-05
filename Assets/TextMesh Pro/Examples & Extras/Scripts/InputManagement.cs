using System.Collections.Generic;
using UnityEngine;

public class InputManagement : MonoBehaviour
{
    //Being static, other scripts access this easily, which is very helpful
    public static InputManagement instance;
    
    //Dictionary = a collection of KEY-VALUE pairs
    //Key is the name of the action
    //Value is the KeyCode(the actual physical key)
    public Dictionary<string, KeyCode> keyBindings = new Dictionary<string, KeyCode>()
    {
        {"Move", KeyCode.Space},        // Space bar to move
        {"Left", KeyCode.LeftArrow},    // Left arrow to turn left
        {"Right", KeyCode.RightArrow},  // Right arrow to turn right
        {"Light", KeyCode.F}            // F key for flashlight, NYI
    };
    
    //Awake runs before Start
    void Awake()
    {
        
        if (instance == null) //Allows only one input manager
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); //If another exists, destroy this duplicate
        }
    }
    
    public KeyCode GetKey(string action)//What key is for ...
    {
        if (keyBindings.ContainsKey(action))
        {
            return keyBindings[action]; //Return the KeyCode
        }
        else
        {
            Debug.LogError("Action '" + action + "' not found in key bindings!");
            return KeyCode.None; //Return "no key" if not found
        }
    }
    
    //Change/set a key to something else, useful for the MM
    public void RebindKey(string action, KeyCode newKey)
    {
        if (keyBindings.ContainsKey(action))
        {
            keyBindings[action] = newKey; //Update the dictionary
            Debug.Log(action + " rebound to " + newKey);
        }
        else
        {
            Debug.LogError("Cannot rebind: Action '" + action + "' not found!");
        }
    }
}
