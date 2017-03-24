using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewController : MonoBehaviour {
    //variable 
	const int STATE_IDLE = 0;
	const int STATE_CORRER = 1;

	public bool isGrounded;
	public bool bind;
	public IsGrounded tester;
	public Rigidbody2D rgb;
	public bool status;
	public GameObject SelectMenu;

	public float speed = 5f;
	public float moveX;
	public float moveY;
	public float scroll;
	public float jumpHeight;
	public float gravityStore;
	public Vector3 startPos;

	public bool inLadder;
	public GameObject cam;
	public Animator anim;
	public bool dir = true;
	public int state;
	public int blinkTime;
	public GameObject animate;

	// Use this for initialization
	void Start () {
		rgb = GetComponent<Rigidbody2D> ();
		
		gravityStore = rgb.gravityScale;
		status = FindObjectOfType<Edit> ().edOrPlay;
		startPos = transform.position;

		anim = animate.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		status = FindObjectOfType<Edit> ().edOrPlay;

		//permet le deplacement 
		if (!status) {
			//Reset des propriétés qui ont été changées pendant le mode édition
			GetComponent<Rigidbody2D> ().isKinematic = false;
			Camera.main.orthographicSize = 5;
			bind = true;
			
			SelectMenu.SetActive (false);

			//permet de remettre le personnage à sa place initial quand il tombe
			if (transform.position.y < -15) {
				transform.position = startPos;
			}

			//permet de savoir si il est sur un objet de type sol
			isGrounded = tester.grounded;
			moveX = Input.GetAxis ("Horizontal");

			//permet de se deplacer et de sauter
			if (isGrounded) {
				if (Input.GetKeyDown (KeyCode.Space)) {
					rgb.velocity = new Vector2 (rgb.velocity.x, jumpHeight);
				} else 
					rgb.velocity = new Vector2 (moveX * speed, rgb.velocity.y);
				if (Input.GetKey (KeyCode.D)) {
					changeState(STATE_CORRER);
					if (!dir) {
						dir = true;
						changeDirection (dir);
					}
				} else if (Input.GetKey (KeyCode.A)) {
					changeState(STATE_CORRER);
					if (dir){
						dir = false;
						changeDirection (dir);
					}

				}

				if (Input.GetKeyUp (KeyCode.D)) {
					changeState(STATE_IDLE);

				} else if (Input.GetKeyUp (KeyCode.A)) {
					changeState(STATE_IDLE);

				}
			}



		}

		// permet d'utiliser les echelles
		if (inLadder) {
			//supprime la gravité
			rgb.gravityScale = 0f;

			rgb.velocity = new Vector2 (moveX * speed, Input.GetAxis ("Vertical") * speed);

			//enleve la collision pour grimper
			if (Input.GetAxis("Vertical") > 0)
				GetComponent<BoxCollider2D> ().isTrigger = true;
		}

		//reset la collision et la gravité
		if (!inLadder) {
			rgb.gravityScale = gravityStore;
			GetComponent<BoxCollider2D> ().isTrigger = false;
		}
	
		// modification dans l'édition
		if (status) {
			bind = false;
			GetComponent<Rigidbody2D> ().isKinematic = true;
			rgb.gravityScale = 0f;
			SelectMenu.SetActive(true);

			//zoom et mouvement de la caméra
			moveX = 3*Input.GetAxis ("Horizontal");
			moveY = 3*Input.GetAxis ("Vertical");
			scroll = -50*Input.GetAxis ("MouseScroll");
			cam.transform.parent = null;
			cam.transform.Translate (new Vector3 (moveX * Time.deltaTime, moveY * Time.deltaTime, 0f));
			Camera.main.orthographicSize += scroll * Time.deltaTime;
		}
	}

	//fonction de fin de jeu 
	void OnTriggerEnter2D(Collider2D c) {
		if (!status && c.CompareTag ("Game Over")) {
			blinkTime = 8;
			InvokeRepeating ("GameOver", 0, 0.5f);
		}
	}

	//Changer l'animation
	void changeState (int state) {
		switch (state) {
		case STATE_IDLE:
			anim.SetInteger ("State", STATE_IDLE);
			break;
		case STATE_CORRER:
			anim.SetInteger ("State", STATE_CORRER);
			break;
		}
	}

	//Changer la direction du sprite
	void changeDirection(bool dir) {
		if (dir) 
			transform.Rotate (0, -180, 0);
		else if (!dir)
			transform.Rotate (0, 180, 0);
	}

	//Blink puis game over
	void GameOver() {
		if (blinkTime > 0) {
			animate.GetComponent<Renderer> ().enabled = !animate.GetComponent<Renderer> ().enabled;
			blinkTime--;
		}
		else if (blinkTime == 0) {
			transform.position = startPos;
			animate.GetComponent<Renderer> ().enabled = true;
			blinkTime = -1;
		}
	}

}

