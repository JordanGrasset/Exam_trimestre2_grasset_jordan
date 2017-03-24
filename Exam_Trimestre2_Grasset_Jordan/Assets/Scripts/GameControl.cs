using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

	public static GameControl control;

	public string CurrentLevelName;
	public float currentX;
	public float currentY;
	public float currentZ;
	public bool testNewButton = false;

	void Awake () {

		//Si l'objet permanent n'existe pas
		if (control == null) {

			//Ne pas détruire l'objet en
			//changeant de scène
			DontDestroyOnLoad (gameObject);

			//L'objet permanent est celui
			//qui contient ce script
			control = this;

			//Si l'objet permanent n'est pas
			//celui qui contient ce script
		} else if (control != this) {

			//Detruire cet objet
			Destroy (gameObject);
		}
	}

	public void Save() {

		//Création d'un nouveau fichier de sauvegarde avec le nom
		//du niveau
		testNewButton = true;
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create 
			(Application.persistentDataPath + "/"+CurrentLevelName+".dat");

		//Le nom dans le fichier devient celui du jeu
		PlayerData data = new PlayerData();
		data.savedLevelName = CurrentLevelName;
		data.savedX = currentX;
		data.savedY = currentY;
		data.savedZ = currentZ;

		//Écriture dans le fichier de nos données
		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load(string levelName) {

		//Si le fichier de sauvegarde existe
		if (File.Exists (Application.persistentDataPath + "/"+levelName+".dat")) {

			//Aperture du fichier correspondant
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open 
				(Application.persistentDataPath + "/"+levelName+".dat", FileMode.Open);

			//Lecture des données sur le fichier
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			//Le nom dans le jeu devient celui du fichier
			CurrentLevelName = data.savedLevelName;
			Application.LoadLevel(3);

			//Dans une nouvelle scène, chargement du prefab avec le nom du fichier
			Resources.Load ("Assets/Resources/" + CurrentLevelName + ".prefab");
			Camera.main.transform.SetParent 
			(GameObject.FindGameObjectWithTag ("Player").transform);
		}
	}
}

[Serializable]
class PlayerData {
	public string savedLevelName;
	public float savedX;
	public float savedY;
	public float savedZ;
}