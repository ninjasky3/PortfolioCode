using UnityEngine;
using System.Collections;

public class TruckScript : EnemyScript {

	protected override void Start () {
		gameController = GameObject.Find ("Game_Controller");
		tower = GameObject.Find ("toren");
		// Total life of the enemy
		life = 10;
		// Move enemey
		rigid.velocity = (Vector3.left * speed / 2);
		// Shoot a bulet
		controllerscript = gameController.GetComponent<Game_Manager> ();
	}


}
