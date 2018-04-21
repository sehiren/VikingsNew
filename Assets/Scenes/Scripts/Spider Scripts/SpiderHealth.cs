using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderHealth : MonoBehaviour {

    public int currentHealth;
    public int maxHealth;
    bool isDead;

    Animator anim;
    SphereCollider collider; 

    // Use this for initialization
    void Start ()
    {
        maxHealth = 50;
        currentHealth = maxHealth;
        isDead = false;

        anim = GetComponent<Animator>();
        collider = GetComponent<SphereCollider>();
        

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int amount, Vector3 hitPoint)//la llamará el vikingo cuando le de un espadazo y le toque el collider 
    {

        if (isDead)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        collider.isTrigger = true;

        anim.SetTrigger("Dead");
        Destroy(gameObject, 4f);

    }


}
