using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MCMovement : MonoBehaviour {

    // Use this for initialization
    public float speed = 10.0f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    public static Quaternion newRotation;
    int floorMask;
    float camRayLength = 100f;
    public Transform spawnCave;

    private void Start()
    {


        if (CaveExit.exitCave)
        {
            Debug.Log("HA SALIDO DE LA CUEVA");
           // this.GetComponent<NavMeshAgent>().enabled = false;
           // this.transform.position = spawnCave.position;
           // this.GetComponent<NavMeshAgent>().enabled = true;

            this.GetComponent<NavMeshAgent>().Warp (spawnCave.position);

            CaveExit.exitCave = false;
           
        }

    }

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.useGravity = true;
        Physics.gravity = new Vector3(0, -900f, 0);
    }

    private void FixedUpdate()
    {


        Move();
        Turning();
        Animating();
    }

    void Move()

    {
        if (!anim.GetBool("Attacking"))
        {
            if (Input.GetKey(KeyCode.S)) speed = 7.0f;
            else speed = 10.0f;
            /* var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
             var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;



             transform.Translate(x, 0, 0);
             transform.Translate(0, 0, z);

             */
            if (Input.GetKey(KeyCode.W))
            {

                transform.position += transform.forward * Time.deltaTime * speed;
            }

            //if (Input.GetKey(KeyCode.D))
            //{
            //    transform.position += transform.right * Time.deltaTime * speed;
            //}

            if (Input.GetKey(KeyCode.S))
            {

                transform.position -= transform.forward * Time.deltaTime * speed * 0.5f;
            }

            //if (Input.GetKey(KeyCode.A))
            //{
            //    transform.position -= transform.right * Time.deltaTime * speed;
            //}
            /*if (Input.GetKey(KeyCode.Space))
            {
                playerRigidbody.AddForce(0, 90000, 0);
                StartCoroutine(Fall());

            }*/
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(0.1f);
        playerRigidbody.AddForce(0, -90000, 0);
    }


    void Turning()
    {

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))//out guarda la info de la funcio en floorHit
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating()
    {
        /*bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);*/
    }
}

