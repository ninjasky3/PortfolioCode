using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{

    //private
    private int originalspeed;
    private GameObject currentCollidingEnemy;

    //public
    public GameObject shootExplosion;
    public GameObject bullet_tank;
    public GameObject explosion;
    public GameObject soldier;
    public float standingDistance;
    public float life;
    public bool allowedToMove;
    public bool noPoints;
    public bool Armored;
    public int shootrate;
    public int speed;
    public Vector2 spawnvaluesbullet;
    public TextMesh scoreUpText;
    public Transform raypos;
    //child variables
    protected GameObject gameController;
    protected GameObject tower;
    protected Game_Manager controllerscript;
    protected float startingLife;
    protected int spawnbullet;
    protected bool canshoot;
    protected Animator enemyAnimator;
    protected Rigidbody2D rigid;
    [SerializeField]
    public bool Animate = true;
    [SerializeField]
    protected Image enemyHpBar;
    [SerializeField]
    protected AnimatorOverrideController destroyed1;
    [SerializeField]
    protected AnimatorOverrideController destroyed2;



    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        tower = GameObject.Find("toren");
        controllerscript = gameController.GetComponent<Game_Manager>();
        Image enemyBar = Instantiate(enemyHpBar) as Image;
        enemyBar.GetComponent<EnemyHpBar>().enemy = gameObject;
        enemyAnimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        startingLife = life;


        // Move enemey
        rigid.velocity = (Vector3.left * speed / 2);
        // Shoot a bulet
        Invoke("Shoot", spawnbullet = Random.Range(2, 5));
        originalspeed = speed;
    }


    protected virtual void Update()
    {
        transform.parent = null;
    }

    void FixedUpdate()
    {
        CastRaycast();
    }




    /// <summary>
    /// Shoot a bullet.
    /// </summary>
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
            if (Armored == false)
            {
                // Check spawn location and aim
                Vector2 spawnposition = new Vector2(transform.position.x - 2F, transform.position.y + 1F);
                Quaternion spawnrotation = Quaternion.identity;

                // Spawn bullet
                Instantiate(bullet_tank, spawnposition, spawnrotation);
            }
            if (Armored == true)
            {
                // Check spawn location and aim
                Vector2 spawnposition = new Vector2(transform.position.x - 2F, transform.position.y + 1F);

                // Spawn bullet
                Instantiate(bullet_tank, spawnposition, transform.rotation);
                Instantiate(shootExplosion, spawnposition, shootExplosion.transform.rotation);
            }
            // Set next shoot time
        }
        Invoke("Shoot", spawnbullet = shootrate);
    }
    /// <summary>
    /// cast raycast to check if the object should stop moving and animating
    /// </summary>
    protected virtual void CastRaycast()
    {
        if (allowedToMove == false)
        {
            //instantiating the raycast
            Ray2D ray = new Ray2D(new Vector2(transform.position.x - standingDistance, transform.position.y), transform.TransformDirection(Vector3.left));
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1);
            //check movement
            if (hit != null && hit.collider != null)
            {
                //stop moving
                if (hit.collider.tag == "enemy")
                {
                    rigid.velocity = (Vector3.left * 0 / 2);
                    if (Animate)
                    {
                        enemyAnimator.speed = 0;
                    }
                }
            }
            else
            {
                //start moving
                rigid.velocity = Vector3.left * speed / 2;
                if (Animate)
                {
                    enemyAnimator.speed = 1;
                }
            }
        }
    }

    /// <summary>
    /// check if the life of the enemy is under 0
    /// </summary>
    public void CheckLife()
    {
        //destroy the enemy
        if (life <= 0)
        {
            controllerscript.Score += 10;
            scoreUpText.text = "+10";
            Instantiate(scoreUpText, transform.position, transform.rotation);
            Destroy(gameObject);
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            if (gameController.GetComponent<Game_Manager>().maxMana > gameController.GetComponent<Game_Manager>().mana)
            {
                gameController.GetComponent<Game_Manager>().mana += 5;
            }
            if (noPoints == false)
            {
                gameController.GetComponent<Game_Manager>().enemiesKilled += 1;
            }
        }
        //damaged model 1
        if (life <= startingLife * 0.80 && life >= startingLife * 0.40 && gameObject.name != "hinderbomb")
        {
            enemyAnimator.runtimeAnimatorController = destroyed1;
        }
        //damaged model 2
        if (life <= startingLife * 0.40 && gameObject.name != "hinderbomb")
        {
            enemyAnimator.runtimeAnimatorController = destroyed2;
        }
    }
    bool WheelRotation()
    {
        return true;
    }
}

