using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToPlayer : MonoBehaviour {

    public GameObject player;
    GameObject villager;
    Vector3 playerPosition;
    Vector3 npcPosition;
    Vector3 delta;
    Quaternion rotation;

    // Use this for initialization
    void Start () {

        playerPosition = player.transform.position;
        npcPosition = this.transform.position;
        delta = new Vector3(playerPosition.x - npcPosition.x, 0.0f, playerPosition.z - npcPosition.z);

        rotation = Quaternion.LookRotation(delta);

    }
	
	// Update is called once per frame
	void Update () {
        playerPosition = player.transform.position;
        npcPosition = this.transform.position;
        delta = new Vector3(playerPosition.x - npcPosition.x, 0.0f, playerPosition.z - npcPosition.z);

        rotation = Quaternion.LookRotation(delta);
        this.transform.rotation = rotation;
	}
}
