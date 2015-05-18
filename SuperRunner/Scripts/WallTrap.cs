using UnityEngine;
using System.Collections;

public class WallTrap : MonoBehaviour {
	public Transform beginPoint;
	public Transform endPoint;
	private float speed = 0f;
	private float startTime;
	private float journeyLength;
	private bool endPosition = false;

	void Start(){


	}

	void OnTriggerEnter(Collider col){
		if(col.collider.tag == "Player"){
			speed = 10.5f;
			startTime = Time.time;

		}
	}
	void OnTriggerExit(Collider col){
	
	}

	void FixedUpdate(){

		if(endPosition == false){

			float distCovered = (Time.time - startTime) * speed ;
			journeyLength = Vector3.Distance(beginPoint.position, endPoint.position);
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp(beginPoint.position, endPoint.position, fracJourney);
		}

		if(endPoint.position == transform.position){
			endPosition = true;
		}

	}



}


