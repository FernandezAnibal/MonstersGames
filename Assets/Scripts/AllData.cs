using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class AllData : MonoBehaviour {

	public static AllData data;


	/*
	 Array para el almacenamiento de la data de los jugadores 
	 [Nombre,MaxSpeed,MaxPower,PowerChargue,PowerForce, StatsPoints, Disponibilidad, Precio]

	para añadir un nuevo personaje cambiar el tamaño de monsterDataArray
	*/

	public float[,] monstersData; 

	//lista para almacenar una default y una que se usa para comparar con la que ya existe
	public List<Monsters> monstersList = new List<Monsters> ();
	public List<Monsters> monstersListD = new List<Monsters> ();
	/*
	 Array para almacenar el dinero y las mejoras que sean compradas con dinero 
	[Frecuencia de Tiempo, Frecuencia de Monedas, Cantidad de Tiempo, Cantidad de Monedas, Cantidad de tiempo Inicial, Cantidad de tiempo de Recarga]
	*/
	public float[] upgradeData;

	//Almacena la selección del juego, se hizo así porque en la misma escena se presentan varios juegos
	public int gameLevel;

	//Almacena el canal preferido del jugador
	public int prefChannel;

	//Almacena el mounstro preferido por el jugador
	public int monsterPref;

	//Almacena el dinero
	public float money;


	void Awake(){

		//Para poner valores default la primera corrida
		if ( PlayerPrefs.GetInt("firstTime") == 0) {
			DirectoryInfo dataDir = new DirectoryInfo (Application.persistentDataPath);
			dataDir.Delete (true);
			prefChannel = 0;
			monsterPref = 0;
			money = 0;
			SaveSelectedMonster ();
			saveMoney ();
			Debug.Log ("FirstTime");
			PlayerPrefs.SetInt ("firstTime", 1);
		}


		//Crea dos listas una por default y la que se usara en el juego
		DefaultDataList ();
		//Carga la información si existe en la lista que se usara en el juego y la lista por default queda intacta
		LoadData();
		loadMoney ();
		LoadSelectedMonster ();
		loadUpgrades ();
	
		//Se compara se compara el tamaño de una lista con el tamaño de otra para ver si se inclueron nuevos personajes
		if (monstersList.Count == monstersListD.Count) {

		} else {

		}
		//Para que no se destruya con cada paso de escena
		if (data == null) {
			DontDestroyOnLoad (gameObject);
			data = this;
		} else if(data != this) {
			Destroy (gameObject);
		}



	}
		
	public void ResetAllData(){
		PlayerPrefs.SetInt ("firstTime",0);
		Debug.Log (PlayerPrefs.GetInt("firstTime"));
		monstersList.Clear();
		DefaultDataList ();
		SaveData ();
		saveUpgrades ();
	}

	//Para saber en todo momento en que nivel se encuentra el juego sin llamar a las funciones de unity
	public void ChangeGame(int value) {
	
		gameLevel = value;


	}
	//Para salvar al jugador seleccionado

	public void SaveSelectedMonster(){
		
		PlayerPrefs.SetInt("MonsterPref", monsterPref);

		PlayerPrefs.SetInt ("ChannelPref", prefChannel);

	}

	//Cargar las preferencias del del judaodr (Mousntro y Canal Preferido)
	public void LoadSelectedMonster(){

		monsterPref = PlayerPrefs.GetInt("MonsterPref");

		prefChannel = PlayerPrefs.GetInt ("ChannelPref");

	}




	public void SaveData()
	{
		//Cargar la lista en el array para guardarlo
		int i=0;
		foreach (Monsters monster in monstersList)
		{

			monstersData [i, 0] = monstersList [i].speed;
			monstersData [i, 1] = monstersList [i].power;
			monstersData [i, 2] = monstersList [i].powerCharge;
			monstersData [i, 3] = monstersList [i].powerForce;
			monstersData [i, 4] = monstersList [i].statsPoints;
			monstersData [i, 5] = monstersList [i].availableM;
			i++;

		}


		//Crear un archivo con metodo binario
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath +Path.DirectorySeparatorChar+ "monsterDataInfo.dat");

		MonstersDataClass data = new MonstersDataClass ();
		data.monstersDataArray = monstersData;

		bf.Serialize (file, data);
		file.Close ();


	}

	public void LoadData()
	{
		//Cargar un archivo con metodo binario
		if (File.Exists (Application.persistentDataPath +Path.DirectorySeparatorChar+ "monsterDataInfo.dat")) {

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + Path.DirectorySeparatorChar+ "monsterDataInfo.dat", FileMode.Open);

			MonstersDataClass data = (MonstersDataClass)bf.Deserialize (file);
			file.Close ();

			monstersData = data.monstersDataArray;
			//descargar la información del array guardado en la lista
			for (int l = 0 ; l < monstersList.Count; l++){
				monstersList [l].speed = monstersData[l,0] ;
				monstersList [l].power = monstersData[l,1] ;
				monstersList [l].powerCharge = monstersData[l,2] ;
				monstersList [l].powerForce = monstersData[l,3] ;
				monstersList [l].statsPoints = monstersData[l,4] ;
				monstersList [l].availableM = monstersData[l,5] ;
			}

		}



	}

	public void addMoney (float value){
	
		money += value;
	
	}

	public void saveUpgrades(){

		//Crear un archivo con metodo binario
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + Path.DirectorySeparatorChar+ "monsterUpgrades.dat");

		MonstersDataClass data = new MonstersDataClass ();
		data.upgradeDataArray = upgradeData;

		bf.Serialize (file, data);
		file.Close ();

	}

	public void loadUpgrades()
	{
		//Cargar un archivo con metodo binario
		if (File.Exists (Application.persistentDataPath + Path.DirectorySeparatorChar + "monsterUpgrades.dat")) {

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + Path.DirectorySeparatorChar + "monsterUpgrades.dat", FileMode.Open);

			MonstersDataClass data = (MonstersDataClass)bf.Deserialize (file);
			file.Close ();

			upgradeData = data.upgradeDataArray;

		} 
	}

	public void saveMoney(){
	
		//Crear un archivo con metodo binario
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + Path.DirectorySeparatorChar+ "monsterMoney.dat");

		MonstersDataClass data = new MonstersDataClass ();
		data.moneyDataC = money;

		bf.Serialize (file, data);
		file.Close ();

	}

	public void loadMoney()
	{
		//Cargar un archivo con metodo binario
		if (File.Exists (Application.persistentDataPath + Path.DirectorySeparatorChar + "monsterMoney.dat")) {

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + Path.DirectorySeparatorChar + "monsterMoney.dat", FileMode.Open);

			MonstersDataClass data = (MonstersDataClass)bf.Deserialize (file);
			file.Close ();

			money = data.moneyDataC;

		} 
	}



	void DefaultDataList(){

		float maxSpeedD = 5f;
		float maxPowerD = 10f;
		float powerChargeD = 1f;
		float powerForceD = 1.5f;
		int statsPointsD = 1000;

		monstersListD.Add (new Monsters ("Drackie", maxSpeedD, maxPowerD, powerChargeD, powerForceD, statsPointsD, 1, 0));
		monstersListD.Add (new Monsters ("Frankie", maxSpeedD, maxPowerD, powerChargeD, powerForceD, statsPointsD, 1, 0));
		monstersListD.Add (new Monsters ("Jackie", maxSpeedD, maxPowerD, powerChargeD, powerForceD, statsPointsD, 0, 20));
		monstersListD.Add (new Monsters ("Mummy", maxSpeedD, maxPowerD, powerChargeD, powerForceD, statsPointsD, 0, 10));
		monstersList = monstersListD;
		monstersData = new float[monstersList.Count, 7];
		upgradeData = new float[] {5f , 5f, 1f, 1f, 9f, 6f};
	}

}


[Serializable]
class MonstersDataClass
{

	public float[,] monstersDataArray = new float[4, 7];
	public float moneyDataC;
	public float[] upgradeDataArray = new float[5];
}

