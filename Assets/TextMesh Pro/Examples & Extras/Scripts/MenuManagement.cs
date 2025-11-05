using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Settings;

    //For various menu-ing with button needs
    public Quaternion target; //Bringing back target, still using for rotation
    bool isMain = true; //Refers to if the current menu is the MAIN menu, to define what the player is swapping to
    //PlayerRotation rotationScript = GetComponent<Rotate>();
    public PlayerRotation Rotation;
    public bool isRotating = false;
    public Slider rotationSpeedSlider; //To update rotation speed
    public TextMeshProUGUI speedText; //For text to show the change

    private bool waitingToShowSettings;
    private bool waitingToShowMain;


    void Start(){
        Settings.SetActive(false);//So you don't see the settings with the main menu lmao
        target = transform.rotation; //initialize target, just like player rotation
        if (rotationSpeedSlider != null && Rotation != null)
        {
            rotationSpeedSlider.value = Rotation.rotationSpeed;
            rotationSpeedSlider.onValueChanged.AddListener(OnRotationSpeedChanged);
        }
    }
    public void LoadGame(){
        SceneManager.LoadScene("PLEASE");
    }

    public void SwapMenu(){
        
        if(isMain){
            MainMenu.SetActive(false); //Disables the main menu
            Rotation.StartRotation(-90f);
            waitingToShowSettings = true;
            isMain = false;

        } //No if-else-if needed, as the bool has only 2 possible values
        else{
            Settings.SetActive(false); //Disables the settings
            Rotation.StartRotation(90f);
            waitingToShowMain = true;
            isMain = true;
        }
    }
    public void OnRotationSpeedChanged(float speed){
        if (Rotation != null)
        {
            Rotation.rotationSpeed = speed;
            if(speedText != null){
                speedText.text = "Rotation Speed: " + speed.ToString("F0");
            }
            Debug.Log("Rotation speed is now:  " + speed);
        }
    }

    

    void Update(){

        Rotation.Rotate();

        if(!Rotation.isRotating){

            if(waitingToShowSettings){
                Settings.SetActive(true);
                waitingToShowSettings = false;
            }
            
            if(waitingToShowMain){
                MainMenu.SetActive(true);
                waitingToShowMain = false;
            }
        }
    }
}
