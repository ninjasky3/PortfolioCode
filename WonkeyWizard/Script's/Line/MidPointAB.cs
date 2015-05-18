using UnityEngine;
using System.Collections;
/*
 * Find Vector from Object this script is attached to ( in this example C ) to the midpoint between 2 other objects ( A & B )
 * Steven Sims January 10th 2014
 * 3 Objects A B C you want the vector from object C (this script attached to it) to the point midway between objects A and B in 3D space
 * Use the midpoint formula to create a new vector that describes the midpoint http://www.purplemath.com/modules/midpoint.htm
 * All you do us take the sum of the 2 points position co-ordinates and divide it by 2 do this for each component
 * such as midpoint.x = (pointA.x + pointB.x)/2
 * do the same for the y and z components
 * The midpointvector is then
 * MidPoint = ((pointA.x + pointB.x)/2, (pointA.y + pointB.y)/2, (pointA.z + pointB.z)/2)
 * 
 * NOTE: the lines with (not needed) in the comment are just for visualising the lines and are not
 * needed for the actual system
*/
public class MidPointAB : MonoBehaviour {
		public Transform target;
		public Transform OtherObjectA; // First object of pair
		public Transform OtherObjectB; // Second object of pair
		public float distance;
		public float X;
		public float Y;
		
		public void Update ()
		{
			
			
			//instantiate shield that collides
			if(Input.GetMouseButtonUp(0)){
				OtherObjectA = GameObject.Find("Origin(Clone)").transform; ;
				OtherObjectB = GameObject.Find("Destination(Clone)").transform; ;
				distance = Vector3.Distance(OtherObjectA.position, OtherObjectB.position);
				
				// calculate the midpoint
				X = (OtherObjectA.position.x + OtherObjectB.position.x) * 0.5F ;
				Y = (OtherObjectA.position.y + OtherObjectB.position.y) * 0.5F ;
				
				// set position and scale
				transform.position = new Vector2 (X,Y);
				transform.localScale = new Vector2 (distance,0.2F);
				
				// Rotate to the destination
				Quaternion rotation = Quaternion.LookRotation
					(OtherObjectB.transform.position - transform.position, transform.TransformDirection(Vector3.up));
				transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
			}
		}
		void OnTriggerEnter2D(Collider2D other){
			if (other.GetComponent<Collider2D>().tag == "bullet" || other.GetComponent<Collider2D>().tag == "Planebullet") {
				OtherObjectA.GetComponent<OriginScript>().DestroyMe();
			}
		}
	}

