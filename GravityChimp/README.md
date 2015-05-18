# PortfolioCode GravityChimp
Code Snippets from the game GravityChimp


EnemyScript for the pig enemys

using UnityEngine;
using System.Collections;
 
public class EnemyWalk : MonoBehaviour {
       
        public bool alife = true;
        bool aggroLeft;
        bool aggroRight;
        int speed = 50;
       
        public  bool gravityIsUp;
        tk2dSpriteAnimator animator;
        tk2dSprite sprite;
       
        void Start(){
                animator = GetComponent<tk2dSpriteAnimator>();
                sprite = GetComponent<tk2dSprite>();
                animator.AnimationEventTriggered += AnimationEventHandler;
        }
       
        void walk(bool left,float speedMultiplayer = 1){
                float walkSpeed = speed*speedMultiplayer;
                if (gravityIsUp){
                        if(left){
                                rigidbody.AddForce(walkSpeed,0,0);
                        }else{
                                rigidbody.AddForce(-walkSpeed,0,0);
                        }
                }else{
                        if(left){
                                rigidbody.AddForce(-walkSpeed,0,0);    
                        }else{
                                rigidbody.AddForce(walkSpeed,0,0);     
                        }
                }
        }
       
        void switchDirection(){
                if(sprite.FlipX){
                        sprite.FlipX = false;  
                }else{
                        sprite.FlipX = true;
                }
        }
       
        void FixedUpdate () {
                if(alife){
                        //follow player
                        if(aggroLeft){
                                walk (true,2.0f);      
                        }else if (aggroRight){
                                walk (false,2.0f);     
                        }
                        //normal walk
                        if(!aggroLeft&&!aggroRight){
                                if(sprite.FlipX){
                                        walk (true);
                                }else{
                                        walk(false);
                                }
                        }
               
                        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.left));
                        RaycastHit hit;
                        Debug.DrawRay(ray.origin, ray.direction * 20, Color.red);      
       
                        if (Physics.Raycast(ray, out hit, 20))
                        {
                                if (hit.collider.gameObject.tag == "Player")
                                {
                                        aggroLeft = true;
                                        sprite.FlipX = true;
                                }else{
                                        aggroLeft = false;     
                                }
                        }else{
                                aggroLeft = false;     
                        }
                       
                        Ray ray2 = new Ray(transform.position, transform.TransformDirection(Vector3.right));
                        RaycastHit hit2;
                        Debug.DrawRay(ray2.origin, ray2.direction * 20, Color.red);    
       
                        if (Physics.Raycast(ray2, out hit2, 20))
                        {
                                if (hit2.collider.gameObject.tag == "Player")
                                {
                                        aggroRight = true;
                                        sprite.FlipX = false;
                                }else{
                                        aggroRight = false;    
                                }
                        }else{
                                aggroRight = false;    
                        }      
                }
        }
       
        void AnimationEventHandler(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip, int frameNum)
        {
                Destroy(gameObject);
        }
       
        void OnCollisionEnter (Collision col)
    {
                if(col.collider.tag == "Player"){
                        if(col.contacts[0].normal.y > 0.8 ||col.contacts[0].normal.y < -0.8){
                                animator.Play("Die");
                                alife = false;
                                audio.Play();
                        }
                        float xforce = col.contacts[0].normal.x ;
                        float yforce = col.contacts[0].normal.y ;
                        if(yforce>0.8){
                                yforce = 1;
                        }else if(yforce<-0.8){
                                yforce = -1;
                        }else{yforce = 0 ;}
                        if(xforce>0.8){
                                xforce = 1;
                        }else if(xforce<-0.8){
                                xforce = -1;
                        }else{xforce = 0 ;}
                       
                        Vector3 playerForceDirection = new Vector3(-xforce*30,-yforce*30,0);
                        Debug.Log (yforce+" "+playerForceDirection);
                        col.collider.rigidbody.AddForce(playerForceDirection,ForceMode.Impulse);
                        rigidbody.AddForce(new Vector3(xforce*60,0,0),ForceMode.Impulse);
                }else{
                        switchDirection();     
                }
        }
       
        void OnCollisionStay ( Collision col ){
                for(int co = 0;co<col.contacts.Length;co++){
                               
                }
        }
}


The player script for the chimp that switches gravity 


using UnityEngine;
using System.Collections;
 
public class PlayerMove : MonoBehaviour {
       
        public float gravityForce = 80f;
        public float horForce =70.0f;
        public Vector3 playerOrgin;
       
        public AudioClip switchGrafity;
       
        bool grafityUpDown = false;
        bool gravitySwitchDone = false;
        bool gravityswitchHalfDone = false;
        bool gravityswitchTriggerd = false;
        float xAxis;
        bool onGround = false;
        int timer;
        int frameID = 0;
        tk2dSprite sprite;
       
       
        void Awake(){
                playerOrgin = transform.position;
        }
       
