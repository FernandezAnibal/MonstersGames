using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Arbitro : MonoBehaviour {

	//Tiempo
	public float time;

	//Boton de pausa (Cambiar esto mas adelante para que la busqueda sea automatica)
	public Button pausaButton;

	//Visor de distancia restante para el checkpoint
	public Text textGoal;

	//Visor de distancia para el checkpoint
	public Text distanceToGoalT;

	//Valor de distancia para el checkpoint
	private float distanceToGoal;

	//Tiempo de para iniciar la partida
	public float timeStart;

	//Control de escena
	SceneControl sceneControl;
	Control control;

	//Player
	public GameObject monster;
	public float positionP;

	public Text tiempoText = null;
	public Image chargeSlider;
	public Slider powerSlider;
	public Slider energySlider;
	public Slider speedSlider;
	public Text multiText;
	public Text timeStartT;
	public bool timeStop;
	public bool fail;
	private string memoryCount;
	private float timeToReset;

	//Tiempo de reacción
	private float timeToReact;
	//Para establecer el tiempo de reacción
	private bool checkItReact;

	//Tiempo que esta en cuenta regresiva
	public float disTime;
	//Booleano para decir que el tiempo regresivo se detenga
	private bool distTimeStop;
	//Tiempo de renovacion
	private float rDisTime;

	//******************* Variables para manejos de stats*********************************
	//Texto en pantalla para mostrar los stats
	public Text statsPointsText;
	//Distancia para el bonus de habilidad
	public int distanceStats;
	//Posición de referencia para los stats
	public float rPosStats;
	//Cantidad de puntos por distancia
	public int statsPoints;
	//Contador de veces que se le da al bonus
	public int bonusCounter;
	//Cuantas veces debe darse al bonus para sumar un punto
	public int bonuscheck;
	//*************************************************************************************

	//Distancia para renovación del tiempo
	private int distanceTimeR;
	//Posición de referencia para la renovación de tiempo
	public float rPosTime;

	//Establecer un tiempo de recovery en caso de que haya posibilidades de llegar a la meta despues de haberse acabado el tiempo
	public float recoveryTime;

	//Booleano que define un evento fijo para terminar la partida;
	public bool GameOver;

	//Animacion de panel Final
	public Animator finishAnim;

	//Dinero inicial
	public float MoneyArbitro;
	//Visor de Dinero final
	public Text coinText;

	//
	public Text DistanceT;

	// Use this for initialization
	void Start () {


		//Se puede pensar en modificarlo en un futurop de acuerdo a los stats del jugador
		bonuscheck = 3;
		bonusCounter = 0;
		recoveryTime = 1f;
		distanceStats = 100;
		rPosStats = 0;
		distanceTimeR = 100;
		rPosTime = 0;
		statsPoints = 0;
		//Tiempo Inicial
		disTime = AllData.data.upgradeData[4];
		//Tiempo de Recarga
		rDisTime = AllData.data.upgradeData[5];
		//Dinero Inicial
		MoneyArbitro = AllData.data.money;

		checkItReact = true;
		GameOver = false;

		monster = GameObject.FindGameObjectWithTag ("Player");
		sceneControl = GameObject.FindGameObjectWithTag("SceneControl").GetComponent<SceneControl> ();
		control = GameObject.Find ("Control").GetComponent<Control> ();
		positionP = monster.transform.position.x;
		distTimeStop = false;
		timeStop = false;
		timeStart = 3f;
		fail = false;

		//Imprimir distancia al checkpoint inicial
		if (AllData.data.gameLevel == 5) {
			textGoal.gameObject.SetActive (true);
			textGoal.text = "First Checkpoint: " + distanceTimeR.ToString ();
			distanceToGoalT.text = distanceTimeR.ToString ();
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (Time.timeScale == 0){
			timeStartT.gameObject.SetActive (false);
		}
		// Si arranca antes del timestart comete falta
		if (timeStart > 0 && monster.transform.position.x >= 0) {
			fail = true;
		} 

		//Grabar el tiempo de reacción
		if (positionP == monster.transform.position.x) {
			timeToReact = time;
		} else {


			if (checkItReact) {
				if (timeToReact >= 0.1 && timeToReact <= 0.2)
					statsPoints++;
				if (timeToReact >= 0.05 && timeToReact <= 0.1)
					statsPoints += 2;
				if (timeToReact >= 0.0 && timeToReact <= 0.05)
					statsPoints += 3;
				checkItReact = false;
			}
				
			
		}
		//si no falla else tiempoText corre normal
		if (!fail) {
			//Imprimir los números de partida
			if (timeStart > 0)
				timeStartT.text = Mathf.CeilToInt (timeStart).ToString ();

			// Devuelve el numero a un tamaño pequeño
			if (memoryCount != timeStartT.text) {
				timeStartT.fontSize = 5;
			}

			//Siempre almacena el número para volverlo pequeño cuando cambia
			memoryCount = timeStartT.text;

			//Agranda el número
			if (time < 0.5)
				timeStartT.fontSize = timeStartT.fontSize + 3;
			//Lo desaparece cuando llega a un valor
			if (time > 0.5f)
				timeStartT.gameObject.SetActive (false);


			if (timeStart <= 0 && !timeStop) {
				timeStartT.text = "Go!";
			}

		} else {
			// si falla se usa el mismo texto que se utilizaba para las letras se utiliza para escribir faul
			timeStartT.fontSize = 155;
			timeStartT.text = "FAUL";
			timeToReset += Time.deltaTime;

			if (timeToReset >= 1)
				sceneControl.ResetScene ();	

		}
			

		if (AllData.data.gameLevel <= 4) {

			PrintTimeFormat(time);

		}

		if (AllData.data.gameLevel == 5) {
			//Imprimir el tiempo en el formato para pantalla
			PrintTimeFormat(disTime);

			distanceToGoal = (Mathf.Floor (distanceTimeR -(monster.transform.position.x-rPosTime)));
			if (monster.transform.position.x> 0 ) {
				distanceToGoalT.text = (Mathf.FloorToInt (distanceToGoal)).ToString ();
			}


			// si cumple con la meta de las distancia entonces se le suma tiempo y se plantea una nuueva distancia a recorrer
			if((monster.transform.position.x - rPosTime) > distanceTimeR ){
				disTime += rDisTime;
				distanceTimeR += 100;
				textGoal.text = "Next Checkpoint: " + Mathf.FloorToInt(rPosTime+ distanceTimeR).ToString();
				rPosTime = monster.transform.position.x;

				//Activar el personaje en caso de que despues de apagarlo pasa la meta
				if (distTimeStop) {
					control.goChar ();
					distTimeStop = false;
				}

			}

			if (disTime <= 0 ){
				control.stopChar ();
				distTimeStop = true;
				disTime = 0;
			}
		}


		//***********************************************Sección para subir los puntos de stats***************************************************************************
		if((monster.transform.position.x - rPosStats) > distanceStats ){
			statsPoints++;
			rPosStats = monster.transform.position.x;
		}
	


		}

	void FixedUpdate() {
		// Conteo de tiempo inicial
		if (timeStart > 0)
			timeStart -= Time.deltaTime;

		//Contador de tiempo ascendente
		if (timeStart <= 0 && !timeStop) {
			time += Time.deltaTime;
		}

		//contador de tiempo descendente
		if (AllData.data.gameLevel == 5 && !distTimeStop) {
			if (timeStart <= 0 && !timeStop) {
				disTime -= Time.deltaTime;
			}
		}

		//En caso de que se termine se descuenta el tiempo de recovery para ver si tiene la oportunidad de recuperarse
		if (distTimeStop) {
			if (recoveryTime >= 0) {
				recoveryTime -= Time.deltaTime;
			} else { // aquí va el comando que termina la partida completa ***********************************************************
				if (!GameOver){
					FinishGame ();
					GameOver = true;
				}
			}
		}

	}

	//Sube puntos en función de cuantas veces activa ell bonus
	public void BonusStats(){
		
		if (bonusCounter == (bonuscheck-1)) {
			statsPoints++;
			bonusCounter = 0;
		} else {
			bonusCounter++;
		}
					
	}

	//Se le suma el error que tiene que al llegar a la meta no salva el último valor de stats
	public void FinishGame() {
		
		AllData.data.LoadData ();
		AllData.data.monstersList[AllData.data.monsterPref].statsPoints += statsPoints;
		AllData.data.SaveData();
		AllData.data.saveMoney ();
		pausaButton.interactable = false;
		statsPointsText.text = "Stats Gain: " +statsPoints.ToString ();
		coinText.text = "Coins Collected: " + (AllData.data.money - MoneyArbitro).ToString ();
		DistanceT.text ="Distance: " + Mathf.FloorToInt (monster.transform.position.x).ToString();
		finishAnim.SetBool ("IsDisplayed", true);
	}


	//Función para imprimir en pantalla

	public void PrintTimeFormat(float time1){

		int minutes = (Mathf.FloorToInt(time1/60f))%60 ;
		int seconds = Mathf.FloorToInt(time1%60);
		int fraction = Mathf.FloorToInt (time1 * 100f);
		fraction = fraction % 100;
		string	niceTime = string.Format ("{0:0}:{1:00}:{2:00}", minutes, seconds, fraction);
		tiempoText.text = niceTime;
	}

}
