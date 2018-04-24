using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    GameObject player;


	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other == player)
        {
            ActiveFungus();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other == player)
        {

            DesactiveFungus();
        }
    }

    void DesactiveFungus()
    {
       
    }


    // Update is called once per frame
    void Update () {
		
	}

    void ActiveFungus()
    {

    }
}
