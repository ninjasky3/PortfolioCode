using UnityEngine;
using System.Collections;

public class OldAI : MonoBehaviour {

		//public Transform target;										// target to aim for
		public float targetChangeTolerance = 1;				            // distance to target before target can be changed
		public ThirdPersonCharacter character { get; private set; }     // the character we are controlling
		Vector3 targetPos;
		
		//public
		public float speed = 1;
		public float rotationSpeed = 1;
		public Transform [] waypoints;
		//private
		private int wayPointIndex;
		private Vector3 targetDist;
		private Vector3 lookPosition;
		private Vector3 moveDirection;
		
		public Transform target;
		private float maxSpeed	=	10;
		private float mass		=	50;
		// Use this for initialization
		void Start () {
			character = GetComponent<ThirdPersonCharacter>();
			wayPointIndex = 0;
			rigidbody.velocity	=	new Vector3(0,0,0) * maxSpeed;
		}
		void Update(){
			Seek();
		}
		// switch direction
		void Seek () {
			
			// we berekenen eerst de afstand/Vector tot de 'target' (in dit voorbeeld de andere cubus)		
			Vector3 desiredStep	=	target.position - rigidbody.position;		
			
			// deze desiredStep mag niet groter zijn dan de maximale Speed
			//
			// als een vector ge'normalized' is .. dan houdt hij dezelfde richting
			// maar zijn lengte/magnitude is 1
			desiredStep.Normalize();
			
			// als je deze weer vermenigvuldigt met de maximale snelheid dan
			// wordt de lengte van deze Vector maxSpeed (aangezien 1 x maxSpeed = maxSpeed)
			// de x en y van deze Vector wordt zo vanzelf omgerekend
			Vector3 desiredVelocity			=	desiredStep	*	maxSpeed;
			
			// bereken wat de Vector moet zijn om bij te sturen om bij de desiredVelocity te komen
			Vector3 steeringForce			=	desiredVelocity - rigidbody.velocity;
			
			// uiteindelijk voegen we de steering force toe maar wel gedeeld door de 'mass'
			// hierdoor gaat hij niet in een rechte lijn naar de target
			// hoe zwaarder het object hoe moeilijker hij kan bijsturen
			rigidbody.velocity				=	rigidbody.velocity + steeringForce / mass;
			if(target != null){
				
				character.Move(desiredVelocity, false, true,steeringForce);
			}else{
				// We still need to call the character's move function, but we send zeroed input as the move param.
				character.Move( Vector3.zero, false, false, transform.position + transform.forward * 100 );
			}
		}
		void FixedUpdate () {
			if(waypoints.Length > 0){
				if(Vector3.Distance(transform.position, waypoints[wayPointIndex].position) < 1 )
				{
					wayPointIndex++;
				}
				if(wayPointIndex == waypoints.Length) 
				{
					wayPointIndex = 0;
				}
				//Vector3 targetDist = waypoints[wayPointIndex].position - transform.position;
				//transform.Translate(Vector3.forward * speed * Time.deltaTime);
				//Quaternion lookPosition =(transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDist), rotationSpeed * Time.deltaTime));
				//moveDirection = (Vector3.forward * speed * Time.deltaTime);
				
				
				
			}
			
		}
		
		void OnTriggerEnter(Collider col)
		{	
			if(col.name == "waypoint"){
				//rotationSpeed += 0.5f;
				//speed -= 1;
				if(wayPointIndex == waypoints.Length) {
					wayPointIndex = 0;
					
				}
				
			}
		}
		
		public void SetTarget(Transform target)
		{
			//this.target = target;
			
		}
	}


