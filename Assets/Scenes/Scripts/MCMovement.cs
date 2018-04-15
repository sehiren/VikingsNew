using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCMovement : MonoBehaviour {

    // Use this for initialization
    public float speed = 6f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    public static Quaternion newRotation;
    int floorMask;
    float camRayLength = 100f;
    public Transform spawnPoint;

    private void Start()
    {
        spawnPoint = GameObject.Find("Spawn").transform;
        this.transform.position = spawnPoint.position;
        this.transform.rotation = spawnPoint.rotation;
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
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            playerRigidbody.AddForce(0, 90000, 0);
            StartCoroutine(Fall());

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

