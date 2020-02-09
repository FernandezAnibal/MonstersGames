using UnityEngine;
using System.Collections;

public class FollowChar : MonoBehaviour {

	public GameObject personaje;


	// Use this for initialization
	void Start () {
	
		personaje = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void FixedUpdate () {


		transform.position = Vector3.Lerp(transform.position,(new Vector3(personaje.transform.position.x + 8f, personaje.transform.position.y + 7.2f, transform.position.z)), Time.deltaTime*5f);
	
	}
}
