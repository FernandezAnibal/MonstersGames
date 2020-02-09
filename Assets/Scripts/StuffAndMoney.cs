using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StuffAndMoney : MonoBehaviour {

	public GameObject monster;
	public GameObject clocksGenerator;
	public GameObject coinsGenerator;
	public GameObject stuff;

	public GameObject clock;
	public GameObject coin;

	private GameObject [] clocksStorage;
	private GameObject [] coinsStorage;
	private GameObject [] obsStorage;


	public int coins;
	public int clocks;

	private int indStuff;
	private int indCoins;
	private int indClocks;
	private float probabilityCoins;
	private float probabilityClocks;
	private float distance;
	private float rPos;


	// Use this for initialization
	void Start () {
		//Probabilidad de aparecer relojes
		probabilityClocks = AllData.data.upgradeData[0];
		//Probabilidad de aparecer monedas
		probabilityCoins =AllData.data.upgradeData[1];
		coins = 4;
		clocks = 4;

		//******************************************************************Almacenadores
		// Llenar un array con todos los obstaculos que tengan en el hijo se hace un doble
		obsStorage = new GameObject[stuff.transform.childCount*2];
		for (int i = 0; i < stuff.transform.childCount; i++){
			//obsStorage [i] = stuff.transform.GetChild (i).gameObject;
			//obsStorage [i+stuff.transform.childCount] = stuff.transform.GetChild (i).gameObject;
			GameObject obj = (GameObject)Instantiate (stuff.transform.GetChild (i).gameObject);
			GameObject obj2 = (GameObject)Instantiate (stuff.transform.GetChild (i).gameObject);
			obj.SetActive (false);
			obj2.SetActive (false);
			obsStorage[i] = obj;
			obsStorage[i+stuff.transform.childCount] = obj2;

		}

		// Llenar el creador de relojes con relojes
		clocksStorage = new GameObject[clocks];
		for (int i = 0; i < clocks; i++) 
		{
			GameObject obj = (GameObject)Instantiate (clock);
			obj.SetActive (false);
			clocksStorage[i] = obj;
		}

		//Llenar el creador de monedas con monedas
		coinsStorage = new GameObject[coins];
		for (int i = 0; i < coins; i++) 
		{
			GameObject obj = (GameObject)Instantiate (coin);
			obj.SetActive (false);
			coinsStorage[i] = obj;
		}
		//**************************************************************************************


		rPos = transform.position.x;
		distance = 10;
		monster = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {

		//Sigue al mounstro por todos lados
		transform.position = new Vector3(monster.transform.position.x,transform.position.y,transform.position.z);

		// si se mueve a una distancia del punto de referencia activa el cambio de posición
		if( (transform.position.x - rPos) > distance ){
			ChangePos (clocksGenerator);
			ChangePos (coinsGenerator);
			rPos = transform.position.x;

			//recorre el indice para cada generador
			if (indClocks < clocksStorage.Length - 1) {
				indClocks++;
			} else {
				indClocks = 0;
			}

			if (indCoins < coinsStorage.Length - 1) {
				indCoins++;
			} else {
				indCoins = 0;
			}



			int indStuffR = 0;
			while(obsStorage [indStuff].activeSelf == true && indStuffR < 5){
				indStuff = Mathf.FloorToInt (Random.Range (0, obsStorage.Length - 1));
				indStuffR++;
			}

			/*
			if ( indStuff < obsStorage.Length - 1) {
				indStuff++;
			} else {
				indStuff = 0;
			}
			*/

			//Cada vez que pasa la distance se evalua segun la probabilidad cuando salen las monedas o los relojes

			if ( Mathf.FloorToInt(Random.Range(0, 100f)) <= probabilityClocks) { 
				clocksStorage [indClocks].transform.position = clocksGenerator.transform.position;
				clocksStorage [indClocks].SetActive (true);
			}

			if ( Mathf.FloorToInt(Random.Range(0, 100f)) <= probabilityCoins) { 
				coinsStorage [indCoins].transform.position = coinsGenerator.transform.position;
				coinsStorage [indCoins].SetActive (true);
			}

			//Solo sale si el objeto del indice elegido esta invisible
			if (obsStorage [indStuff].activeSelf == false) {

				if (Mathf.FloorToInt (Random.Range (0, 100f)) <= 30) { 
					obsStorage [indStuff].transform.rotation = new Quaternion (transform.rotation.x, Random.rotation.y, transform.rotation.z, transform.rotation.w);
					obsStorage [indStuff].transform.position = stuff.transform.position;
					obsStorage [indStuff].SetActive (true);

				}
			}

			//si alguna de las cosas esta a mas de 15 metros del personaje hacia atras entonces pasa a ser inactivo
			for(int i = 0; i < clocksStorage.Length ; i++)
			{
				if ((monster.transform.position.x - clocksStorage [i].transform.position.x) > 15f)
					clocksStorage [i].SetActive (false);
			}

			for(int i = 0; i < coinsStorage.Length ; i++)
			{
				if ((monster.transform.position.x - coinsStorage [i].transform.position.x) > 15f)
					coinsStorage [i].SetActive (false);
			}

			for(int i = 0; i < obsStorage.Length ; i++)
			{
				if ((monster.transform.position.x - obsStorage [i].transform.position.x) > 15f) {
					//por si se habían desactivados como rigidbody
					obsStorage [i].GetComponent<Rigidbody> ().isKinematic = false;
					obsStorage [i].GetComponent<Collider> ().enabled = true;
					obsStorage [i].SetActive (false);
				}
			}


		}
	}

	void ChangePos(GameObject thing){

		switch (Mathf.FloorToInt(Random.Range(0f,1.99f)))
		{
		case 1:
			thing.transform.position = new Vector3(thing.transform.position.x, 3, thing.transform.position.z);
			break;
		case 0:
			thing.transform.position = new Vector3(thing.transform.position.x, 1, thing.transform.position.z);
			break;
		default:
			thing.transform.position = new Vector3(thing.transform.position.x, 1, thing.transform.position.z);
			break;
		}

	}


}
