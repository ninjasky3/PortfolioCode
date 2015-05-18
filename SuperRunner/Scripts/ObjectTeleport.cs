using UnityEngine;
using System.Collections;

public class ObjectTeleport : MonoBehaviour {

	public Transform pointA;
	public Transform pointB;
	private bool Moving;
	public float Speed = 0.05f;
	
	void Start (){
		
		Moving = true;
	}
	void FixedUpdate () {
		
	
		if (transform.position == pointA.position) {
			Moving = true;
		}
		if (transform.position == pointB.position) {
			Moving = false;
		}
		
		
		//move platform to point A or B
		if (Moving == true) {
			transform.position = Vector3.MoveTowards (transform.position, pointB.position, Speed);
		}
		else{
			transform.position = pointA.position;
		}
		
	}
}
