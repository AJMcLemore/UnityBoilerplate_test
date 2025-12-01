using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PSlider : MonoBehaviour
{
    public Slider Puzzle;
    public float Match;
    private Image sliderFillImage;
    private Color normalColor = Color.white;
    private Color matchColor = Color.green;
    private bool Matched = false;
    public GameObject PuzzleMenu;
    private bool MatchMade = false;
    private bool PuzzleComplete = false;
    public PuzzleTransition transition;

    // Start is called before the first frame update
    void Start()
    {
        MatchMade = false;
        PuzzleMenu.SetActive(false);
        Puzzle.onValueChanged.AddListener(OnSliderValueChanged);
        if (sliderFillImage != null)
        {
            sliderFillImage.color = normalColor;
        }
    }
    void OnSliderValueChanged(float SN){
        if(SN == Match || SN < (Match + 1) || SN > (Match - 1)){
            if(!Matched){
                Matched = true;
                OnSliderMatched();
            }
            else if (Matched){
                Matched = false;
                OnSliderUnmatched();
            }
        }
    }

    void OnSliderMatched(){
        if (sliderFillImage != null)
        {
            sliderFillImage.color = matchColor;
            PuzzleComplete = true;
        }
    }
    void OnSliderUnmatched(){
        // Change color back to normal
        if (sliderFillImage != null)
        {
            sliderFillImage.color = normalColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!MatchMade){
            //Match = ((Time.deltaTime % 20) + 1);
            MatchMade = true;
        }
        if(PuzzleComplete){
            transition.tableViewAway();
        }
    }
}
