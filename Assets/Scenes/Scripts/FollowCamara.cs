using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamara : MonoBehaviour {

    public float height = 1f;
    public float distance = 2f;
    private float turnSpeed = 8f;
    private Vector3 offsetX;
    private Vector3 offsetY;
    public Transform player;


    void Start()
    {

        offsetX = new Vector3(0, height, distance);

    }

    void LateUpdate()
    {
        offsetX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offsetX;

        transform.position = player.position + offsetX;
        transform.LookAt(player.position);
    }
}

