using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class TutorialBullet : MonoBehaviour
{
    [SerializeField]
    private TextMesh scoreText;
    [HideInInspector]
    private GameObject scoreObject;
    private TrailRenderer trailrenderer;
    private GameObject gameController;
    private Game_Manager controllerScript;
    private bool canHitBack;
    private bool bulletHit;
    public bool hitable;
    private Rigidbody2D rigidbody;
    private Vector2 oldVel;
    private Vector2 force;
    [SerializeField]
    public float damage = 10;
    private float gainedScore = 2;
    public bool spawned;

    private int xForce;
    private int yForce;
    void Awake()
    {
        trailrenderer = gameObject.GetComponent<TrailRenderer>();
        trailrenderer.sortingLayerName = "TrailRenderer";
        rigidbody = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("Game_Controller") as GameObject;
        controllerScript = gameController.GetComponent<Game_Manager>();

    }

    void Start()
    {
        xForce = -250;
        yForce = 180;
        canHitBack = false;
        this.rigidbody.AddForce(new Vector2(xForce, yForce));

    }
    void FixedUpdate()
    {
        oldVel = rigidbody.velocity;
		controllerScript.mana += 1000f;
    }
    /// <summary>
    /// Raises the collsion enter2 d event.
    /// </summary>
    /// <param name="col">Col.</param>
    void OnCollisionEnter2D(Collision2D col)
    {
        hitable = true;
        StartCoroutine(CheckHitable());
        if (canHitBack)
        {
            ContactPoint2D cp = col.contacts[0];
            // calculate with addition of normal vector
            rigidbody.velocity = oldVel + cp.normal * 2.0f * oldVel.magnitude;
            // calculate with Vector3.Reflect
            rigidbody.velocity = Vector3.Reflect(oldVel, cp.normal);
            // bumper effect to speed up ball
            rigidbody.velocity += cp.normal * Time.time * 0.05f;

            if (gainedScore < 15)
            {
                damage *= 1.5f;
                gainedScore *= 1.5f;
                controllerScript.score += gainedScore;
                scoreText.text = "" + Mathf.Round(gainedScore);
                Instantiate(scoreText, transform.position, scoreText.transform.rotation);
            }
            canHitBack = false;
            bulletHit = false;


        }
        if (col.gameObject.tag == "Trail")
        {
            Destroy(col.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (hitable == true)
        {
            if (col.GetComponent<Collider2D>().tag == "hinderbombnose")
            {
                Destroy(gameObject);
            }


            if (col.tag == "enemy" && col.gameObject.name != "StopCube(Clone)" && col.gameObject.tag != "Trail")
            {

                gameController.GetComponent<CameraShake>().Shake();
                col.GetComponent<EnemyScript>().life -= damage;
                gameController.GetComponent<Game_Manager>().mana += 2.5F;
				TutorialEnd();
                Destroy(gameObject);
                col.GetComponent<EnemyScript>().CheckLife();
                controllerScript.Invoke("BackFromTut", 5f);
		
            }


        }
        if (col.tag == "TutorialBlock")
        {
            //rigidbody.velocity = new Vector2(0,0);
            //rigidbody.gravityScale = 0;
            Instantiate(Resources.Load("ClickHere"));
            Instantiate(Resources.Load("Mask"));
            Time.timeScale = 0.0001f;
            Time.fixedDeltaTime = 0.0001f;
            Destroy(col.gameObject);
        }
        if (col.tag == "SecondTutorialBlock")
        {
            //rigidbody.velocity = new Vector2(0,0);
            //rigidbody.gravityScale = 0;
            Instantiate(Resources.Load("ClickHere2"));
			Instantiate(Resources.Load("morebounce"));
			Instantiate(Resources.Load("SecondMask"));
            Instantiate(Resources.Load("RestartBlock"));
            Instantiate(Resources.Load("RestartBlock 2"));
            spawned = true;
            Time.timeScale = 0.0001f;
            Time.fixedDeltaTime = 0.0001f;
            Destroy(col.gameObject);
        }
        if (col.tag == "RestartBlock")
        {
            Instantiate(Resources.Load("failText"));
            StartCoroutine(RestartingLevel());
        }
    }

    IEnumerator RestartingLevel()
    {
       
        yield return new WaitForSeconds(2);
        Application.LoadLevel(45);
    }

    IEnumerator CheckHitable()
    {
        if (canHitBack == false && bulletHit == false)
        {
            bulletHit = true;
            yield return new WaitForSeconds(0.5f);
            canHitBack = true;
        }
    }

	void TutorialEnd(){
		Instantiate(Resources.Load("levelcompletetutorial"));
	}

}
