using UnityEngine;
using System.Collections;

public class DestroyMe : MonoBehaviour {

	// Use this for initialization

    public float timer = 3f;
	void Start () {
        Invoke("DestroyThisObject", timer);
	}

    void DestroyThisObject()
    {
        Destroy(gameObject);
    }
}
