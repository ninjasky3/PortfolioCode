# PortfolioCode WonkeyWizard
Code snippets

Game Manager

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{


    //enemy related publics variables
    public int maxEnemys;
    public int enemiesKilled;

    //level related privates variables
    private float previousAmountOfMana;
    private float fillValue;
    private float decreaseValue;
    private float refFloat = 0.1f;
    private int lastPlayedLevel;
    private bool levelDone;
    private bool paused;
    private bool setTrail;

    //level related privates SERIALIZED variables
    [SerializeField]
    private GameObject manaBarObject;
    [SerializeField]
    private Image manaBar;
    [SerializeField]
    private float maxScoreTime;

    //level related public variables
    public GameObject levelCompletedSprite;
    public GameObject clearStarsParent;
    public GameObject starScoreObject;
    public GameObject clearParticles;
    public GameObject scoreObject;
    public GameObject timeObject;
    public GameObject[] clearStars;
    public GameObject wizard;
    public GameObject tower;
    public int completedLevels;
    public int starScore;
    public int stars;
    public float score;
    public float mana;
    public float maxMana;
    public Sprite[] manaBarSprites;
    public Sprite winSprite;
    public Image timerImage;
    public Text scoreText;
    public Text starScoreText;
    // Shield related objects
    private GameObject origin;
    private GameObject optionsScreen;
    public GameObject currentMouseTrail;
    public GameObject trail;
    //animations
    public Animator wizardAnimation;
    private AudioSource wizardCastSound;


    public float Score
    {
        get { return score; }
        set { score = value; }
    }

    void Awake()
    {
        //finding objects
        GameObject optionButton = GameObject.FindGameObjectWithTag("OptionsButton");
        levelCompletedSprite = GameObject.FindGameObjectWithTag("LevelCompleteSprite");
        //parent object for the levelcompleted sprite
        clearStarsParent = GameObject.FindGameObjectWithTag("FinalWinStars");
        manaBarObject = GameObject.FindWithTag("ManaBar");
        optionsScreen = GameObject.Find("PauzeScreen");
        scoreObject = GameObject.Find("ScoreObject");
        timeObject = GameObject.Find("TimeObject");
        wizard = GameObject.Find("wizard");
        tower = GameObject.Find("toren");
        wizardAnimation = wizard.GetComponent<Animator>();
        manaBar = manaBarObject.GetComponentInChildren<Image>();
        scoreText = scoreObject.GetComponentInChildren<Text>();
        timerImage = timeObject.GetComponentInChildren<Image>();
        //the mouse point that creates the trail
        origin = Resources.Load("MouseTrail") as GameObject;
        wizardCastSound = wizard.GetComponent<AudioSource>();
        optionButton.GetComponent<Options>().AudioCheck();
        for (int i = 0; i < 3; i++)
        {
            clearStars[i] = GameObject.Find("WinStarSprite" + i);
        }
        optionsScreen.SetActive(false);
        clearStarsParent.SetActive(false);
        levelCompletedSprite.SetActive(false);
        //0.0083 * 120 = 0.996 && 0.0166 * 60 = 0.996
        decreaseValue = 0.996f / maxScoreTime;
    }

    // Use this for initialization
    void Start()
    {
        
        //set values
        //fillvalue is used in filling the mana/hpbars
        fillValue = 1.0f;
        Invoke("GainMana", 2f);
        mana = 100;
        previousAmountOfMana = mana;
        maxMana = 100;
        completedLevels = PlayerPrefs.GetInt("CompletedLevels");
        lastPlayedLevel = Application.loadedLevel;
        Invoke("SpawnBird", 2f);
        Instantiate(Resources.Load("StopCube"));
        Debug.Log(lastPlayedLevel);
        Debug.Log(completedLevels);
    }



    // Update is called once per frame
    void Update()
    {
        Shield();
        CheckMana();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauzeCheck();
        }
    }

    void FixedUpdate()
    {
        if (enemiesKilled == maxEnemys && levelDone == false)
        {
            clearlevel();
        }
        UpdateScore();
    }

    /// <summary>
    /// Wizard line drawing behaviour
    /// </summary>
    void Shield()
    {
        if (!paused)
        {
            //Instantiating the the line
            if (Input.GetMouseButtonDown(0))
            {
                if (mana > 0)
                {
                    if (currentMouseTrail != null)
                    {
                        //destroy current line of one is already instantiated
                        currentMouseTrail.GetComponent<TrailRenderer>().time = 0;
                        DestroyImmediate(trail);
                    }

                    Vector3 particlepos = new Vector3(wizard.transform.position.x + 0.4f, wizard.transform.position.y, wizard.transform.position.z);
                    if (trail == null)
                    {

                        setTrail = true;
                        wizardAnimation.SetBool("IsCasting", true);
                        wizardCastSound.Play();
                        //setting the line on the proper position
                        Vector3 shield = Input.mousePosition;
                        shield.z = 10;
                        shield = Camera.main.ScreenToWorldPoint(shield);
                        //instantiate the line with the shield position
                        currentMouseTrail = Instantiate(origin, shield, Quaternion.identity) as GameObject;
                        currentMouseTrail.name = "MouseTrail";
                        trail = GameObject.FindGameObjectWithTag("Trail");
                        //set the current trail parent to be the trail collider
                        currentMouseTrail.transform.parent = trail.transform;
                    }
                    //particle creation on mouse position
                    GameObject magicsparks = Instantiate(Resources.Load("MagicSparkParticles"), particlepos, transform.rotation) as GameObject;
                    magicsparks.transform.Rotate(new Vector3(90, 0, 0));
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (mana > 0 && currentMouseTrail != null && setTrail == true)
                {
                    //line following your mouse/finger position
                    previousAmountOfMana = mana;
                    mana -= 0.3f;
                    Vector3 pos = Input.mousePosition;
                    pos.z = 10;
                    pos = Camera.main.ScreenToWorldPoint(pos);
                    currentMouseTrail.transform.position = pos;
                }
            }
            //when you stop drawing the line
            if (Input.GetMouseButtonUp(0))
            {
                wizardAnimation.SetBool("IsCasting", false);
                setTrail = false;
            }
        }
    }


    /// <summary>
    /// Complete your current level.
    /// </summary>
    void clearlevel()
    {
        if (levelDone == false)
        {
            //check how many stars you have gained.
            if (tower.GetComponent<Tower>().healthPoints == 100)
            {
                stars += 1;
            }
            if (Score > starScore)
            {
                stars += 1;
            }
            if (maxScoreTime > 0.2f)
            {
                stars += 1;
            }
            //make sure youre not playing the same level to unlock additional levels
            if (lastPlayedLevel >= completedLevels)
            {
                completedLevels += 1;
            }
            //winscreen with the amount of stars you have gained
            levelCompletedSprite.SetActive(true);
            switch (stars)
            {
                case 1:
                    clearStars[0].GetComponent<SpriteRenderer>().sprite = winSprite;
                    break;
                case 2:
                    clearStars[0].GetComponent<SpriteRenderer>().sprite = winSprite;
                    clearStars[1].GetComponent<SpriteRenderer>().sprite = winSprite;
                    break;
                case 3:
                    clearStars[0].GetComponent<SpriteRenderer>().sprite = winSprite;
                    clearStars[1].GetComponent<SpriteRenderer>().sprite = winSprite;
                    clearStars[2].GetComponent<SpriteRenderer>().sprite = winSprite;
                    break;
            }
            clearStarsParent.SetActive(true);
            //save values of the game
            PlayerPrefs.SetInt("CompletedLevels", completedLevels);
            PlayerPrefs.SetInt("CompletedStars", stars);
            PlayerPrefs.SetInt("LastCompletedLevel", lastPlayedLevel);
            PlayerPrefs.SetInt("Level" + lastPlayedLevel, stars);
            PlayerPrefs.Save();
            scoreText.text = "SCORE : " + score;
            levelDone = true;
            Invoke("BackToMenu", 5);
        }
    }

    /// <summary>
    /// Checking the mana and filling the manabar image.
    /// </summary>
    void CheckMana()
    {
        if (mana > maxMana)
        {
            mana = maxMana;
        }
        if (mana < (fillValue * 100))
        {
            fillValue -= 0.01f;
        }

        if (mana > (fillValue * 100))
        {
            fillValue += 0.01f;
        }
        manaBar.fillAmount = fillValue;

    }

    void GainMana()
    {
        mana += 10;
        Invoke("GainMana", 5f);
    }

    /// <summary>
    /// Update score text
    /// </summary>
    void UpdateScore()
    {
        if (levelDone == false)
        {
            //set minimum time value
            if (maxScoreTime >= 0.1f)
            {
                maxScoreTime -= 1 * Time.deltaTime;
                //decrease the timer image fill with the value of the timer
                timerImage.fillAmount = maxScoreTime * decreaseValue;
            }
            scoreText.text = "SCORE " + Mathf.Round(score);
        }
    }


    /// <summary>
    /// spawning the environment birds
    /// </summary>
    void SpawnBird()
    {
        Instantiate(Resources.Load("Bird"), new Vector2(Random.Range(9, -9), Random.Range(0, 5)), transform.rotation);
        Invoke("SpawnBird", Random.Range(2, 8));
    }

    /// <summary>
    /// Go back to menu.
    /// </summary>
    public void BackToMenu()
    {

        Application.LoadLevel(42);
    }

    /// <summary>
    /// Go back to the menu from the tutorial
    /// </summary>
    public void BackFromTut()
    {

        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("firstTime", 1);
        Application.LoadLevel(42);
    }


    public void OpenOptions(bool open)
    {
        optionsScreen.SetActive(open);
    }

    /// <summary>
    /// pauze/unpauze the game
    /// </summary>
    public void PauzeCheck()
    {

        if (paused == false)
        {
            paused = true;
            optionsScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            paused = false;
            optionsScreen.SetActive(false);
            Time.timeScale = 1f;
        }

    }

}




