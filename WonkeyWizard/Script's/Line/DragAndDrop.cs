using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour {
	
	Vector3 point;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Destroy ();
		
		if (Input.GetMouseButton (0))
		{
			OnMouseDrag();
		}
	}
	
	void OnMouseDrag(){
		point = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		point.z = transform.position.z;
		transform.position = point;
	}
	
	void Destroy(){
		
		if(Input.GetMouseButtonDown(0)){
			Destroy (gameObject);
		}
		
		if(Input.GetMouseButtonUp(0)){
			Destroy (gameObject,  3);
		}
		
	}
	
}
