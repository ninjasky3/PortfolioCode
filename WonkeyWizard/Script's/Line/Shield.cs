using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	
	public ParticleSystem shield;
	public Transform target;
	
	private float distance;
	public Transform origin;
	public Transform destination;
	
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().sortingLayerName = "Main";
		GetComponent<ParticleSystem>().startSpeed = 5;
	}
	
	// Update is called once per frame
	void Update () {
		
		destination = GameObject.Find("Destination(Clone)").transform;
		distance = Vector3.Distance(transform.position, destination.position);
		target = GameObject.Find("Destination(Clone)").transform;
		transform.LookAt (target);
		GetComponent<ParticleSystem>().startLifetime = distance / 5;
		
	}
}
