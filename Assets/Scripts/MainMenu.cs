using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Text moneyText;
	// Use this for initialization
	void Start () {

		moneyText.text = "Money: " + AllData.data.money.ToString();
	
	}

	public void resetAll (){
		AllData.data.ResetAllData ();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
