using UnityEngine;
using System.Collections;

public class AddMoney : MonoBehaviour {

	//dinero que se agregan con cada moneda
	public float plusMoney;

	// Use this for initialization
	void Start () {
		
		plusMoney = AllData.data.upgradeData[3];;

	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 2f, 0, 0);
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag == "Player") {
			AllData.data.addMoney (plusMoney);
			gameObject.SetActive (false);
		}

	}
}
