using UnityEngine;
using System.Collections;

public class PlaneScript : EnemyScript {

	

	
	void Update(){
		
		// Set enemy back to start
		Reset ();
		transform.parent = null;
	}

	
	// Shoot a bullet
	protected override void Shoot(){
		if(canshoot == true){
			// Check spawn location and aim
			Vector2 spawnposition = new Vector2 (transform.position.x -1F,transform.position.y - 0.1F);
			Quaternion spawnrotation = Quaternion.identity;
			
			// Spawn bullet
			Instantiate (bullet_tank, spawnposition, spawnrotation);
		}
		
		// Set next shoot time
		Invoke("Shoot", spawnbullet = Random.Range (4, 7));
	}
	
	// Set enemy back at start position
	void Reset(){
		
		if(transform.position.x < -12F){
			transform.position = new Vector2 (14F,-2.88F);
			tower.GetComponent<Tower>().LoseLife();
		}
		
		if(transform.position.x < -2.8F){
			canshoot = false;
		}
		
		if(transform.position.x > -2.8F){
			canshoot = true;
		}
	}
}
