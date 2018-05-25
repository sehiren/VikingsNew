using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour {
    public Canvas mainCanvas;
    public Canvas optionCanvas;
    public Canvas instructionCanvas;
    public GameObject instructionImage;
    public Canvas creditsCanvas;
    public GameObject creditsImage;
    // Use this for initialization
    void Start() {
        instructionCanvas.enabled = false;
        mainCanvas.enabled = true;
        optionCanvas.enabled = false;
        instructionImage.active = false;
        creditsCanvas.enabled = false;
        creditsImage.active = false;
    }

    // Update is called once per frame
    void Update() {

    }

    public void NewGame(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OptionsButton(){

        mainCanvas.enabled = false;
        optionCanvas.enabled = true;
        instructionCanvas.enabled = false;
        instructionImage.active = false;
        creditsCanvas.enabled = false;
        creditsImage.active = false;

    }

    public void BackButton()
    {
        mainCanvas.enabled = true;
        optionCanvas.enabled = false;
        instructionCanvas.enabled = false;
        instructionImage.active = false;
        creditsCanvas.enabled = false;
        creditsImage.active = false;

    }

    public void InstructionsButton()
    {
        mainCanvas.enabled = false;
        optionCanvas.enabled = false;
        instructionCanvas.enabled = true;
        instructionImage.active = true;
        creditsCanvas.enabled = false;
        creditsImage.active = false;
    }

    public void CreditsButton()
    {

        mainCanvas.enabled = false;
        optionCanvas.enabled = false;
        instructionCanvas.enabled = false;
        instructionImage.active = false;
        creditsCanvas.enabled = true;
        creditsImage.active = true;
    }
}
