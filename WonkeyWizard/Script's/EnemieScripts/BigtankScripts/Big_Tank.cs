using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Big_Tank : EnemyScript{

    private AudioSource tankAudio;

    protected override void Start()
    {
        startingLife = life;
        Image enemyBar = Instantiate(enemyHpBar) as Image;
        enemyBar.GetComponent<EnemyHpBar>().enemy = gameObject;
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
        tankAudio = GetComponent<AudioSource>();
    }

    protected virtual void Shoot()
    {
        if (transform.position.x <= 9)
        {
            canshoot = true;
        }
        else
        {
            canshoot = false;
        }
        if (canshoot == true)
        {

                 tankAudio.Play();
                // Check spawn location and aim
                Vector2 spawnposition = new Vector2(transform.position.x - 2F, transform.position.y + 1F);

                // Spawn bullet
                Instantiate(bullet_tank, spawnposition, transform.rotation);
                Instantiate(shootExplosion, spawnposition, shootExplosion.transform.rotation);
            
            // Set next shoot time

        }
        Invoke("Shoot", spawnbullet = shootrate);
    }
}
