using UnityEngine;
using System.Collections;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour {
    [SerializeField]
    private AudioSource[] SoundEffects;
    [SerializeField]
    private AudioMixer mainMixer;
     [SerializeField]
    private AudioMixerGroup Master;
    [SerializeField]
     private AudioMixerGroup SFX;

    private GameObject options;
    private bool audioOff;
    private bool sfxOff;
    private int pitches;

	// Use this for initialization
	void Awake () {
        options = GameObject.FindGameObjectWithTag("OptionsButton");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F9))
        {
           
            switch (pitches)
            {
                  
                case 0:
                    mainMixer.SetFloat("MusicPitch", 0.5f);
                    pitches++;
                    break;
                case 1:
                    mainMixer.SetFloat("MusicPitch", 1f);
                    pitches++;
                    break;
                case 2:
                    mainMixer.SetFloat("MusicPitch", 1.5f);
                    pitches++;
                    break;
                case 3:
                    mainMixer.SetFloat("MusicPitch", 2f);
                    pitches = 0;
                    break;

            }
        }
    }

   public void StopMusic()
    {
        if (!audioOff)
        {
            mainMixer.SetFloat("MusicVolume", -80f);
            audioOff = true;
        }
        else
        {
            mainMixer.SetFloat("MusicVolume", 0f);
            audioOff = false;
        }
    }

   public void StopSFX()
    {
        if (!sfxOff)
        {
            mainMixer.SetFloat("SFXVolume", -80f);
            sfxOff = true;
        }
        else
        {
            mainMixer.SetFloat("SFXVolume", 0f);
            sfxOff = false;
        }
    }
}
