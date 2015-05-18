using UnityEngine;
using System.Collections;

public class ExplodingBalloon : EnemyScript{

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

    protected override void Update()
    {
        rigid.velocity = Vector3.left * speed / 2;
    }
    protected override void Shoot()
    {

    }

    public void ExplosionAnimation()
    {
        Instantiate(explosionParticle, new Vector2(transform.position.x, transform.position.y - 3f), transform.rotation);
    }

    protected override void CastRaycast()
    {
      
    }
}
