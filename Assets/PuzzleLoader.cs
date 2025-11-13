using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PuzzleLoader : MonoBehaviour
{
    public string sceneToLoad = "";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ButtonPuzzle"))
        {
            SceneManager.LoadScene(1);
        }
        else if(other.CompareTag("ExitTrigger"))
        {
            SceneManager.LoadScene(2);
        }
        else if(other.CompareTag("FreebieTrigger"))
        {
            SceneManager.LoadScene(3);
        }
        else if(other.CompareTag("PicturePuzzle"))
        {
            SceneManager.LoadScene(5);
        }
        else if(other.CompareTag("NumberPuzzle"))
        {
            SceneManager.LoadScene(4);
        }
        else
        {
            Debug.Log("Scene not found");
        }
    }
}
