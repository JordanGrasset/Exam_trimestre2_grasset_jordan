using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SaveLevel : MonoBehaviour {
	public GameObject EditButton;
	public GameObject Scroll;
	public GameObject FinirButton;
	public GameObject FinirPanel;
	public GameObject savedObjects;
	public InputField InputLevelName;
	public bool CreateNewButton;
	public Vector3 prefabTransform;
	public Vector3 lastButton;


	public void OnClick() {
		GameControl.control.CurrentLevelName = InputLevelName.text;

		//detruit les élements d'édition
		Destroy (EditButton);
		Destroy (Scroll);
		Destroy (FinirPanel);
		Destroy (FinirButton);
	
		//permet de crée une copie en prefab
		PrefabUtility.CreatePrefab
			("Assets/Resources/"+InputLevelName.text+".prefab", savedObjects);
		prefabTransform = savedObjects.transform.position;
		GameControl.control.currentX = prefabTransform.x;
		GameControl.control.currentX = prefabTransform.y;
		GameControl.control.currentX = prefabTransform.z;
		GameControl.control.Save ();

		}

}
