using UnityEngine;
using System.Collections;
//permet de verifier si la platforme est du type sol
public class IsGrounded : MonoBehaviour {
	public bool grounded;

	void Start() { 
		grounded = false;
	}

	void OnTriggerEnter2D (Collider2D c) {
		if (c.tag == "ground") 
			grounded = true;
		
	}

	void OnTriggerStay2D (Collider2D c) {
		if (c.tag == "ground")
			grounded = true;
	}

	void OnTriggerExit2D (Collider2D c) {
		if (c.tag == "ground")
			grounded = false;
	}
}
