using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour {
    public Canvas mainCanvas;
    public Canvas optionCanvas;
    // Use this for initialization
    void Start() {

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

    }

    public void BackButton()
    {
        mainCanvas.enabled = true;
        optionCanvas.enabled = false;

    }
}