Levelselect script with a star system

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelSelect : MonoBehaviour {

	// Use this for initialization
	public int playableLevels;
    public int obtainedStars;
	private int lockedlevels;
	private int lastPlayedLevel;
	private int leveltest;
	private int totalStarsObtained;
    [SerializeField]
	private int[]    _levelstars;
    [SerializeField]
	private Button[] buttons;
    [SerializeField]
	private Sprite[] lockedButtons;
    [SerializeField]
    private Sprite[] openImageStars;
    [SerializeField]
	private Camera mainCam;
    [SerializeField]
	private TextMesh totalStars;
	void Start () {

		playableLevels = PlayerPrefs.GetInt ("CompletedLevels");
		obtainedStars = PlayerPrefs.GetInt ("CompletedStars");
		lastPlayedLevel = PlayerPrefs.GetInt("LastCompletedLevel");
		totalStars.text = "" + obtainedStars;
		lockedlevels = 38 - playableLevels;
		//show the appropriate star sprite for the amount of stars obtained according to its level
		for (int i = 0; i < 40; i++)
		{
			string levelNumber = "Level" + i;
			_levelstars[i] = PlayerPrefs.GetInt(levelNumber,0);
			totalStarsObtained += _levelstars[i];
			totalStars.text = "" + totalStarsObtained;
            //set image sprites for all the 40 buttons according to the gained stars
            //38 - i - 2 reverse button counting
			if(_levelstars[i] == 1){
				buttons [38 - i + 2].image.sprite = openImageStars[0];
			}
			if(_levelstars[i] == 2){
				buttons [38 - i + 2].image.sprite = openImageStars[1];
			}
			if(_levelstars[i] == 3){
				buttons [38 - i + 2].image.sprite = openImageStars[2];
			}
		}
		//Lock the level buttons
		for (int i = 0; i <= lockedlevels; i++) {
	    buttons[i].GetComponent<Renderer>().sortingOrder = 0;
		buttons [i].image.sprite = lockedButtons[i];
        buttons[i].GetComponent<Animator>().enabled = false;
            //open the levels that aren't locked
	        if(_levelstars[i] == lockedlevels){
				buttons [40 - lastPlayedLevel].image.sprite = openImageStars[0];
			}
		}
	}

	// Update is called once per frame
	void Update () {
	playableLevels = PlayerPrefs.GetInt ("CompletedLevels");
	}
	/// <summary>
	/// Loads the level.
	/// </summary>
	/// <param name="level">Level you want to load.</param>
	public void LoadLevel(int level){
		if (level <= playableLevels +1) {
			Application.LoadLevel(level);
		}
	}

	public void BackToMenu(){
		Application.LoadLevel (0);
	}


	/// <summary>
	/// Switches the camera.
	/// </summary>
	/// <param name="forward">If set to <c>true</c> forward.</param>
	public void SwitchCamera(int forward){
				if (forward == 1) {
					mainCam.transform.position = new Vector3 (39.5f, 0.32f,-10f);
				}if (forward == 0) {
				    mainCam.transform.position = new Vector3 (1.85f, 0.32f,-10f);
				}
				if (forward == 2) {
			        mainCam.transform.position = new Vector3 (75.2f, 0.32f,-10f);
				}
				if (forward == 3) {
			        mainCam.transform.position = new Vector3 (108.9f, 0.32f,-10f);
				}
		}
}


EnemyScript (Parent script to the enemys)


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




one of the child scripts (BigTank)


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
