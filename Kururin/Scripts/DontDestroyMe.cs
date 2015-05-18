using UnityEngine;
using System.Collections;

public class DontDestroyMe : MonoBehaviour {

	// Used for Object's that wont be destroyed when a scene loads.
	// As it's the first object created the Scene will automaticly go to the Menu Scene.
	// The reason why this GameObject is in a seperate scene is because for example
	// we place this GameObject inside the first scene of the game(The Menu)
	// it will be created and kept alive everytime you load the Scene with this GameObject
	// this is something you must avoid else the game will clutter itself with this Object.
	void Start () {
	DontDestroyOnLoad(gameObject);
	Application.LoadLevel("Menu");
	}
}
