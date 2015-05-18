using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {

	public GameObject player;
	public Transform[] points;
	public bool[] Moving;
	public int MaxPoints;
	public int num ;
	private float Speed = 0.1f;
	private float BackSpeed = 0.01f;
	
	void Start (){
		
		Moving [0] = true;
	}
	void FixedUpdate () {
		
		if (num == MaxPoints) {
			num = 0;
			Moving [0] = true;
		}
		if (transform.position == points [num].position) {
			Moving [num] = false;
			num = (num  == 1 ? 0 : num + 1);
			Moving [num] = true;
		}
		
		
		//move platform to point A or B
		if (Moving [num]) {
			transform.position = Vector3.MoveTowards (transform.position, points [num].position, Speed);
		}
		if (Moving [1]) {
			Speed = BackSpeed;
		}
		else if(Moving[0]){
			Speed = 0.1f;
		}
		
	}
}
