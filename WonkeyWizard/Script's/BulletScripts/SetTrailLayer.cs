using UnityEngine;
using System.Collections;

public class SetTrailLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<TrailRenderer>().sortingLayerName = "TrailRenderer";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
