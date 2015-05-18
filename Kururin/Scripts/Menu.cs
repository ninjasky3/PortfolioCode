using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	public GUIStyle ButtonStyle;
	public GUIStyle nullStyle;
	public GUIStyle levelStyle;
	public GUIStyle secretStyle;
	public GUIStyle checkboxStyle;

	private FadeScript fadescript;
	private PlayerData pData;

	private int scrollposition;
	private bool startgame;
	private bool startcustomise;

	public Texture CreditsPanel;
	public Texture OptionsPanel;
	public Texture LevelPanel;
	public Texture note;
	public float levelposition = -500;
	private int optionsposition = -960;
	private int creditsposition = 960;
	private bool optionsopen;
	private bool creditsopen;
	private bool levelopen;
	private int levelnum;

	private float musicvolume = 0.5f;
	void Start(){
		fadescript = GameObject.Find("MainCube").GetComponent<FadeScript>();
		pData = GameObject.Find("MainCube").GetComponent<PlayerData>();
	}

	// all the levels
	void GoToLevel(int levelnum){
		switch(levelnum){
			case 1:
			Application.LoadLevel("Level1");
			break;
			case 2:
			Application.LoadLevel("Level2");
			break;
			case 3:
			Application.LoadLevel("Level4");
			break;
			case 4:
			Application.LoadLevel("Level3");	
			break;
			case 5:
			Application.LoadLevel("Test");	
			break;
			case 6:
			Application.LoadLevel("");	
			break;
			case 7:
			Application.LoadLevel("");	
			break;
			case 8:
			Application.LoadLevel("");
			break;
			case 9:
			Application.LoadLevel("");
			break;
			case 10:
			Application.LoadLevel("Level10");
			break;
		}
	}
	//cheat for getting all the perfect levels
	void FixedUpdate(){
		if(Input.GetKey(KeyCode.P)){
			pData.perfect1 = true;
			pData.perfect2 = true;
			pData.perfect3 = true;
			pData.perfect4 = true;
			pData.perfect5 = true;
		}
		//loading the custom screen
		if(startcustomise && fadescript.blackscreen){
			fadescript.fadingOut = false;
			Application.LoadLevel("Costumisation");
		}
		//sliding of the optionscreen
		if(optionsopen){
			if(optionsposition != 0){
				optionsposition += 16;
			}
		}
		else{
			if(optionsposition != -960){
				optionsposition -= 16;
			}
		}

		//sliding of the level screen from above
		if(levelopen){
			if(levelposition != 0){
				levelposition += 12.5f;
			}
		}
		else{
			if(levelposition != -500){
				levelposition -= 12.5f;
			}
		}
		//sliding of the credit screen from right
		if(creditsopen){
			if(creditsposition != 0){
				creditsposition -= 16;
			}
		}
		else{
			if(creditsposition != 960){
				creditsposition += 16;
			}
		}
		scrollposition --;
		if(scrollposition <= -100){
			scrollposition = -100;
		}
		//fading of the screen and then starting the level
		if(fadescript.alphaFadeValue == 1 && startgame){
			Debug.Log("GOTOSTART");
			fadescript.fadingOut = false;
			GoToLevel(levelnum);
		}
	}
	void  OnGUI (){
		if(GUI.Button (new Rect (0,600 + scrollposition,320,100), "Start", ButtonStyle)){
			if(creditsopen){
				creditsopen = false;
			}
			if(optionsopen){
				optionsopen = false;
			}
			if(levelopen){
				levelopen = false;
			}
			else{
				levelopen = true;
			}
		}
		//buttons
		if(GUI.Button (new Rect (320,600 + scrollposition,320,100), "Options", ButtonStyle)){
			if(creditsopen){
				creditsopen = false;
			}
			if(levelopen){
				levelopen = false;
			}
				if(optionsopen){
					optionsopen = false;
				}
				else{
					optionsopen = true;
				}
		}

		if(GUI.Button (new Rect (640,600 + scrollposition,320,100), "Credits", ButtonStyle)){
			if(optionsopen){
				optionsopen = false;
			}
			if(levelopen){
				levelopen = false;
			}
				if(creditsopen){
					creditsopen = false;
				}
				else{
					creditsopen = true;
				}
		}

		//OptionPanel Content//
		GUI.DrawTexture(new Rect(optionsposition,0,960,500),OptionsPanel);
		GUI.DrawTexture(new Rect(optionsposition + 630,160,100,100),note);
		musicvolume  = GUI.HorizontalSlider(new Rect(optionsposition+ 600,280,160,80),musicvolume,0,1.0f);
		AudioListener.volume = musicvolume;
		if(GUI.Button (new Rect (optionsposition + 860,0,100,500), "", nullStyle)){
			optionsopen = false;
		}
		if(GUI.Button (new Rect (optionsposition+ 200,200,200,100), "Customise", ButtonStyle)){
			startcustomise = true;
			fadescript.fadingOut = true;

		}
		/////////////////////////
		//CreditsPanel Content//
		GUI.DrawTexture(new Rect(creditsposition,0,960,500),CreditsPanel);
		if(GUI.Button (new Rect (creditsposition + 0,0,100,500), "", nullStyle)){
			creditsopen = false;
		}
		///////////////////////
		//LevelPanel Content
		GUI.DrawTexture(new Rect(0,levelposition,960,500),LevelPanel);
		if(GUI.Button (new Rect (0,levelposition + 400,960,95), "", nullStyle)){
			levelopen = false;
		}
		if(pData.perfect1){
			if(GUI.Button (new Rect (180,levelposition + 100,64,64), "1", secretStyle)){
				levelnum = 1;
				ReadytoStart();
			}
		}
		else{
			if(GUI.Button (new Rect (180,levelposition + 100,64,64), "1", levelStyle)){
				levelnum = 1;
				ReadytoStart();
			}
		}
		if(pData.perfect2){
			if(GUI.Button (new Rect (308,levelposition + 100,64,64), "2", secretStyle)){
				levelnum = 2;
				ReadytoStart();
			}
		}
		else{
		if(GUI.Button (new Rect (308,levelposition + 100,64,64), "2", levelStyle)){
			levelnum = 2;
			ReadytoStart();
		}
		}
		if(pData.perfect3){
			if(GUI.Button (new Rect (436,levelposition + 100,64,64), "3", secretStyle)){
				levelnum = 3;
				ReadytoStart();
			}
		}
		else{
		if(GUI.Button (new Rect (436,levelposition + 100,64,64), "3", levelStyle)){
			levelnum = 3;
			ReadytoStart();
		}
		}
		if(pData.perfect4){
			if(GUI.Button (new Rect (564,levelposition + 100,64,64), "4", secretStyle)){
				levelnum = 4;
				ReadytoStart();
			}
		}
		else{
		if(GUI.Button (new Rect (564,levelposition + 100,64,64), "4", levelStyle)){
			levelnum = 4;
			ReadytoStart();
		}
		}
		if(pData.perfect5){
			if(GUI.Button (new Rect (692,levelposition + 100,64,64), "5", secretStyle)){
				levelnum = 5;
				ReadytoStart();
			}
		}
		else{
		if(GUI.Button (new Rect (692,levelposition + 100,64,64), "5", levelStyle)){
			levelnum = 5;
			ReadytoStart();
		}
		}

		//if(pData.secretunlocked){
		if(pData.perfect1 && pData.perfect2 && pData.perfect3 && pData.perfect4 && pData.perfect5){
		if(GUI.Button (new Rect (436,levelposition + 300,64,64), "?", secretStyle)){
			levelnum = 10;
			ReadytoStart();
			}
		}

	}
	void ReadytoStart(){
		startgame = true;
		fadescript.fadingOut = true;
	}
}