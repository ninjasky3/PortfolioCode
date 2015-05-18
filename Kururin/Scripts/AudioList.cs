using UnityEngine;
using System.Collections;

public class AudioList : MonoBehaviour {
	public AudioClip WALLHIT;
	public AudioClip SPRINGHIT;
	public AudioClip DIE;
	public AudioClip WIN;

	void Start() {

		WALLHIT = Resources.Load("Audio/Bots") as AudioClip;
		SPRINGHIT = Resources.Load("Audio/Springveer") as AudioClip;
		DIE = Resources.Load("Audio/Lose") as AudioClip;
		WIN = Resources.Load("Audio/victory") as AudioClip;
	}

}