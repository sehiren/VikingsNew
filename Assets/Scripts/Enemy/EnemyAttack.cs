using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    bool playerInRange;
    float timer;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
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


        
        if (playerInRange)
        {
            timer += Time.deltaTime;
            Attack();
            if (timer >= 2f) {
                anim.SetBool("isAttack", false);
                //this.GetComponentInChildren<Collider>().enabled = false;
                timer = 0;
            }

        }
        else
        {
            anim.SetBool("isAttack", false);
            //this.GetComponentInChildren<Collider>().enabled = false;
            timer =0;
        }
	}

    void Attack()
    {
        if (timer > 0.15)
        {
            anim.SetBool("isAttack", true);
           
        }

        if (playerHealth.currentHealth > 0 )
        {
            // ... damage the player.
            playerHealth.TakeDamage(attackDamage);
        }

    }
}
