using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {
	private PlayerData pData;
	private GameObject player;

	private int testint;
	public GameObject[] side1;
	public GameObject[] side2;
	// Use this for initialization
	void Start () {
		testint = 1;
	pData = GameObject.Find("MainCube").GetComponent<PlayerData>();
		switch(pData.playerType){
		case 1:
			GameObject p1 = Instantiate(Resources.Load("Players/PlayerOne"),transform.position,Quaternion.identity) as GameObject;
			p1.name = "PlayerOne";
			p1.transform.parent = gameObject.transform;
			side1[0] = GameObject.Find("Ring1");
			side1[1] = GameObject.Find("Ring2");
			side1[2] = GameObject.Find("Ring3");
			side2[0] = GameObject.Find("Ball1");
			side2[1] = GameObject.Find("Ball2");
			side2[2] = GameObject.Find("Bal");
			break;
		case 2:
			GameObject p2 = Instantiate(Resources.Load("Players/PlayerTwo"),transform.position,Quaternion.identity) as GameObject;
			p2.name = "PlayerTwo";
			p2.transform.parent = gameObject.transform;
			side1[0] = GameObject.Find("Ring1");
			side1[1] = GameObject.Find("Ring2");
			side1[2] = GameObject.Find("Ring3");
			side2[0] = GameObject.Find("Ball1");
			side2[1] = GameObject.Find("Ball2");
			side2[2] = GameObject.Find("Ball3");
			break;
		case 3:
			GameObject p3 = Instantiate(Resources.Load("Players/PlayerThree"),transform.position,Quaternion.identity) as GameObject;
			p3.name = "PlayerThree";
			p3.transform.parent = gameObject.transform;
			side1[0] = GameObject.Find("Ring1");
			side1[1] = GameObject.Find("Ring2");
			side1[2] = GameObject.Find("Ring3");
			side2[0] = GameObject.Find("Ball1");
			side2[1] = GameObject.Find("Ball2");
			side2[2] = GameObject.Find("Ball3");
			break;
		}
		for(int c = 0; c < side1.Length; c++){
			if(side1[c] != null){
				side1[c].renderer.material.color = pData.mainColor;
			}
		}
		for(int c = 0; c < side2.Length; c++){
			if(side2[c] != null){
				side2[c].renderer.material.color = pData.secondaryColor;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
