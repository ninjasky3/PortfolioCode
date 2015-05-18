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
