using UnityEngine;
using System.Collections;

public class InfiniteConstructor : MonoBehaviour {


	//Personaje
	public GameObject monster;
	public GameObject pista1;
	public GameObject pista2;
	private float rPos;
	private float distance;

	// Use this for initialization
	void Start () {
		rPos = 0;
		// mide la distancia entre el centro de los dos y la toma para aparecer uno delante del otro exactamente
		distance = (pista1.GetComponent<BoxCollider> ().size.x + pista2.GetComponent<BoxCollider> ().size.x)/2;
		pista1.transform.position = new Vector3 (0, 0, 0);
		monster = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {


		//asegura que la distancia entre el mounstro y el centro de la pita sea menor a 20
		if ( Mathf.Abs(pista1.transform.position.x - monster.transform.position.x) < 20 && (rPos-pista1.transform.position.x) < 20 ) {
				pista2.transform.position = new Vector3 ((rPos + distance), 0, 0);
				rPos = pista2.transform.position.x;
			}

		if ( Mathf.Abs(pista2.transform.position.x - monster.transform.position.x) < 20 && (rPos-pista2.transform.position.x) < 20 ) {
			pista1.transform.position = new Vector3 ((rPos + distance), 0, 0);
			rPos = pista1.transform.position.x;
			}


	}
}
