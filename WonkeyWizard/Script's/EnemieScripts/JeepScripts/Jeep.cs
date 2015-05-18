using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Jeep : EnemyScript {

    protected Rigidbody2D rigidbody;
    private AudioSource shootSound;
	// Move the cloud
	protected override void Start () {
            //startingLife = life;
            Image enemyBar =  Instantiate(enemyHpBar) as Image;
            enemyBar.GetComponent<EnemyHpBar>().enemy = gameObject;
                
                rigidbody = GetComponent<Rigidbody2D>();
				gameController = GameObject.Find ("Game_Controller");
				tower = GameObject.Find ("toren");
				enemyAnimator = GetComponent<Animator> ();
				// Total life of the enemy
				life = 50;
				// Move enemey
				rigidbody.velocity = (Vector3.left * speed / 2);
				// Shoot a bulet
				Invoke ("Shoot", spawnbullet = 2);
				controllerscript = gameController.GetComponent<Game_Manager> ();
                shootSound = GetComponent<AudioSource>();
		}
	/// <summary>
	/// Shoot a bullet.
	/// </summary>
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
			Vector2 spawnposition = new Vector2 (transform.position.x,transform.position.y + 2F);
			Quaternion spawnrotation = Quaternion.identity;
            shootSound.Play();
			// Spawn bullet
			Instantiate (bullet_tank, spawnposition, spawnrotation);
		}
		
		// Set next shoot time
		Invoke("Shoot", spawnbullet = 4);
	}


	

}
