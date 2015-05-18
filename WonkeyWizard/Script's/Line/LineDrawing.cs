using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof (Camera))]
public class LineDrawing : MonoBehaviour
{
	private LineRenderer lineRenderer;
	private float counter;
	public float distance;
	
	public Transform origin;
	public Transform destination;
	
	public float lineDrawSpeed;
	
	
	// Use this for initialization
	void Start () {
		
		if(!(lineRenderer = GetComponent<LineRenderer>())){
			lineRenderer = gameObject.AddComponent<LineRenderer>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		lineRenderer.sortingLayerName = "Magic";
		Destroy ();
		
		if(Input.GetMouseButtonUp(0)){
			InvokeRepeating("Drawshield", 0.1F,0.01F);
		}
		
	}
	
	void Drawshield(){
		
		//set origin position
		destination = GameObject.Find ("Destination(Clone)").transform;
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.SetPosition (0, origin.position);
		lineRenderer.SetWidth (0.2F, 0.2F);
		distance = Vector3.Distance (origin.position, destination.position);
		
		//drawing the line between the 2 points
		if (counter < distance) {
			counter += 0.1F / lineDrawSpeed;
			float x = Mathf.Lerp (0, distance, counter);
			Vector3 pointA = origin.position;
			Vector3 pointB = destination.position;
			Vector3 pointAlongLine = x * Vector3.Normalize (pointB - pointA) + pointA;
			lineRenderer.SetPosition (1, pointAlongLine);			
		}
		
	}
	
	void Destroy(){
		//destroy origin
		if(Input.GetMouseButtonDown(0)){
			Destroy(transform.parent.gameObject);
		}
		//destroy destination
		if(Input.GetMouseButtonUp(0)){
			Destroy(transform.parent.gameObject,3);
		}
		
	}
	
}

