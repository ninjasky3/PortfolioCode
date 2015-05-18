
using UnityEngine;
using System.Collections;

public class OriginScript : MonoBehaviour {
	


	public void DestroyMe(){
		Invoke ("Destroyed", 0.1F);
	}
	
	private void Destroyed(){
		Destroy (gameObject);
	}
}