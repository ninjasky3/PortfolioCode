using UnityEngine;
using System.Collections;

public class TruckCollider : MonoBehaviour {

	// Use this for initialization
	public Transform trucker;
	public int speed;
	private GameObject gameController;
    private Rigidbody2D rigid;
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
		rigid.velocity = (Vector3.left * speed / 2);
		trucker = transform.FindChild ("TruckArt");
		gameController = GameObject.Find ("Game_Controller");
	}
	void Update(){
		Reset ();
		CastRaycast ();
		if (trucker == null) {
			Destroy (gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject != null) {
						if (other.GetComponent<Collider2D>().tag == "bullet" && other.GetComponent<Bullet>().hitable) {
						gameController.GetComponent<CameraShake>().Shake();
						Destroy (other.gameObject);
						}
				}
	}

	void Reset(){
		
		if(transform.position.x < -12F){
			transform.position = new Vector2 (14F,-3.009761F);
		}
		
		if(transform.position.x < -2.8F && transform.position.x > 14F){
		}
		
		if(transform.position.x > -2.8F && transform.position.x < 14F){
		}
	}

	public void CastRaycast(){
			Ray2D ray = new Ray2D (new Vector2 (transform.position.x - 4, transform.position.y), transform.TransformDirection (Vector3.left));
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 1);
			Debug.DrawRay (ray.origin, ray.direction * 1, Color.yellow);	
			if (hit != null && hit.collider != null) {
				if (hit.collider.tag == "enemy") {
					rigid.velocity = (Vector3.left * 0 / 2);
				} 
			} else {
				rigid.velocity = (Vector3.left * speed / 2);	
			}
	}
}
