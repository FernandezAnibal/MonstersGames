using UnityEngine;
using System.Collections;

public class AddTime : MonoBehaviour {

	Arbitro arbitro;

	//Tiempo por cada reloj tomado
	public float plusTime;

	// Use this for initialization
	void Start () {

		arbitro = GameObject.FindGameObjectWithTag("Arbitro").GetComponent<Arbitro> ();
		plusTime = AllData.data.upgradeData[2];;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 2f, 0, 0);
	}

	void OnTriggerEnter(Collider other){
	
		if (other.gameObject.tag == "Player") {
			arbitro.disTime += plusTime;
			gameObject.SetActive (false);
		}
	
	}
}
