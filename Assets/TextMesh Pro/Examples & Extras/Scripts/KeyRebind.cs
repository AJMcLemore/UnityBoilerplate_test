using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyRebind : MonoBehaviour
{
    bool isWaitingForKey = false; //Whether to wait for an input or not
    public string actionName; //What key is being rebound
    public TextMeshProUGUI buttonText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartRebinding() //This should probably be first, but idrc
    {
        isWaitingForKey = true;
        buttonText.text = "..."; //Not a lotta space
    }

    bool IsValidKey(KeyCode key){
        // Exclude mouse buttons
        if(key >= KeyCode.Mouse0 && key <= KeyCode.Mouse6){
            Debug.Log("Mouse buttons not allowed");
            return false;
        }
        
        // Exclude Escape (reserved for cancelling)
        if (key == KeyCode.Escape){
            return false;
        }
        
        // Key is valid!
        return true;
    }

    void UpdateButtonText(){
        KeyCode currentKey = InputManagement.instance.GetKey(actionName);
        buttonText.text = currentKey.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaitingForKey)//For rebinding
        {
            if (Input.anyKeyDown)//If any key is pressed
            {
                //Loop through all possibilities, until we find a match. seems inefficient tbh
                foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(key))
                    {
                        //Filter Invalid keys, for now mouse buttons
                        if (IsValidKey(key))
                        {
                            //Yell at the dictionary to be updated
                            InputManagement.instance.RebindKey(actionName, key);
                            
                            //Stop waiting
                            isWaitingForKey = false;
                            
                            //Update bind button to show new key
                            UpdateButtonText();
                            break;//Get outta here
                        }
                    }
                }
            }
            
            //Escape "works", but just to cancel
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isWaitingForKey = false;
                UpdateButtonText();
                Debug.Log("Rebinding cancelled");
            }
        }
    }
}
