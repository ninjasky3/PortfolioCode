using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI() {
		if (GUI.Button (new Rect (Screen.width/2 - 50, Screen.height/2 - 120, 200, 50), "Start"))
			Application.LoadLevel ("ParkourLevel1");
		
		if (GUI.Button (new Rect (Screen.width/2 - 50, Screen.height/2 - 25, 200, 30), "Exit"))
						Application.Quit ();
		
	}
}
