using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Transform target;
    public float distance = 0.0f;
  	private float height = 3.0f;
    private float damping = 5.0f;
	public bool targetFound = false;
    void FixedUpdate () 
		//camera looking for the player
    {
		if(target == null)
		{
			targetFound = false;
		}
		if(target != null){
			targetFound = true;
		}
		//camera is moving with player
		if(targetFound){
   			  Vector3 wantedPosition;
         	  wantedPosition = target.TransformPoint(0, height - 2, distance);
         	  transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * damping);
        }

	}
	//starts shaking the camera when hit
	public void shakeCamera(){
		StartCoroutine("Shake");
	}
	IEnumerator Shake(){
		this.transform.Translate(new Vector3(0.5f,0,0));
		yield return new WaitForSeconds(0.03f);
		this.transform.Translate(new Vector3(0,0,-0.5f));
		yield return new WaitForSeconds(0.03f);
		this.transform.Translate(new Vector3(-0.5f,0,0));
		yield return new WaitForSeconds(0.03f);
		this.transform.Translate(new Vector3(0.5f,0,0.5f));
	}
}
