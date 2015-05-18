using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class FadeIn : MonoBehaviour {
	[SerializeField]
	private Image[] image;
	[SerializeField]
	private float time = 3.5f;
	[SerializeField]
	private ParticleSystem[] partEffect;
	[SerializeField]
	private Transform[] slerpBeginPoints;
	[SerializeField]
	private Transform[] slerpEndPoints;


	private float refFloat = 0.1f;

	void Awake(){
		for(int j = 0; j < partEffect.Length; j++){
			partEffect[j].Play();
		}
		for(int i = 0; i < image.Length; i++){
			image[i].fillAmount = 0;
		}
	}
	void Update(){
		float fillValue = Mathf.SmoothDamp(0f, 1f, ref refFloat, time);
		for(int i = 0; i < image.Length; i++){
			image[i].fillAmount += fillValue;
		}
		for(int j = 0; j < partEffect.Length; j++){
			partEffect[j].transform.position = Vector3.MoveTowards (slerpBeginPoints[j].transform.position, slerpEndPoints[j].transform.position, Time.time * 5.8f);
		}

	}
}
