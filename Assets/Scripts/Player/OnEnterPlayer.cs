using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnEnterPlayer : MonoBehaviour {

    public string levelToLoad;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Fuera");
    }
}
