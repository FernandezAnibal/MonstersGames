using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FindData : MonoBehaviour {

	public Button g100m;
	public Button g200m;
	public Button ghurdles;
	public Button g1500m;
	public Button infiniteTrack;

	// Use this for initialization

	void Awake (){

		//Se utiliza para seleccionar el nivel, utilizando esta función se accesa a cuando se apretan los botones

		g100m.onClick.AddListener (() => {
			selectgame (1);
		});
		g200m.onClick.AddListener (() => {
			selectgame (2);
		});
		ghurdles.onClick.AddListener (() => {
			selectgame (3);
		});
		g1500m.onClick.AddListener (() => {
			selectgame (4);
		});
		infiniteTrack.onClick.AddListener (() => {
			selectgame (5);
		});
	}

	void Start () {
	
	}
	
	void selectgame (int value){

		AllData.data.ChangeGame (value);
	}

	// Update is called once per frame
	void Update () {
	

	}


}
