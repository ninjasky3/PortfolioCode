using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

	public float ratio;
	private float up = -1;



	void Start(){
	}
	void Update(){
		ratio += Time.deltaTime;
		Color col = GetComponent<Renderer>().material.color;
		col.a = Mathf.Lerp (-col.a,up,ratio);
		GetComponent<Renderer>().material.color = col;
		up += 0.05f;

	}
}