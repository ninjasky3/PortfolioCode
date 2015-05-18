using UnityEngine;
using System.Collections;

public class Trampoline : MonoBehaviour {

	public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.collider.gameObject == player) {
			Debug.Log(player);
			player.rigidbody.AddForce(new Vector3(0,100,0));
				}
	
	}

	void OnTriggerStay(Collider col){
		if (col.collider.gameObject == player) {
			player.rigidbody.AddForce(new Vector3(0,150,0));
		}
	}
}
