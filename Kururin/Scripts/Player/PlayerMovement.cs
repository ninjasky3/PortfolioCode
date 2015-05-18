using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public GUIStyle buttons;
	public int nextlevel;
	public Texture panel;
	public float movementSpeed = 1;
	public float rotationSpeed = 2;
	public bool rotationDirection = false;
	public int playerhealth = 3;

	public Vector3 respawnposition;
	public bool respawndirection;
	public ParticleSystem checkpointparticles;
	public CameraController cameraController;
	public AudioList aList;
	private PlayerData pData;

	public bool disablemovement;
	public bool isdead;
	public bool isinvincible;
	public bool damagedelay = false;
	private Vector3 dir = new Vector3(0,-1,0);
	private RaycastHit hit;
	private float dist = 2;
	private bool victoryplay = false;
	private float speedup;

	private Vector3 movement = new Vector3(0,0);
	public bool hasdied = false;
	private int cheatcode;
	public bool leveldone;
	private bool moveUp;
	public bool lastlevel = false;
	private string wintext = "Perfect";
	// Use this for initialization
	void Start () {
		speedup = 1;
		cheatcode = 0;
		aList = GameObject.Find("MainCube").GetComponent<AudioList>();
		pData = GameObject.Find("MainCube").GetComponent<PlayerData>();
		cameraController = GameObject.Find("Camera").GetComponent<CameraController>();
		respawnposition = transform.position;
	}
	void FixedUpdate () {
		if (moveUp){
			isdead = true;
			transform.position = new Vector3(transform.position.x,transform.position.y + 0.1f,transform.position.z);
		}
		if(!isdead){
			CharacterMovement();
			CheckBelowPlayer();
		}
		if(playerhealth <= 0 && !isdead){
			StartCoroutine("WaitforRespawn");
		}
	}
	void CheckBelowPlayer(){
		if(Physics.Raycast(transform.position,dir,out hit,dist)){
			if(hit.collider.name == "Checkpoint"){
				playerhealth = 3;
				Collider coll = hit.collider;
				isinvincible = true;
				respawnposition = coll.gameObject.transform.position;
				respawndirection = rotationDirection;
				checkpointparticles = coll.gameObject.GetComponentInChildren<ParticleSystem>();
				checkpointparticles.Play();
			}
			if(hit.collider.name == "LevelEnd"){
				Vector3 thisposition = hit.collider.gameObject.transform.position;
				transform.position = new Vector3(thisposition.x,thisposition.y + 1,thisposition.z);
				disablemovement = true;
				StartCoroutine("ToNextLevel");
			}
		}
		else{
			isinvincible = false;
			if(checkpointparticles != null){
				checkpointparticles.Stop();
				checkpointparticles = null;
			}
		}

	}
	IEnumerator ToNextLevel(){
		if(!victoryplay){
			audio.PlayOneShot(aList.WIN);
			victoryplay = true;
		}
		transform.Rotate(new Vector3(0,30,0));
		yield return new WaitForSeconds(1);
		cameraController.target = null;
		moveUp = true;
		leveldone = true;
		if(!hasdied){
		switch(nextlevel){
			case 2:
				pData.perfect1 = true;
			break;
			case 3:
				pData.perfect2 = true;
			break;
			case 4:
				pData.perfect3 = true;
				break;
			case 5:
				pData.perfect4 = true;
				break;
			case 6:
				pData.perfect5 = true;
				break;
			case 7:
				pData.perfect6 = true;
				break;
			case 8:
				pData.perfect7 = true;
				break;
			case 9:
				pData.perfect8 = true;
				break;
			case 10:
				pData.perfect9 = true;
				break;
			case 11:
				pData.perfect10 = true;
				break;
			}
		}
	}
	void CharacterMovement(){
		if(!isdead){
			if(!disablemovement){
				CheckRotation();
				PlayerInput();
			}
		}
		else{
			cameraController.transform.position = cameraController.transform.position;
			transform.position = transform.position;
		}
	}

	void CheckRotation(){
		if(!rotationDirection){
			transform.Rotate(new Vector3(0,rotationSpeed,0));
		}
		else{
			transform.Rotate(new Vector3(0,-rotationSpeed,0));
		}
	}

	void PlayerInput(){
		if(Input.GetKey(KeyCode.Space)){
			speedup = 1.5f;
		}
		else{
			speedup = 1;
		}
		movement = new Vector3(0,0,0);
		Vector3 position = transform.position;
		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
		if(input.x > 0){
			movement.x = movementSpeed * speedup;
		}
		else if(input.x < 0){
			movement.x = -movementSpeed * speedup;
		}
		if(input.z > 0){
			movement.z = movementSpeed * speedup;
		}
		else if(input.z < 0){
			movement.z = (-movementSpeed * speedup);
			Debug.Log(movement.z + ":" + speedup);
		}
		if(!isdead){
			gameObject.rigidbody.velocity = movement;
			position.x = Mathf.Round(position.x * 100f) / 100f;
			position.z = Mathf.Round(position.z * 100f) / 100f;
			transform.position = position;
		}
	}
	IEnumerator WaitforRespawn(){
		wintext = "You Won";
		audio.PlayOneShot(aList.DIE);
		cameraController.target = null;
		Transform[] go = GetComponentsInChildren<Transform>();
		foreach(Transform gob in go){
			if(gob.collider){
				gob.collider.enabled = false;
			}
			if(gob.renderer){
				gob.renderer.enabled = false;
			}
		}
		GameObject explosion = Resources.Load("PlayerExplosion") as GameObject;
		Instantiate(explosion,new Vector3(transform.position.x,transform.position.y - 0.5f,transform.position.z), new Quaternion(90,0,0,101));
		isdead = true;
		yield return new WaitForSeconds(2);
		isdead = false;
		RespawnPlayer();
		foreach(Transform gob in go){
			if(gob.collider){
				gob.collider.enabled = true;
			}
			if(gob.renderer){
				gob.renderer.enabled = true;
			}
		}
		hasdied = true;
		cameraController.target = this.gameObject.transform;
	}
	void giveKnockback(){
		audio.PlayOneShot(aList.WALLHIT);
		if(!damagedelay){
			Debug.Log("Give Delay");
			StartCoroutine("DamageDelay");
		}
		if(rotationDirection){
			transform.Rotate(new Vector3(0,20,0));
		}
		else{
			transform.Rotate(new Vector3(0,-20,0));
		}

	}
	IEnumerator DamageDelay(){
		damagedelay = true;
		inflictDamage();
		yield return new WaitForSeconds(0.5f);
		damagedelay = false;
	}
	void inflictDamage(){
		if(damagedelay){
			playerhealth--;
		}
	}
	void changeRotation(){
		if(rotationDirection){
			rotationDirection = false;
		}
		else{
			rotationDirection = true;
		}
	}
	void RespawnPlayer(){
		playerhealth = 3;
		rotationDirection = respawndirection;
		transform.position = new Vector3(respawnposition.x,transform.position.y,respawnposition.z);
	}
	void OnCollisionEnter(Collision coll){
		if(coll.collider.tag == "Spring"){
			audio.PlayOneShot(aList.SPRINGHIT);
			changeRotation();
		}
		if(coll.collider.tag == "Wall"){
			transform.position = new Vector3(transform.position.x - (movement.x / 8),transform.position.y,transform.position.z - (movement.z / 8));
			giveKnockback();
			cameraController.shakeCamera();
		}
	}
	void OnGUI(){
		if(!leveldone){
			if(GUI.Button (new Rect (0,0,100,50), "Quit", buttons)){
				Application.LoadLevel("Menu");
			}
		}
		if(leveldone){
			GUI.DrawTexture(new Rect(360,200,300,200),panel);
			if(GUI.Button (new Rect (420,230,180,50), wintext, buttons)){}
			if(!lastlevel){
				if(GUI.Button (new Rect (400,300,100,50), "Next", buttons)){
					switch(nextlevel){
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
					}
				}
			}
			if(GUI.Button (new Rect (520,300,100,50), "Quit", buttons)){
				Application.LoadLevel("Menu");
			}
		}
	}
}