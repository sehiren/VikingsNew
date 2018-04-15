using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEscaleras : MonoBehaviour {

    // Use this for initialization
    public static bool entrar = false;

    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            entrar = true;

        }
            
        
    }

}
