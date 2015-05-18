using UnityEngine;
using System.Collections;

public class MapControls : MonoBehaviour {
	public Vector3 mapcameraposition;
	public Camera mapcamera;
	public GameObject cameracam;
	public CameraController cameracontrol;

	public bool mapopen;
	// Use this for initialization
	void Start () {
		cameracam = GameObject.Find("Main Camera");
		cameracontrol = cameracam.GetComponent<CameraController>();
	}
	void Update(){
		if(Input.GetKeyDown(KeyCode.M)){
			if(!mapopen){
			OpenMap();
			}
			else{
				CloseMap();
			}
		}
	}
	// Update is called once per frame
	void OpenMap(){
		cameracam.camera.enabled = false;
		mapcamera.enabled = true;
		Time.timeScale = 0;
		mapopen = true;
	}
	void CloseMap(){
		cameracam.camera.enabled = true;
		mapcamera.enabled = false;
		Time.timeScale = 1;
		mapopen = false;
	}
}
