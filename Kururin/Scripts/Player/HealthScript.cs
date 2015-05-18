using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {
	public Texture healthIcon;
	public Texture nohealthIcon;
	private int health;
	public PlayerMovement playerscript;
	private bool heart1,heart2,heart3;
	// Use this for initialization
	void Start () {
		playerscript = GetComponent<PlayerMovement>();

	
	}
	void Update() {
		health = playerscript.playerhealth;
		Debug.Log(health);
		if (health == 3){
			heart1 = true;
			heart2 = true;
			heart3 = true;
		}

		if (health == 2){
			heart1 = true;
			heart2 = true;
			heart3 = false;
		}

		if (health == 1){
			heart1 = true;
			heart2 = false;
			heart3 = false;
		}

		if (health == 0){
			heart1 = false;
			heart2 = false;
			heart3 = false;
		}


	}
	//health icons worden getekent
	void OnGUI(){
		if(!playerscript.leveldone){
			if(heart1){
				GUI.DrawTexture(new Rect(150,0,50,50),healthIcon);
			}
			else{
				GUI.DrawTexture(new Rect(150,0,50,50),nohealthIcon);
			}
			if(heart2){
				GUI.DrawTexture(new Rect(200,0,50,50),healthIcon);
			}
			else{
				GUI.DrawTexture(new Rect(200,0,50,50),nohealthIcon);
			}
			if(heart3){
				GUI.DrawTexture(new Rect(250,0,50,50),healthIcon);
			}
			else{
				GUI.DrawTexture(new Rect(250,0,50,50),nohealthIcon);
			}
		}
	}
}
