using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	// Use this for initialization
	public AudioSource Sounds;
	public AudioClip[] runningSounds;
	public AudioClip   jumpSound;
	private bool otherSoundIsPlaying;
	private float timer;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		timer+= 1 * Time.deltaTime;

			//Sounds.clip = runningSounds[Random.Range(0,3)];
	
			//Sounds.Play();
			if(ThirdPersonCharacter.playSound == true && otherSoundIsPlaying == false){
				if(!Sounds.isPlaying){
					int n = Random.Range(0,runningSounds.Length);
					Sounds.clip = runningSounds[n];
					Sounds.Play();
					runningSounds[n] = runningSounds[0];
					runningSounds[0] = Sounds.clip;
					
				}
			}
		if(ThirdPersonCharacter.playJumpSound == true){



			if(!Sounds.isPlaying && timer > 1.2f){
			
			Sounds.clip = jumpSound;
			Sounds.Play();
			otherSoundIsPlaying = true;
				timer = 0;
			}
		}
		else{
			otherSoundIsPlaying = false;
		}

		if(ThirdPersonCharacter.playSound == false && ThirdPersonCharacter.playJumpSound == false){
			Sounds.Stop();
		}
	}
}
