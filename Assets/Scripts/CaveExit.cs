using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CaveExit : MonoBehaviour {

    public static bool exitCave = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerStay(Collider other)
    {
       
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Pulsa E para irte de la cueva");


            if (Input.GetKey(KeyCode.E))
            {
                
                SceneManager.LoadScene("Main");
                exitCave = true;

            }
        }

       


    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Quitamos bocadillo para E");
        }
    }
}
