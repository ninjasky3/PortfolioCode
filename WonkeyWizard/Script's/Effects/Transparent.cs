using UnityEngine;
using System.Collections;

public class Transparent : MonoBehaviour {

	// Use this for initialization
    private float transparency;
    private Color thisColor;
	void Start () {


        thisColor = this.GetComponent<SpriteRenderer>().color;
        thisColor.a = 0.3f;
        this.GetComponent<SpriteRenderer>().color = thisColor;
	}

}
