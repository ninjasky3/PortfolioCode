﻿using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Magic";
	}
}
