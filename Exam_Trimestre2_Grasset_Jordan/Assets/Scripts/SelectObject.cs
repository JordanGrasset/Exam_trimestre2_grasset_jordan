using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectObject : MonoBehaviour {

	GameObject ply;
	GameObject saveObjects;

	void Start() {
		ply = GameObject.FindGameObjectWithTag ("Player");
		saveObjects = GameObject.FindGameObjectWithTag ("Main");
	}

	public void OnClick() {

		
        //permet de crée la platforme choisit qui a le même nom que le bouton
		GameObject created = (GameObject)Instantiate (Resources.Load (this.name));

		//place au milieu
		created.transform.position = Camera.main.ScreenToWorldPoint
			(new Vector3(Screen.width/2, Screen.height/2, ply.transform.position.z + 10f));

		//lie à un parent
		created.transform.SetParent (saveObjects.transform);
	}
}
