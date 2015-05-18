
using UnityEngine;
using System.Collections;

public class ObjectTeleport : MonoBehaviour {
	//public
	public Transform PointA;
	public Transform PointB;
	public float Speed = 1;
	//private
	private bool MovingToB = false;
	void Start(){
	}
	
	void FixedUpdate () {
		// switch direction
		if(transform.position == PointA.position){	MovingToB = true;	}
		if(transform.position == PointB.position){	MovingToB = false;	}
		
		//move platform to point A or B
		if(MovingToB){
			transform.position = Vector3.MoveTowards(transform.position, PointB.position, Speed);
		}else{
			//teleports
			transform.position = PointA.position;
		}
	}
}
