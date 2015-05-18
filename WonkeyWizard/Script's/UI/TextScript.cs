using UnityEngine;
using System.Collections;

public class TextScript : MonoBehaviour {

	// Use this for initialization
	TextMesh thistext;
	void Start () {
		thistext = gameObject.GetComponent<TextMesh>();

	}
	
	// Update is called once per frame
	void Update () {
	thistext.GetComponent<Renderer>().sortingLayerName = "Magic";

	}
}
