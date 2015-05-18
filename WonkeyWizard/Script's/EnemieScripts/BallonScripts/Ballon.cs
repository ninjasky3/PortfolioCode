using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ballon : EnemyScript {

	public int Damage;
	public GameObject explosionParticle;

    protected override void Start()
    {
        startingLife = life;
        rigid = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        tower = GameObject.Find("toren");
        gameController = GameObject.Find("Game_Controller");
        // Total life of the enemy
        // Move enemey
        rigid.velocity = (Vector3.left * speed / 2);
        // Shoot a bulet
        Invoke("Shoot", spawnbullet = Random.Range(2, 5));
        controllerscript = gameController.GetComponent<Game_Manager>();

    }

	protected override void Shoot(){

	}

}
