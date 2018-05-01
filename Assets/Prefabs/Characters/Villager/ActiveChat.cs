using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveChat: MonoBehaviour {

    GameObject player;
    public GameObject dialog;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown("z"))
            {
                dialog.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        dialog.SetActive(false);
    }
}
