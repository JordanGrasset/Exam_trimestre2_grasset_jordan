using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SavePanel : MonoBehaviour {

	public bool clicked = true;
	public GameObject saveGrey;

	// Use this for initialization
	void Start () {
		clicked = true;
	}


	public void OnClick() {
		if (!clicked) {
			saveGrey.SetActive(false);
			clicked = true;
		} else {
			saveGrey.SetActive(true);
			clicked = false;
		}
	}
}
