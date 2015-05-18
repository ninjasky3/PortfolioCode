using UnityEngine;
using System.Collections;

  [RequireComponent(typeof(Rigidbody2D))]

public class BirdScripts : MonoBehaviour {



      private Rigidbody2D rigidbody;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        rigidbody.AddForce(new Vector2(Random.Range(50 * 2 , -50), -Random.Range(50 * 2, -50)));
	}
}
