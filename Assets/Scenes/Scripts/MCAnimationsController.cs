using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCAnimationsController : MonoBehaviour {

    Animator player_animator;
    GameObject player;
    GameObject backSword;
    GameObject handSword;
    bool armed;

	// Use this for initialization
	void Start ()
    {
        player_animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        backSword = GameObject.FindGameObjectWithTag("back_sword");
        handSword = GameObject.FindGameObjectWithTag("hand_sword");
        armed = false;

	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) )
        {
            player_animator.SetBool("Run", true);
            player_animator.SetBool("Backwards", false);

        }

        if (Input.GetKey(KeyCode.S))
        {
            player_animator.SetBool("Run", false);
            player_animator.SetBool("Backwards", true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!armed)
            {
                
                player_animator.SetLayerWeight(1, 1.0f);
                player_animator.SetBool("Attack", true);
               
               
            }
            else if (armed)
            {
               
                player_animator.SetLayerWeight(2, 1.0f);
                player_animator.SetBool("Unattack", true);
               
            }
        }
        if (player_animator.GetAnimatorTransitionInfo(1).IsName("Sword -> Base"))
        {
            player_animator.SetLayerWeight(1, 0.0f);
            player_animator.SetBool("Attack", false);
        }
        if (player_animator.GetAnimatorTransitionInfo(2).IsName("Sword -> Base"))
        {
            player_animator.SetLayerWeight(2, 0.0f);
            player_animator.SetBool("Unattack", false);
        }
        else if (Input.anyKey == false)
        {
            player_animator.SetBool("Run", false);
            player_animator.SetBool("Backwards", false);
        }
    }

    void Sword() {
        if (!armed)
        {
            armed = true;
            backSword.GetComponent<MeshRenderer>().enabled = false;
            handSword.GetComponent<MeshRenderer>().enabled = true;
        }else if (armed)
        {
            armed = false;
            backSword.GetComponent<MeshRenderer>().enabled = true;
            handSword.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
