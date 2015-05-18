using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour 
{
	private float min = -15;
	private float max = -2;
	private float fov = 40;
	void  Start ()
	{

	}
	void  FixedUpdate ()
	{
		if(fov > 60){
			fov = 60;
		}
		else if(fov < 20){
			fov = 20;
		}
		camera.fieldOfView = fov;
		if(Input.GetAxis("Zoom") < 0){
			fov += 0.15f;
		}
		else if(Input.GetAxis("Zoom") >0){
			fov -= 0.15f;
		}
	}
}