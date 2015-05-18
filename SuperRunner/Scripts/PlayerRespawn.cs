using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour {

	public Transform respawnPoint;
	public GameObject player;
	public GameObject checkpointMover;
	// Use this for initialization	
	void Start () {
	
	}

	void OnCollisionEnter(Collision col){
		if(col.collider.tag == "KillingObject"){
			player.transform.position = respawnPoint.position;
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.collider.tag == "KillingObject"){
			player.transform.position = respawnPoint.position;
		}
		if(col.collider.tag == "CheckPoint"){
			respawnPoint.transform.position = col.transform.position;
			Debug.Log(col.transform.position);
		}
	}



}
