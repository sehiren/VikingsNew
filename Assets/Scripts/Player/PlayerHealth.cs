using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int playerHealth;
    public int currentHealth;
    Animator anim;
    bool isDead;
	// Use this for initialization
	void Start () {
        playerHealth = 100;
        currentHealth = playerHealth;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerHealth <= 0) isDead = true;
		
	}

    public void TakeDamage(int amount)//la llamará el enemigo al atacar  
    {

        if (isDead)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        anim.SetBool("Dead", true);
    }
}
