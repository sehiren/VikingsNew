using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderHealth : MonoBehaviour {

    public int currentHealth;
    public int maxHealth;
    public int damageTaken;
    bool isDead;
    GameObject handSword;


    Animator anim;
    SphereCollider collider; 

    // Use this for initialization
    void Start ()
    {
        maxHealth = 50;
        currentHealth = maxHealth;
        isDead = false;
        damageTaken = 10;

        anim = GetComponent<Animator>();
        collider = GetComponent<SphereCollider>();
        handSword = GameObject.FindGameObjectWithTag("hand_sword");


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int amount)//la llamará el vikingo cuando le de un espadazo y le toque el collider 
    {
        Debug.Log("HOLA1");

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

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == handSword.transform.GetChild(0).gameObject)
        {
            Debug.Log("HOLA");
            TakeDamage(damageTaken);
        }
    }


    


}
