using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Edit : MonoBehaviour {

	public bool edOrPlay;
	public GameObject butt;

	// Use this for initialization
	void Start () {
	}

    //changer le texte du bouton
	public void OnClick () {
		if (!edOrPlay) {
			butt.GetComponentInChildren<Text> ().text = "Jouer";
			edOrPlay = true;
			FindObjectOfType<NewController> ().startPos = FindObjectOfType<NewController> ().transform.position;
		} else {
			butt.GetComponentInChildren<Text> ().text = "Editer";
			edOrPlay = false;
			FindObjectOfType<NewController> ().transform.position = FindObjectOfType<NewController> ().startPos;
		}
	}
}
