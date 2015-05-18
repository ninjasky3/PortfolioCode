using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {


	private float speed = 0.2f;

	// Move the cloud
	void Start () {
        GetComponent<Rigidbody2D>().velocity = (Vector3.left * speed);
	}
}
