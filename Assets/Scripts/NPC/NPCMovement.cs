using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour {

    public Transform puntoA;
    public Transform puntoB;

    NavMeshAgent nav;
    Animator anim;

    public bool goingTowardsA = true;
    public bool goingTowardsB = false;

    bool checkingIfReached = true;

    // Use this for initialization
    void Start()
    {

        

        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        //nav.SetDestination(puntoA.position);
        anim.SetBool("Run", true);
        nav.SetDestination(puntoA.position);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(nav.destination == puntoB.position )
        {
            nav.SetDestination(puntoA.position);
            anim.SetBool("Run", true);
            if(nav.remainingDistance < 5.0f)
            {

            }
        }
       

       if(nav.destination == puntoA.position && nav.remainingDistance < 5.0f)
        {
            nav.SetDestination(puntoB.position);
            anim.SetBool("Run", true);
        }*/
        Debug.Log(nav.velocity.magnitude);

        if (ReachedNavTarget())
        {
            Debug.Log("Reached");
            if (goingTowardsA)
            {
                goingTowardsA = false; goingTowardsB = true;

                GoToPoint(puntoB);
            }
            else if (goingTowardsB)
            {
                goingTowardsB = false; goingTowardsA = true;
                GoToPoint(puntoA);
            }
        }
    }

    void ResetCheck()
    {
        checkingIfReached = true;
    }

    void GoToPoint(Transform which)
    {
        checkingIfReached = false;
        Invoke("ResetCheck", 1f);
        nav.SetDestination(which.position);
    }

    bool ReachedNavTarget( )
    {
        if (checkingIfReached)
            return nav.velocity.magnitude == 0f;
        else
            return false;
    }
}
