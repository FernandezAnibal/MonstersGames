using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnCharacter : MonoBehaviour {

	public List<GameObject> characterList;
	public int index;
	public GameObject[] characters;
	public int prefChannel;
	//Spawpoint
	public Vector3 spawnPoint;
	// Use this for initialization

	void Start () {

		index = AllData.data.monsterPref;
		//Cargar de la carpeta todos los mounstros
		characters = Resources.LoadAll<GameObject> ("Characters");

		//Instanciar cada mounstro como gameobjects y meterlos dentro del objeto Characterlist

			GameObject monster = Instantiate (characters[index]) as GameObject;
			monster.transform.SetParent (transform);

			characterList.Add (monster);
			monster.SetActive (false);
			characterList [0].SetActive (true);


		prefChannel = AllData.data.prefChannel;
		switch (prefChannel)
		{
		case 1:
			spawnPoint = new Vector3 (-1, 0.1f, 0f);
			break;
		case 0:
			spawnPoint = new Vector3 (-1, 0.1f, 0f);
			break;
		default:
			spawnPoint = new Vector3 (-1, 0.1f, 0f);
			break;
		}

		//Colocar al personaje al inicio de la carrera

		monster.transform.position = spawnPoint;

		//Colocarlo en la dirección adecuada al inicio de la carrera
		var rotation = Quaternion.LookRotation(Vector3.zero-Vector3.left);
		monster.transform.rotation = rotation;

	

	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
