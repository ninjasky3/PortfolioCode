using UnityEngine;
using System.Collections;

public class Nose : MonoBehaviour {

	// Use this for initialization
	public Transform hinderbomb;
	public int speed;
	private GameObject gameController;
    private Rigidbody2D rigid;
    public float standingDistance = 2f;
	void Start () {

        rigid = GetComponent<Rigidbody2D>();
		gameController = GameObject.Find ("Game_Controller");
        rigid.velocity = (Vector3.left * speed / 2);
		hinderbomb = transform.FindChild ("hinderbomb");
	}
	void Update(){
		if (hinderbomb == null) {
		Destroy (gameObject);
		}
        CastRaycast();
	}

    /// <summary>
    /// cast raycast to check if the object should stop moving and animating
    /// </summary>
    protected virtual void CastRaycast()
    {
        
        
            Ray2D ray = new Ray2D(new Vector2(transform.position.x - standingDistance, transform.position.y), transform.TransformDirection(Vector3.left));
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1);
            Debug.DrawRay(ray.origin, ray.direction * 1, Color.red);

            if (hit != null && hit.collider != null)
            {
                if (hit.collider.tag == "enemy")
                {
                    rigid.velocity = (Vector3.left * 0 / 2);
          
                }
            }
            else
            {
                rigid.velocity = Vector3.left * speed / 2;
                
            }
        }
    
}
	