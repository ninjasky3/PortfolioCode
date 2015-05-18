using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tower : MonoBehaviour {

	// lifes of the tower
    [HideInInspector]
	public float healthPoints;
    private float maxLife;
    private float fillValue;
    private float startingLife;
    [SerializeField]
    private GameObject healthBarObject;
    [SerializeField]
    private Sprite[] towerSprites;
    private Image healthBar;
    private GameObject gameController;
    private Game_Manager controllerScript;
	//public GameObject balloonAnimation;
	// Use this for initialization
	void Start () {
        
        fillValue = 1.0f;
		healthPoints = 100;
        startingLife = healthPoints;
        healthBarObject = GameObject.FindWithTag("HpBar");
        healthBar = healthBarObject.GetComponentInChildren<Image>();
        gameController = GameObject.Find("Game_Controller") as GameObject;
        controllerScript = gameController.GetComponent<Game_Manager>();
	}

    void Update()
    {
        CheckLife();
    }
	void OnTriggerEnter2D(Collider2D other) {
        
		if(other.GetComponent<Collider2D>().tag == "enemy")
		{

			other.gameObject.GetComponent<ExplodingBalloon>().ExplosionAnimation();
            healthPoints -= other.gameObject.GetComponent<ExplodingBalloon>().Damage;
			CheckLife();
			Destroy (other.gameObject);
			CheckLife();

		}
       
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "bullet")
        {
            LoseLife();
            Destroy(col.gameObject);
            gameController.GetComponent<CameraShake>().Shake();
            GameObject dustParticles = Instantiate(Resources.Load("DustParticles"), transform.position, transform.rotation) as GameObject;
           dustParticles.transform.Rotate(new Vector3(90, 0, 0));
           this.GetComponent<CameraShake>().Shake();
        }
    }


	

    void CheckLife()
    {
        if (healthPoints < (fillValue * 100))
        {
            fillValue -= 0.01f;
        }

        if (healthPoints > (fillValue * 100))
        {
            fillValue += 0.01f;
        }
        healthBar.fillAmount = fillValue;
        if (healthPoints < 0)
        {
           Application.LoadLevel(41);
        }
        if (healthPoints <= startingLife * 0.80 && healthPoints >= startingLife * 0.40)
        {
            GetComponent<SpriteRenderer>().sprite = towerSprites[1];
        }
        if (healthPoints <= startingLife * 0.40)
        {
            GetComponent<SpriteRenderer>().sprite = towerSprites[2];
        }
    }

	public void LoseLife(){
		healthPoints -= 14;
		CheckLife ();
		}

}
