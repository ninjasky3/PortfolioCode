using UnityEngine;
using System.Collections;

public class Camera_Shake : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Invoke("Left",0.1F);
	}
	
	// Update is called once per frame
	void Update () {
		Shake();
	}

	void Shake(){
		if(transform.position.x == 0.1F ){
			Left();
		}

		if(transform.position.x == -0.1F ){
			Right();
		}
	
	}

	void Left(){
		transform.Translate(Vector2.right * Time.deltaTime);
//		transform.position = new Vector3(Mathf.PingPong(Time.time, -0.03F), transform.position.y, transform.position.z);
//		Invoke("Right",0.0001F);
		}

	void Right(){
		transform.Translate(Vector2.right * -Time.deltaTime);
//		transform.position = new Vector3(Mathf.PingPong(Time.time, 0.03F), transform.position.y, transform.position.z);
//		Invoke("Left",0.0001F);
	}
}
