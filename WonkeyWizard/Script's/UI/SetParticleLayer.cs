using UnityEngine;
using System.Collections;

public class SetParticleLayer : MonoBehaviour {

	// Use this for initialization
	private float timer;
    private GameObject parent;
    private Rigidbody2D rigid;
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "NewMagic";
		//Invoke ("DestroyMe", 500);
	}

	void DestroyMe(){
		//Destroy (gameObject);
	}
}
