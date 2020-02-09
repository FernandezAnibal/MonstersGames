using UnityEngine;
using System.Collections;

public class NatureGenerator : MonoBehaviour {

	public GameObject monster;

	public GameObject [] natureStorage;

	public GameObject Storage;

	private int indStuff;


	private float rPos;

	//Distancia mínima
	private float minLimit;
	//distancia máxima
	private float maxLimit;
	private float distance;

	// Use this for initialization
	void Start () {

		if(transform.gameObject.name == "NatureGenerator"){
			minLimit = 10f;
			maxLimit = 19f;
			distance = Random.Range(7f, 10f);
		}

		if(transform.gameObject.name == "RockGenerator"){
			minLimit = 4.5f;
			maxLimit = 9f;
			distance = Random.Range(2f, 6f);
		}



		monster = GameObject.FindGameObjectWithTag ("Player");

		natureStorage = new GameObject[Storage.transform.childCount];
		for (int i = 0; i < Storage.transform.childCount; i++){
			//natureStorage [i] = transform.GetChild (i).gameObject;
			//GameObject obj = (GameObject)Instantiate (Storage.transform.GetChild (i).gameObject);
			//obj.SetActive (false);
			natureStorage[i] = Storage.transform.GetChild (i).gameObject;

		}

	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position = new Vector3 (monster.transform.position.x + 20f,transform.position.y,transform.position.z);

		// si se mueve a una distancia del punto de referencia activa el cambio de posición
		if ((transform.position.x - rPos) > distance) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, (minLimit + Random.Range (0f, maxLimit)));
			rPos = transform.position.x;


			//busca un objeto al azar en l array que este activo si al azar saltan 5 veces se sale para no sobre cargar el procesador
			int indStuffR = 0;
			while (natureStorage [indStuff].activeSelf == true && indStuffR < 5) {
				indStuff = Mathf.FloorToInt (Random.Range (0, natureStorage.Length - 1));
				indStuffR++;
			}


			// activa el objeto en posición
			if (natureStorage [indStuff].activeSelf == false) {

				if (Mathf.FloorToInt (Random.Range (0, 100f)) <= 80) { 
					natureStorage [indStuff].transform.position = transform.position;
					natureStorage [indStuff].transform.rotation = new Quaternion (transform.rotation.x, Random.rotation.y, transform.rotation.z, transform.rotation.w);
					float randomScale;
					randomScale = Random.Range (1, 3);
					natureStorage [indStuff].transform.localScale = new Vector3 (randomScale, randomScale, randomScale);
					natureStorage [indStuff].SetActive (true);

				}
			}

			//desaparece si esta muy lejos hacia atras
			for (int i = 0; i < natureStorage.Length; i++) {
				if ((monster.transform.position.x - natureStorage [i].transform.position.x) > 15f) {
					natureStorage [i].SetActive (false);
				}
			}
		}

	}
}
