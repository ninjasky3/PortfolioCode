using UnityEngine;
using System.Collections;

public class PlayerPlatformCollision : MonoBehaviour {

	private Transform FirstTransform;
	void Start(){
		FirstTransform = transform;

		}

	void OnCollisionStay(Collision col){

	
		if (col.collider.tag == "MovingObjects") {
			transform.position = col.transform.position;
		
				}


	}

	void OnCollisionExit(Collision col){
		
		
		if (col.collider.tag == "MovingObjects") {
			Debug.Log(transform.parent);
			transform.parent = null;
			
		}
		
		
	}
}
