using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public Transform pointA;
	public Transform pointB;
	private bool Moving;
	private bool Moving2;
	public float Speed = 0.05f;
	
	void Start (){
		
		Moving = true;
	}
	void FixedUpdate () {
		
		Debug.Log(Moving2);
		if (transform.position == pointA.position) {
			Moving2 = true;
			Moving = false;
		}
		if (transform.position == pointB.position) {
			Moving = false;
		}
		
		
		//move platform to point A or B
		if (Moving == true) {
			transform.position = Vector3.MoveTowards (transform.position, pointA.position, Speed);
		}
	
		else{

		}
		if (Moving2 == true) {
			transform.position = Vector3.MoveTowards (transform.position, pointB.position, Speed);
		}
		
	}

}
