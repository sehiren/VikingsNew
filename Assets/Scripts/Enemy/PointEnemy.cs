using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointEnemy : MonoBehaviour {

    private Vector3 pointA; //Serà la mateixa que la pos inicial del enemic
    private Vector3 pointB;
    bool gotoB = false;
    Transform posicion;

    UnityEngine.AI.NavMeshAgent nav;


    // Use this for initialization
    void Start ()
    {
        posicion = GetComponent<Transform>();
        pointA = posicion.position;//esto vamos a suponer que se pone bien en el Instance 
       // pointB =new  Vector3(0, -3, -8);//este hay que cambiarlo para que lo haga en el mapGenerator o que invoque a la función del map (ya está puesto pero comentado)
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.enabled = true;
      
    }
	
	// Update is called once per frame
	void Update () {

        if (posicion.position.x == pointA[0] && posicion.position.z == pointA[2] && gotoB == false)
        {
            Debug.Log("entra");
            gotoB = true;
            nav.SetDestination(pointB);
        }

        if (posicion.position.x == pointB[0] && posicion.position.z == pointB[2] && gotoB == true)
        {
            Debug.Log("no deberia entrar");
            gotoB = false;
            nav.SetDestination(pointA);
        } 


		
	}

    private void OnTriggerEnter(Collider other)
    {
        
    }

    public void SetPointA(Vector3 pos)
    {
        pointA = pos;
    }

    public void SetPointB(Vector3 pos)
    {
        pointB = pos;
    }
}
