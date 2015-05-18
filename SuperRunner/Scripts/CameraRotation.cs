using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {

	public float RotationSpeed;
	private float timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		timer += 1 * Time.deltaTime;
		if(timer <= 7){
		transform.Rotate(new Vector3(0,RotationSpeed,0));
		}
		Debug.Log(timer);


	}
}
