using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Options : MonoBehaviour {
	[SerializeField]
	private Animator optionAnimator;
    public GameObject[] buttons;
    private bool audioOff = true;
    private bool sfxOff = true;
    private bool buttonsOff = true;
    private bool settingsOff = true;
    [SerializeField]
    private Material onMaterial;
    [SerializeField]
    private Sprite[] muteSprites;
     [SerializeField]
    private Sprite[] onSprites;
    private GameObject gameManager;
    private int optionPrefs;


    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController") as GameObject;
        optionPrefs = PlayerPrefs.GetInt("optionPrefs");
        //AudioCheck();
    }

    public void SetAudio(GameObject clickedObject)
    {
         if (audioOff)
         {
             if (optionPrefs == 1)
             {
                 PlayerPrefs.SetInt("optionPrefs", 2);
             }
             else
             {
                 PlayerPrefs.SetInt("optionPrefs", 0);
             }
         clickedObject.GetComponent<Image>().sprite = muteSprites[0];
         audioOff = false;
         AudioCheck();
         }
         else
         {
             if (optionPrefs == 1)
             {
                 PlayerPrefs.SetInt("optionPrefs", 2);
             }
             else
             {
                 PlayerPrefs.SetInt("optionPrefs", 0);
             }
         clickedObject.GetComponent<Image>().sprite = onSprites[0];
         audioOff = true;
         AudioCheck();
         }

        }

        public void SetSFX(GameObject clickedObject){
           
            if (sfxOff)
            {
                if (optionPrefs == 0)
                {
                    PlayerPrefs.SetInt("optionPrefs", 2);
                }
                else
                {
                    PlayerPrefs.SetInt("optionPrefs", 1);
                }
            clickedObject.GetComponent<Image>().sprite = muteSprites[1];
            sfxOff = false;
            AudioCheck();
            }

            else
            {
                if (optionPrefs == 0)
                {
                    PlayerPrefs.SetInt("optionPrefs", 2);
                }
                else
                {
                    PlayerPrefs.SetInt("optionPrefs", 1);
                }
            clickedObject.GetComponent<Image>().sprite = onSprites[1];
            sfxOff = true;
            AudioCheck();
            }
        }

        public void AudioCheck()
        {
          
            optionPrefs = PlayerPrefs.GetInt("optionPrefs");

            if (gameManager != null)
            {
                switch (optionPrefs)
                {
                    case 0: gameManager.GetComponent<AudioManager>().StopMusic();
                        break;
                    case 1: gameManager.GetComponent<AudioManager>().StopSFX();
                        break;
                    case 2: gameManager.GetComponent<AudioManager>().StopMusic();
                        gameManager.GetComponent<AudioManager>().StopSFX();
                        break;

                }
            }
        }

        public void QuitGame()
        {
            Time.timeScale = 1;
            Application.Quit();
        }

        public void ReturnToMenu()
        {
            Time.timeScale = 1;
            Application.LoadLevel(42);
        }


	
}