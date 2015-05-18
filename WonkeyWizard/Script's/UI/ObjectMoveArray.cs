using UnityEngine;
using System.Collections;

public class ObjectMoveArray : MonoBehaviour {

	public GameObject player;
	public Transform[] points;
	public bool[] Moving;
	public int MaxPoints;
	public int num ;
	public float Speed;

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
						num = (num  == 1 ? 1 : num + 1);
						Moving [num] = true;
				}
		
				//move platform to point A or B
				if (Moving [num]) {
						transform.position = Vector3.MoveTowards (transform.position, points [num].position, Speed);
				}
			
		}
}