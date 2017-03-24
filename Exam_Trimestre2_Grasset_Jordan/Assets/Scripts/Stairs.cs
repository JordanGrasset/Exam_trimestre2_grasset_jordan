using UnityEngine;
using System.Collections;

public class Stairs : MonoBehaviour {
	public float moveY;

	public GameObject mPlayer;

	private NewController insideL;

	// Use this for initialization
	void Start () {
		mPlayer = FindObjectOfType<NewController> ().gameObject;
		insideL = mPlayer.GetComponent<NewController> ();
	}

	//procedure qui permet de voir quand le joueur est en collision avec l'echelle
	void OnTriggerEnter2D (Collider2D c) {
		if (c.name == "Player") 
			insideL.inLadder = true;
	}

	//Quand le joueur reste dans la zone
	void OnTriggerStay2D (Collider2D c) {
		if (c.name == "Player") 
			insideL.inLadder = true;
	}

	//Quand le joueur sort de la zone
	void OnTriggerExit2D (Collider2D c) {
		if (c.name == "Player")
			insideL.inLadder = false;
	}


}
