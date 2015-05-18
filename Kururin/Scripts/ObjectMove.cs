
using UnityEngine;
using System.Collections;

public class ObjectMove : MonoBehaviour {
	//public
	public Transform PointA;
	public Transform PointB;
	public Transform PointC;
	public Transform PointD;
	public Transform PointE;
	public Transform PointF;
	public float Speed = 1;
	//private
	private bool startgame = true;
	private bool MovingToB = false;
	private bool MovingToC = false;
	private bool MovingToD = false;
	private bool MovingToE = false;
	private bool MovingToF = false;
	private bool direction;
	void Start(){

	}
	// switch direction
	void FixedUpdate () {
		if(transform.position == PointA.position){	startgame = false;	}
		if(transform.position == PointA.position){	MovingToB = true;	}
		if(transform.position == PointB.position){	MovingToB = false; 	}
		if(transform.position == PointB.position){	MovingToC = true;	}
		if(transform.position == PointC.position){	MovingToC = false;	}
		if(transform.position == PointC.position){	MovingToD = true;	}
		if(transform.position == PointD.position){	MovingToD = false;	}
		if(transform.position == PointD.position){	MovingToE = true;	}
		if(transform.position == PointE.position){	MovingToE = false;	}
		if(transform.position == PointE.position){	MovingToF = true;	}
		if(transform.position == PointF.position){	MovingToF = false;	}
		if(transform.position == PointF.position){	MovingToB = true;	}

		//move platform to point A or B
		if(startgame){
			transform.position = Vector3.MoveTowards(transform.position, PointA.position, Speed);
		}
		if(MovingToB){
			transform.position = Vector3.MoveTowards(transform.position, PointB.position, Speed);
		}
		if(MovingToC){
			transform.position = Vector3.MoveTowards(transform.position, PointC.position, Speed);
		}
		if(MovingToD){
			transform.position = Vector3.MoveTowards(transform.position, PointD.position, Speed);
		}
		if(MovingToE){
			transform.position = Vector3.MoveTowards(transform.position, PointE.position, Speed);
		}
		if(MovingToF){
			transform.position = Vector3.MoveTowards(transform.position, PointF.position, Speed);
		}
	}

}
