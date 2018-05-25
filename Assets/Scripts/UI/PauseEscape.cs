using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseEscape : MonoBehaviour {

    public Transform canvas;
    public GameObject instructionImage;
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
		
	}
    public void Pause()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            Camera.main.GetComponent<FollowCamara>().enabled = false;
            Cursor.visible = true;
            instructionImage.active = false;
        }
        else
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            Camera.main.GetComponent<FollowCamara>().enabled = true;
        }
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void MenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void InstructionsButton()
    {
        instructionImage.active = true;

    }
}
