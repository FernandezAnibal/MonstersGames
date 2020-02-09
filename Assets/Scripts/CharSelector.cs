using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class CharSelector : MonoBehaviour {

	public List<GameObject> characterList;
	public int index ;
	public GameObject[] characters;

	//Boton para cambiarlo
	public Text SelectChar;

	//Datos para llenar
	public Text nombre;
	public Slider maxSpeed;
	public Slider maxPower;
	public Slider powerCharge;
	public Slider powerForce;
	public Text stats;

	//Mostrar el dinero
	public Text moneyT;

	//Apagar Luces

	// Use this for initialization
	void Start () {
		moneyT.text ="Money: " + AllData.data.money.ToString();
		index = AllData.data.monsterPref;

		//Cargar de la carpeta todos los mounstros
		 characters = Resources.LoadAll<GameObject>("Characters");

		//Instanciar cada mounstro como gameobjects y meterlos dentro del objeto Characterlist
		foreach (GameObject c in characters){

			GameObject monster = Instantiate (c) as GameObject;
			monster.transform.SetParent (GameObject.Find ("CharacterList").transform);

			characterList.Add (monster);
			monster.SetActive (false);

		}

		characterList [index].SetActive (true);
		AllData.data.LoadData ();
		FillData (index);
		checkIfSelected ();
	
	}


	public void NextMonster(){
	
		characterList [index].SetActive (false);

		if (index == characterList.Count - 1) {
			index = 0;
		} else {
			index++;
		}
		AllData.data.LoadData ();
		FillData (index);
		characterList [index].SetActive (true);
		checkIfSelected ();

	}

	public void PreviousMonster(){

		characterList [index].SetActive (false);

		if (index == 0) {
			index = characterList.Count - 1;
		} else {
			index--;
		}
		AllData.data.LoadData ();
		FillData (index);
		characterList [index].SetActive (true);
		checkIfSelected ();
	


	}
	
	// Update is called once per frame
	void Update () {
	
		//characterList [index].transform.Rotate (0, -0.5f, 0, 0);

	}



	void FillData(int ID){
		
		nombre.text = AllData.data.monstersList [ID].name;
		//Definir las secciones para que sea cuantificado el aumento
		maxSpeed.value = (AllData.data.monstersList[ID].speed-5)*2;
		maxPower.value = AllData.data.monstersList[ID].power - 10;
		powerCharge.value = 10- (2-AllData.data.monstersList[ID].powerCharge)*10;
		powerForce.value = 10-(2.5f - AllData.data.monstersList[ID].powerForce)*10;

		//Stats
		stats.text = "StatsPoints: " + AllData.data.monstersList[ID].statsPoints.ToString();
			
	}


	//*****Botones para Cambio la Velocidad ***********
	public void UpMaxSpeed(){
		if (maxSpeed.value < 10 && AllData.data.monstersList [index].statsPoints >= ((maxSpeed.value+1)*(maxSpeed.value+1)) ) {
			AllData.data.monstersList [index].statsPoints -= ((maxSpeed.value+1)*(maxSpeed.value+1));
				AllData.data.monstersList [index].speed += 0.5f;
				FillData (index);
		}
	
	}

	public void DownMaxSpeed(){

		if (maxSpeed.value > 0){
				AllData.data.monstersList [index].speed -= 0.5f;
				FillData (index);
		}
	}

	//*****Botones para Cambio la Poder Maximo ***********
	public void UpMaxPower(){

		if (maxPower.value < 10 && AllData.data.monstersList [index].statsPoints >= ((maxPower.value+1)*(maxPower.value+1))){
			AllData.data.monstersList [index].statsPoints -= ((maxPower.value+1)*(maxPower.value+1));
			AllData.data.monstersList[index].power += 1f;
			FillData (index);
		}

	}

	public void DownMaxPower(){

		if (maxPower.value > 0){
			AllData.data.monstersList[index].power -= 1f;
			FillData (index);
		}

	}

	//*****Botones para Cambio la carga de poder ***********
	public void UpPowerCharge(){
		if (powerCharge.value < 10 && AllData.data.monstersList [index].statsPoints >= ((powerCharge.value+1)*(powerCharge.value+1))){
			AllData.data.monstersList [index].statsPoints -= ((powerCharge.value+1)*(powerCharge.value+1));
			AllData.data.monstersList[index].powerCharge += 0.1f;
			FillData (index);
		}

	}

	public void DownPowerCharge(){

		if (powerCharge.value > 0){
			AllData.data.monstersList[index].powerCharge -= 0.1f;
			FillData (index);
		}

	}

	//*****Botones para Cambio la fuerza de poder ***********
	public void UpPowerForce(){
		if (powerForce.value < 10 && AllData.data.monstersList [index].statsPoints >= ((powerForce.value+1)*(powerForce.value+1))){
			AllData.data.monstersList [index].statsPoints -= ((powerForce.value+1)*(powerForce.value+1));
			AllData.data.monstersList[index].powerForce += 0.1f;
			FillData (index);
		}

	}

	public void DownPowerForce(){

		if (powerForce.value > 0){
			AllData.data.monstersList[index].powerForce -= 0.1f;
			FillData (index);
		}

	}

	public void SaveStats(){
	
		AllData.data.SaveData();
	}

	public void safeSelected(){
		// comprueba si esta disponible. si esta disponible deja seleccionarlo, si no tiene que comprarlo
		if (AllData.data.monstersList [index].availableM == 1) {
			PlayerPrefs.SetInt ("MonsterPref", index);
			AllData.data.monsterPref = index;
			AllData.data.SaveSelectedMonster ();
			SelectChar.text = "Selected";
		} else {
			if (AllData.data.money >= AllData.data.monstersList [index].price) {
				AllData.data.money -= AllData.data.monstersList [index].price;
				AllData.data.monstersList [index].availableM = 1;
				SelectChar.text = "Select";
				AllData.data.saveMoney ();
				AllData.data.SaveData ();
				moneyT.text ="Money: " + AllData.data.money.ToString();
			} else {
				Debug.Log ("No hay money");
			}
		
		}

	}

	void checkIfSelected() {

		if (AllData.data.monstersList [index].availableM == 1) {
			if (index == AllData.data.monsterPref) {
				SelectChar.text = "Selected";
			} else {
				SelectChar.text = "Select";
			}
		} else {
		
			SelectChar.text ="Buy";
		}

	}

}
