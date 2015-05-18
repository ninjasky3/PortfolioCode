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