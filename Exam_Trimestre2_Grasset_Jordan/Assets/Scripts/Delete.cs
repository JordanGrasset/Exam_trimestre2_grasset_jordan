using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Delete : MonoBehaviour {

	public bool beingDragged;
	public DragDrop drg;
	public GameObject objDragged;
	private Color col;

	public GameObject ply;

	// Use this for initialization
	void Start () {
		drg = FindObjectOfType<DragDrop> ();

	}

	// Update is called once per frame
	void Update () {
		beingDragged = drg.beingDrg;
		objDragged = drg.dragged;
	}

	void OnTriggerStay2D (Collider2D other) {

		//Si l'objet n'est pas le joueur
		if (other.gameObject != ply) {

			//quand un objet est déplacé
			if (beingDragged) {

				//La couleur de la zone devient verte
				GetComponent<Image> ().color = new Color (0, 1f, 0, 0.5f);

				
			} else if (!beingDragged && objDragged != this.gameObject) {

				//détruit l'objet et enleve la couleur
				Destroy (other.gameObject);
				GetComponent<Image> ().color = new Color (1f, 1f, 1f, 0.3f);
			}
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		GetComponent<Image> ().color = new Color (1f, 1f, 1f, 0.3f);
	}
}
