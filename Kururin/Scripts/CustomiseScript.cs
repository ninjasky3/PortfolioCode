using UnityEngine;
using System.Collections;

public class CustomiseScript : MonoBehaviour {
	private FadeScript fade;
	private PlayerData pData;
	private int currentplayer = 2;
	private bool tomenu;
	public GUIStyle buttonStyle;
	public GUIStyle backStyle;

	public Color side1Color = new Color(0,0,0);
	public Color side2Color = new Color(0,0,0);

	private Texture2D colorPreview1;
	private Texture2D colorPreview2;
	private Texture2D panelOverlay1;
	private Texture2D panelOverlay2;

	public GameObject player;
	public GameObject[] side1;
	public GameObject[] side2;
	// Use this for initialization
	void Start () {
		pData = GameObject.Find("MainCube").GetComponent<PlayerData>();
		fade = GameObject.Find("MainCube").GetComponent<FadeScript>();
		CreatePreview(pData.playerType);
		side1Color = pData.mainColor;
		side2Color = pData.secondaryColor;
		Debug.Log("Data Color" +pData.mainColor +" Data SColor " + pData.secondaryColor);
		//player = GameObject.Find("Player");
		//side1 = GameObject.Find("Side1");
		//side2 = GameObject.Find("Side2");
		colorPreview1 = Resources.Load("Textures/Color-Panel") as Texture2D;
		colorPreview2  = Resources.Load("Textures/Color-Panel") as Texture2D;
		panelOverlay1 = Resources.Load("Textures/Color-Panel-Overlay") as Texture2D;
		panelOverlay2 = Resources.Load("Textures/Color-Panel-Overlay") as Texture2D;
	}
	void CreatePreview(int id){
		Debug.Log(player);
		currentplayer = id;
		switch(id){
		case 1:
			GameObject p1 = Instantiate(Resources.Load("Customise/PlayerOne"),new Vector3(2.93f,2,-3),Quaternion.identity) as GameObject;
			p1.name = "PlayerOne";
			side1[0] = GameObject.Find("Ring1");
			side1[1] = GameObject.Find("Ring2");
			side1[2] = GameObject.Find("Ring3");
			side2[0] = GameObject.Find("Ball1");
			side2[1] = GameObject.Find("Ball2");
			side2[2] = GameObject.Find("Bal");
			player = GameObject.Find("PlayerOne");
			player.transform.rotation = Quaternion.AngleAxis(90, Vector3.left);
			break;
		case 2:
			GameObject p2 = Instantiate(Resources.Load("Customise/PlayerTwo"),new Vector3(2.93f,2,-3),Quaternion.identity) as GameObject;
			p2.name = "PlayerTwo";
			side1[0] = GameObject.Find("Ring");
			side1[1] = GameObject.Find("Ring2");
			side1[2] = GameObject.Find("Ring3");
			side2[0] = GameObject.Find("Ball1");
			side2[1] = GameObject.Find("Ball2");
			side2[2] = GameObject.Find("Ball3");
			player = GameObject.Find("PlayerTwo");
			player.transform.rotation = Quaternion.AngleAxis(90, Vector3.left);
			break;
		case 3:
			GameObject p3 = Instantiate(Resources.Load("Customise/PlayerThree"),new Vector3(2.93f,2,-3),Quaternion.identity) as GameObject;
			p3.name = "PlayerThree";
			side1[0] = GameObject.Find("Ring1");
			side1[1] = GameObject.Find("Ring2");
			side1[2] = GameObject.Find("Ring3");
			side2[0] = GameObject.Find("Ball1");
			side2[1] = GameObject.Find("Ball2");
			side2[2] = GameObject.Find("Ball3");
			player = GameObject.Find("PlayerThree");
			player.transform.rotation = Quaternion.AngleAxis(90, Vector3.left);
			break;
	}
	}
	void RemoveIt(){
		side1[0] = null;
		side1[1] = null;
		side1[2] = null;
		side2[0] = null;
		side2[1] = null;
		side2[2] = null;
	}
	// Update is called once per frame
	void FixedUpdate () {
		if(tomenu && fade.blackscreen){
			fade.fadingOut = false;
			Application.LoadLevel("Menu");
		}
		if(player != null){
			player.transform.localScale = new Vector3(1,1,1);
			player.transform.Rotate(new Vector3(0,2,0));
			for(int c = 0; c < side1.Length; c++){
				if(side1[c] != null){
				side1[c].renderer.material.color = side1Color;
				}
			}
			for(int c = 0; c < side2.Length; c++){
			if(side2[c] != null){
				side2[c].renderer.material.color = side2Color;
			}
			}
		}
		else{
			CreatePreview(currentplayer);
		}
	}
	void SwitchPlayer(int id){
		if(id != currentplayer){
			Destroy(player);
			RemoveIt();
			currentplayer = id;
		}
	}
	void OnGUI () {
		if(GUI.Button (new Rect (0,0,200,100), "Back",backStyle)){
			fade.fadingOut = true;
			tomenu = true;
		}
		if(GUI.Button (new Rect (20,300,100,50), "Mavrick",buttonStyle)){
			SwitchPlayer(1);
		}
		if(GUI.Button (new Rect (130,300,100,50), "Divinity",buttonStyle)){
			SwitchPlayer(2);
		}
		if(GUI.Button (new Rect (240,300,100,50), "Bulwark",buttonStyle)){
			SwitchPlayer(3);
		}
		if(GUI.Button (new Rect (60,365,100,50), "Save",buttonStyle)){
			pData.SetData(side1Color,side2Color,currentplayer);
		}
		if(GUI.Button (new Rect (200,365,100,50), "Reset",buttonStyle)){
			side1Color = pData.mainColor;
			side2Color = pData.secondaryColor;
			SwitchPlayer(pData.playerType);
		}
		side1Color = RGBSlider (new Rect (90,130,200,10), side1Color);
		side2Color = RGBSlider (new Rect (90,230,200,10), side2Color);
		GUI.color = side1Color;
		GUI.DrawTexture(new Rect(255,130,50,50),colorPreview1);
		GUI.color = side2Color;
		GUI.DrawTexture(new Rect(255,230,50,50),colorPreview2);
		GUI.color = Color.white;
		GUI.DrawTexture(new Rect(255,230,50,50),panelOverlay2);
		GUI.DrawTexture(new Rect(255,130,50,50),panelOverlay1);

	}
	
	Color RGBSlider (Rect screenRect, Color rgb) {
		rgb.r = GUI.HorizontalSlider (screenRect, rgb.r, 0.0f, 1.0f);
		
		// &lt;- Move the next control down a bit to avoid overlapping
		screenRect.y += 20; 
		rgb.g = GUI.HorizontalSlider (screenRect, rgb.g, 0.0f, 1.0f);
		
		// &lt;- Move the next control down a bit to avoid overlapping
		screenRect.y += 20; 
		
		rgb.b = GUI.HorizontalSlider (screenRect, rgb.b, 0.0f, 1.0f);
		return rgb;
	}
}
