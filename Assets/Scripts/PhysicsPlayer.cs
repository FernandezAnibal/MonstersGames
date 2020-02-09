using UnityEngine;
using System.Collections;

public class PhysicsPlayer : MonoBehaviour {

	Control control;
	// Use this for initialization
	void Start () {

		if (GameObject.Find ("Control") != null)
		control = GameObject.Find ("Control").GetComponent<Control> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate () {

	}

	void  OnCollisionEnter (Collision col){



	
	
	}

	void OnCollisionStay (Collision col){

		if (col.gameObject.tag == "Obstaculo" ) {
			//Mide la distancia entre el objeto y el player
			Vector3 dir = (col.gameObject.transform.position - gameObject.transform.position);
			if (dir.x > 0) {
				if (!control.powerOn) {
					control.fObstaculo = 0.75F;
				} else {
					if (AllData.data.monsterPref == 0 || AllData.data.monsterPref == 3) {
						col.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
						col.gameObject.GetComponent<Collider> ().enabled = false;
					}
					col.rigidbody.AddForce (new Vector3(1,0,0) * 20f * control.totalSpeed );
					col.rigidbody.AddForce (new Vector3(0,1,0) * control.totalSpeed*  10f );
					col.rigidbody.AddForce (dir.normalized * 10f * control.totalSpeed );
				}
			}
		}

	}

	void  OnCollisionExit (Collision col){

		control.fObstaculo = 1F;
			
	}


}
