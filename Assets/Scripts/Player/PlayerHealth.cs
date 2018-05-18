using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int playerHealth;
    public int currentHealth;
    public Slider m_Slider;
    Animator anim;
    bool isDead;
    float damTimer;
	// Use this for initialization
	void Start () {
        playerHealth = 100;
        currentHealth = playerHealth;
        anim = GetComponent<Animator>();
        damTimer = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (playerHealth <= 0) isDead = true;
        damTimer += Time.deltaTime;
		
	}

    public void TakeDamage(int amount)//la llamará el enemigo al atacar  
    {

        if (isDead)
            return;
        if (damTimer > 3)
        {

            currentHealth -= amount;
            damTimer = 1;
            m_Slider.value = currentHealth;
        }
        
        if (currentHealth <= 0)
        {
            Death();
            damTimer = 1;
        }
    }
    public void Death()
    {
        anim.SetBool("Dead", true);
    }
}