        void Start () {
                Physics.gravity = new Vector3(0f,-gravityForce,0f);
                rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                sprite = GetComponent<tk2dSprite>();
                sprite.MakePixelPerfect();
        }
       
        void Update(){
                if(Input.GetKeyUp(KeyCode.Space)){
                        audio.PlayOneShot(switchGrafity);
                        if(onGround){
                                if(grafityUpDown){
                                        Physics.gravity = new Vector3(0f,-gravityForce,0f);
                                        grafityUpDown = false;
                                        onGround = false;
                                }else{
                                        Physics.gravity = new Vector3(0f,gravityForce,0f);
                                        grafityUpDown = true;
                                        onGround = false;
                                }
                                gravityswitchTriggerd = true;
                        }
                }
        }
       
        //bool moveVelocityChange = false;
        void FixedUpdate () {
               
                xAxis = Input.GetAxis("Horizontal");
               
                if(xAxis>0){
                        rigidbody.AddForce(horForce,0,0,ForceMode.Force);
                }else if(xAxis<0){
                        rigidbody.AddForce(-horForce,0,0,ForceMode.Force);
                }
                //animate
                timer+=1;
                if((timer >= 3&&!onGround)||(onGround&&timer >= 7)){
                        timer = 0;
                        //flipX
                        if (xAxis > 0){
                                sprite.FlipX = true;   
                        }else if( xAxis < 0){
                                sprite.FlipX = false;  
                        }
                        //flipY
                        if (gravityswitchHalfDone||onGround){
                                if (grafityUpDown){
                                        sprite.FlipY = true;   
                                }else{
                                        sprite.FlipY = false;  
                                }
                        }
                       
                        //change frame
                        if (onGround == true){
                                if(xAxis==0){
                                        //idle
                                        frameID++;
                                        if(frameID >=3){frameID = 0;};
                                }else{
                                        frameID++;
                                        if(frameID <=8||frameID >=14){frameID = 9;};   
                                }
                        }else if(gravityswitchTriggerd){
                                //switch
                                if(frameID == 25){
                                        gravityswitchHalfDone = true;
                                }
                                if(gravityswitchHalfDone && frameID ==18){
                                        gravitySwitchDone = true;
                                }
                                if(!gravityswitchHalfDone){
                                        frameID++;     
                                }else{
                                        if(!gravitySwitchDone){
                                                frameID--;
                                        }
                                }
                                if(frameID <=17){frameID = 18;};
                        }
                        sprite.spriteId = frameID;
                }
        }
       
        //check if thouching ground
        void onGroundCheck(Collision col){
                for(int cols = 0; cols < col.contacts.Length; cols++){
                        if (col.contacts[cols].normal.x < 0.1&&col.contacts[cols].normal.x > -0.1){
                                onGround = true;       
                                gravitySwitchDone = false;
                                gravityswitchHalfDone = false;
                        }
                }
        }
       
        //collision events
        void OnCollisionEnter (Collision col)
    {
                onGroundCheck(col);
                gravityswitchTriggerd = false;
        }
       
        void OnCollisionStay ( Collision col ){
                onGroundCheck(col);
        }
       
        void OnCollisionExit(Collision col) {
        onGround = false;
    }
}


this script handles the platform movement and moves the player accordingly

using UnityEngine;
using System.Collections;
 
public class PlatformMove : MonoBehaviour {
        //public
        public Transform Origin;
        public Transform Destination;
        public float Speed = 10;
        //private
        private GameObject player;
        private bool direction = false;
        private bool playerOn = false;
        private float deltaX = 0;
        private float OldX;
        private float deltaY = 0;
        private float OldY;
        private tk2dSprite sprite;
       
        void Start(){
                sprite = GetComponent<tk2dSprite>();
                player = GameObject.FindGameObjectWithTag("Player");
                OldX = transform.position.x;   
                OldY = transform.position.y;
        }
       
        void FixedUpdate () {
                //get delta position platform
                float currentX = transform.position.x;
                deltaX = OldX -currentX;
                OldX = currentX;
                float currentY = transform.position.y;
                deltaY = OldY -currentY;
                OldY = currentY;
                // switch direction
                if(transform.position == Destination.position){ direction = true;       }
                if(transform.position == Origin.position){              direction = false;      }
               
                //move platform
                if(direction){
                        transform.position = Vector3.MoveTowards(transform.position, Origin.position, Speed);
                }else{
                        transform.position = Vector3.MoveTowards(transform.position, Destination.position, Speed);
                }
                // stick player to platform
                if(playerOn){
                        player.transform.Translate(new Vector3(-deltaX,-deltaY,0));
                }
        }
       
        void OnCollisionEnter (Collision col)
    {
                if(col.collider.tag == "Player"){
                        playerOn = true;
                }
    }
       
        void OnCollisionStay ( Collision col ){
                if(col.collider.tag == "Player"){
                        playerOn = true;
                }
        }
       
        void OnCollisionExit(Collision col) {
        if(col.collider.tag == "Player"){
                        playerOn = false;
                }
    }
}
