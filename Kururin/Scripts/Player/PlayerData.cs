using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour {
	public Color mainColor;
	public Color secondaryColor;
	public bool secretunlocked;
	public int playerType = 1;

	public bool perfect1;
	public bool perfect2;
	public bool perfect3;
	public bool perfect4;
	public bool perfect5;
	public bool perfect6;
	public bool perfect7;
	public bool perfect8;
	public bool perfect9;
	public bool perfect10;








	// Use this for initialization
	void Start () {
		mainColor = new Color(1,1,1);
		secondaryColor = new Color(0,0,0);
		playerType = 1;
	}

	public void SetData(Color main,Color second,int type){
		mainColor = main;
		secondaryColor = second;
		playerType = type;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
