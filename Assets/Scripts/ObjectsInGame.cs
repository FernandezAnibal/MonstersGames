using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectsInGame : MonoBehaviour {

	//public GameObject oHurdles;
	public GameObject meta100m;
	public GameObject meta110m;
	public GameObject meta200m;
	public GameObject meta1500m;
	public Button jumpB;
	private int gameSelected;


	// Use this for initialization
	void Start () {
		gameSelected = AllData.data.gameLevel;

		//Cargar los objetos segun el juego seleccionado
		switch (gameSelected)
		{
		case 4:
			jumpB.gameObject.SetActive (true);
			meta1500m.gameObject.SetActive (true);
			break;
		case 3:
			//oHurdles.SetActive (true);
			jumpB.gameObject.SetActive (true);
			meta110m.gameObject.SetActive (true);
			break;
		case 2:
			meta200m.gameObject.SetActive (true);
			break;
		case 1:
			meta100m.gameObject.SetActive (true);
			break;
		default:
			//oHurdles.SetActive (true);
			jumpB.gameObject.SetActive (true);
			meta110m.gameObject.SetActive (true);
			break;
		}



	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
