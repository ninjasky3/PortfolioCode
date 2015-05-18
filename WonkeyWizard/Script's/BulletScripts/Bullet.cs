using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent(typeof (Collider2D))] 

public class Bullet : MonoBehaviour {

    [SerializeField]
    private Game_Manager controllerScript;
    private GameObject gameController;
    private GameObject scoreObject;
    [SerializeField]
    private TextMesh scoreText;
	private TrailRenderer trailrenderer;
    [HideInInspector]
    public bool hitable;
	private bool canHitBack;
    private bool bulletHit;
	private Rigidbody2D rigidbody;
	private Vector2 oldVelocity;
	private Vector2 force;
    private float gainedScore = 2;
    public float damage = 10;

	void Awake(){
		trailrenderer = gameObject.GetComponent<TrailRenderer>();
		trailrenderer.sortingLayerName = "TrailRenderer";
		rigidbody = GetComponent<Rigidbody2D>();
		gameController = GameObject.Find ("Game_Controller") as GameObject;
		controllerScript = gameController.GetComponent<Game_Manager> ();
	}

	void Start(){
        //starting force for the bullet
        force.x = Random.Range(-230, -300);
        force.y = Random.Range(80, 200);
		canHitBack = false;
		this.rigidbody.AddForce(force);
	}

	void FixedUpdate() {
		oldVelocity = rigidbody.velocity;
	}


	void OnCollisionEnter2D(Collision2D col){
        hitable = true;
        StartCoroutine(CheckHitable());
        if (canHitBack){
		    ContactPoint2D cp = col.contacts[0];
		    // calculate with addition of normal vector
		    rigidbody.velocity = oldVelocity + cp.normal *2.0f *oldVelocity.magnitude;
		    // calculate with Vector3.Reflect
		    rigidbody.velocity = Vector3.Reflect(oldVelocity,cp.normal);
		    // bumper effect to speed up ball
		    rigidbody.velocity += cp.normal * 1.75f;
            //raise the damage and score for each bump
            if(gainedScore < 15){
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
            //Collision behaviour on enemy hit
            if (col.GetComponent<Collider2D>().tag == "enemy" && col.gameObject.name != "StopCube(Clone)" && col.gameObject.tag != "Trail" && col.gameObject.name != "Truck")
            {
                gameController.GetComponent<CameraShake>().Shake();
                col.GetComponent<EnemyScript>().life -= damage;
                gameController.GetComponent<Game_Manager>().mana += 2.5F;
                col.GetComponent<EnemyScript>().CheckLife();
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Check if the bullet is allowed to return a hit
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckHitable()
    {
        if (canHitBack == false && bulletHit == false)
        {
            bulletHit = true;
            yield return new WaitForSeconds(0.2f);
            canHitBack = true;
        }
    }
}
	