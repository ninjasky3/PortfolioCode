using UnityEngine;
using System.Collections;

public class SetParent : MonoBehaviour {

	// Use this for initialization
    public string parentName;
	void Start () {
        GameObject parentobject = GameObject.FindGameObjectWithTag(parentName);
        transform.parent = parentobject.transform ;
	}
}
