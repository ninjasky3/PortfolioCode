using UnityEngine;
using System.Collections;

public class Trooper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Destroy ();
	}

		void Destroy(){
			if(transform.position.y <= 0F){
				Destroy(gameObject);
			}
		}
}
