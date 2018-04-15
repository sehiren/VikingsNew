using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCAnimationsController : MonoBehaviour {

    Animator player_animator;
    GameObject player;
    GameObject hipHorn;
    GameObject handHorn;

	// Use this for initialization
	void Start ()
    {
        player_animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        hipHorn = GameObject.FindGameObjectWithTag("hip_horn");
        handHorn = GameObject.FindGameObjectWithTag("hand_horn");

	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) )
        {
            player_animator.SetBool("Run", true);

        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
        if (Input.GetMouseButtonDown(0))
        {
            player_animator.SetBool("Attack",true);
            
        }
        else if (Input.anyKey == false)
        {
            player_animator.SetBool("Run", false);
        }
    }

    void Sword() {
        //player_animator.SetBool("Run", true);
        hipHorn.GetComponent<MeshRenderer>().enabled= false;
        handHorn.GetComponent<MeshRenderer>().enabled = true;

    }
}
