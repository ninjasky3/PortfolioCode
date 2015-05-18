using UnityEngine;
using System.Collections;

public class LevelCompleted : MonoBehaviour {


    public GameObject childParticle;
	// Use this for initialization
	void Awake () {
        childParticle.SetActive(true);
	}
	

}
