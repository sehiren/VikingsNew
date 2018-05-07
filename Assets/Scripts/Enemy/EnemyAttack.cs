using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject player;
    bool playerInRange;
    float timer;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
            anim.SetBool("isAttack", false);
        }
    }

    // Update is called once per frame
    void Update () {

        timer += Time.deltaTime;

        if (playerInRange)
        {
            Attack();
            
        }
        else
        {
            anim.SetBool("isAttack", false);
        }
	}

    void Attack()
    {
        timer = 0f;
        anim.SetBool("isAttack", true);

        /*
         *     if(playerHealth.currentHealth > 0)
        {
            // ... damage the player.
            playerHealth.TakeDamage (attackDamage);
        }
         */
    }
}
