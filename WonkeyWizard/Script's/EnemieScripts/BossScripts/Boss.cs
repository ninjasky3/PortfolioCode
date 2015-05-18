using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Boss : EnemyScript {


	protected override void Shoot(){
        if (transform.position.x <= 9)
        {
            canshoot = true;
        }
        else
        {
            canshoot = false;
        }
		if(canshoot == true){
				// Check spawn location and aim
			Vector2 spawnposition = new Vector2 (transform.position.x -2F,transform.position.y + 3F);
			Quaternion spawnrotation = Quaternion.identity;
			Vector2 secondbulletpos = new Vector2 (transform.position.x -2F,transform.position.y + 2F);
			Vector2 thirdbulletpos = new Vector2 (transform.position.x -2F,transform.position.y + 0);

				// Spawn bullet
			int random = Random.Range(1,4);
			switch (random){
			case 1:
				Instantiate (bullet_tank, spawnposition, spawnrotation);
				Instantiate (shootExplosion, spawnposition, transform.rotation);
				break;
			case 2:
				Instantiate (bullet_tank, secondbulletpos, spawnrotation);
				Instantiate (shootExplosion, secondbulletpos, transform.rotation);
				break;
			case 3:
				Instantiate (bullet_tank, thirdbulletpos, spawnrotation);
				Instantiate (shootExplosion, thirdbulletpos, transform.rotation);
				break;
			}
		}
		// Set next shoot time
		Invoke("Shoot", spawnbullet = shootrate);
	}

}
