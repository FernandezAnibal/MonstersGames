using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Control : MonoBehaviour {

	//Personaje
	public GameObject monster;

	//Datos del Personaje

	//Velocidad Máxima
	public float maxSpeed;
	//Energía
	public float maxEnergy;
	//Carga de poder
	public float powerCharge;
	//PoderMaximo
	public float maxPower;

	//Energía Disponible
	public float currentEnergy;
	//Activar descarga de Energía
	public bool energyOn = false;

	//Tiempo de recarga para bonus
	private bool rechargeOn = false;
	private float rechargeTime;
	private float maxrechargeTime = 0.3f;

	//Penalización por recarga
	private float faulRecharge;

	//Penalización por choque
	public float fObstaculo;

	//Definicion de variables de poder especial
	public bool powerOn = false;
	private float currentPower;
	private float powerMultiplier = 1f;
	public float powerForce;



	//Arbitro
	Arbitro arbitro;

	//Animacion
	static Animator anim;
	private bool powerB = false;


	//Botones Ui
	public Button buttonGo;

	//Fuerza de Salto
	public float jumpSpeed ;
	public float gravity ;

	//Parametros de tiempo y velocidad y salto
	private bool jumper = false;
	private float speed = 0F;
	//Velocidad con multiplicadores y penalizaciones 
	public float totalSpeed;


	//Activación y creación de control de personaje
	private bool controlActivated = true;
	private CharacterController controller;
	private Vector3 moveDirection = Vector3.zero;

	//Variable de selección de canal preferido
	public int prefChannel;
	public int monsterPref;

	//Variable para siempre mantener la posición
	private Vector3 rightPos;


	void Awake (){



	}

	void Start () {
		faulRecharge = 1f;
		jumpSpeed = 10F;
		gravity = 25F;
		fObstaculo = 1f;

		//Busca el nombre de la ruta en el objeto que tiene todas las variables
		arbitro = GameObject.FindGameObjectWithTag("Arbitro").GetComponent<Arbitro> ();
		monster = GameObject.FindGameObjectWithTag ("Player");


		// Grabar posición
		rightPos = monster.transform.position;



		//Tomar los objetos necesarios del player para controlar movimiento y animacion

		anim = monster.GetComponent<Animator> ();

		controller = monster.GetComponent<CharacterController>();

		// OJO Inicialización Tomar Energía, Velocidad, y Carga de poder del mounstro
		// MaxSpeed entre 5 y 10.
		// Powercharge entre 1 y 2
		// MaxPower entre 10 y 20
		// PowerForce 1.5 y 2.5
		monsterPref = AllData.data.monsterPref;
		AllData.data.LoadData ();

		maxEnergy = 100f;
		//maxSpeed = 5f;
		maxSpeed = AllData.data.monstersList[monsterPref].speed;
		//maxPower = 10f;
		maxPower = AllData.data.monstersList[monsterPref].power;
		//powerCharge = 1f;
		powerCharge = AllData.data.monstersList[monsterPref].powerCharge;
		//powerForce = 1.5f;
		powerForce = AllData.data.monstersList[monsterPref].powerForce;

		//Decirle al visor las propiedades del mounstro
		arbitro.energySlider.maxValue = maxEnergy;
		arbitro.powerSlider.maxValue = maxPower;
		//arbitro.speedSlider.GetComponent<RectTransform> ().sizeDelta = new Vector2 (maxSpeed*30f , arbitro.speedSlider.GetComponent<RectTransform> ().sizeDelta.y );
		arbitro.speedSlider.maxValue = maxSpeed;

		//Poner la energía al máximo al inicio
		currentEnergy = maxEnergy;

		//Comprobar si un parametro existe para ejecuta una acción u otra
		foreach ( AnimatorControllerParameter parametro in anim.parameters){

			if (parametro.name == "isPower") {
				powerB = true;
			} else {
				powerB = false;
			}

		}


	}


	void FixedUpdate (){
		//Corregir posición en caso de que se salga del canal
		if (monster.transform.position.z != rightPos.z){
			monster.transform.position = Vector3.Lerp (monster.transform.position, new Vector3 (monster.transform.position.x, monster.transform.position.y, rightPos.z), 2f);
		}
		//Velocidad total con penalización y multiplicadores
		totalSpeed = speed * powerMultiplier * faulRecharge * fObstaculo;

		//Gravedad
		moveDirection.y -= gravity * Time.deltaTime;

		//Activar movimiento
		controller.Move (moveDirection * Time.deltaTime);

		//Lógica de movimiento
		if (controller.isGrounded) {

			moveDirection = new Vector3(1,0,0);
			moveDirection *= totalSpeed;

			//Finalizar con stopChar()
			//Salto
			if ((Input.GetKeyDown (KeyCode.C) || jumper) && controlActivated) {
				moveDirection.y = jumpSpeed;
				anim.SetTrigger ("isJumping");
			}
				
		}
			
  
		// Reducción de velocidad
		if (speed >= maxSpeed*0.3f && controller.isGrounded && !powerOn && !energyOn && !rechargeOn ) {
			SpeedDown (100);
		} 
		//Si ya no se controla al personaje la velocidad baja mas rápido
		if (controlActivated == false)
			SpeedDown (30f);

		//Aumento de Velocidad
		if (energyOn) {
			currentEnergy += Time.deltaTime * 35f;
			addVelocity ();
		} else {
		}

		//Cuando se llena la energía se detiene la velocidad constante y se activa el tiempo de bonus
		if (currentEnergy >= maxEnergy) {
			energyOn = false;
			rechargeOn = true;

			//************************************************************************************************** Automata
			/*
			if(arbitro.time !=0)
			EnergyActivate ();
			*/

		} 
		//Cuando pasa el tiempo de bonus se apaga
		if (rechargeTime <= 0) {
			rechargeOn = false;
			//superponer imagen para indicar que esta en tiempo de bonus
		}

		if (rechargeOn == true && arbitro.powerSlider.value < maxPower) {
			arbitro.chargeSlider.gameObject.SetActive (true);
		} else {
			arbitro.chargeSlider.gameObject.SetActive (false);
		}

		//Cuando śe consume la energía se restituye el tiempo de bonus
		if (energyOn) {
			rechargeTime = maxrechargeTime;
		}

		//Disminución del tiempo de recarga
		if (rechargeTime > 0 && rechargeOn == true) {
			rechargeTime -= Time.deltaTime; 

		}
			
		//Manejo del poder

		if (powerOn) {
			//Imprime en pantalla el texto de multiplicación
			arbitro.multiText.enabled = true;
			arbitro.multiText.text = "X"+ powerForce.ToString();
			powerMultiplier = powerForce;
			currentPower -= Time.deltaTime*2f; 
			//Sin penalización cuando esta el poder activado
			fObstaculo = 1F;

		} else {
			arbitro.multiText.enabled = false;
			powerMultiplier = 1f;

		}
		//Activar el texto de en cuanto se multiplica la velocidad con el poder
		if (currentPower <= 0) {
			powerOn = false;
		}

	}

	 void Update() {



		//Impresión de tiempo en pantalla
		arbitro.powerSlider.value = currentPower;
		arbitro.energySlider.value = currentEnergy;
		arbitro.speedSlider.value = totalSpeed;


		//Imprimir Velocidad por teclado (Solo para fines de programación en pc)
		if (Input.GetKeyDown (KeyCode.Z))
			buttonGo.onClick.Invoke ();

		//Detener al llegar a la meta

		
		/* Animaciones */

		//Animacion para correr segun si el poder está activado o no
		if (speed != 0 /*&& controller.isGrounded*/) {

			if (!powerOn) {
				anim.SetBool ("isRunning", true);

				if(powerB)
				anim.SetBool ("isPower", false);
				DEffect (false);
			}

			else
			{
				if(powerB)
				anim.SetBool ("isPower", true);
				DEffect (true);

			}


		}
		else
		{
			anim.SetBool ("isRunning",false);
			anim.SetBool ("isIdle", true);
		}

		// Para ajustar la velocidad de la animación según la velocidad del personaje
		anim.SetFloat ("runMultiplier", (totalSpeed)/5);

		//Animación de estar quieto
		if (speed == 0) {
			anim.SetBool ("isIdle", true);
		} else
		{
			anim.SetBool ("isIdle", false);
		}

	}

	//LLegada a la meta
	//función para detener al personaje
	public void stopChar ()
	{
		controlActivated = false;
		powerOn = false;
		energyOn = false;
		rechargeTime = 0;
		currentEnergy = 0;
		rechargeTime = 0;
	//	arbitro.timeStop = true;
	}

	public void goChar ()
	{
		controlActivated = true;
		rechargeTime = 0;
		currentEnergy = maxEnergy;
		currentPower = 0;
		rechargeTime = 0;
		//	arbitro.timeStop = true;
	}

	//Función para activar energía
	public void EnergyActivate(){

		/*
		if (arbitro.timeStart > 0) {
			stopChar ();
			arbitro.fail = true;


		} else */

		if (currentEnergy >= maxEnergy * 0.8 && controlActivated) {
			faulRecharge = currentEnergy / maxEnergy;
			energyOn = true;
			currentEnergy = 0f;
			addPower ();
			arbitro.chargeSlider.gameObject.SetActive (false);
			rechargeOn = false;
		}




	}

	//Función para activar poder
	public void PowerActivate ()
	{
		if (currentPower > 0 && controlActivated && !powerOn) {
			powerOn = true;
			OnceEffect ();
		}
	}

	//Funcion para efectos que se ejecutan una vez y quedan en pantalla
	public void OnceEffect ()
	{
		if (!monster.transform.Find ("Effects1").gameObject.activeSelf) {
			monster.transform.Find ("Effects1").gameObject.SetActive (true);
		} else {
			monster.transform.Find ("Effects1").gameObject.SetActive (false);
			monster.transform.Find ("Effects1").gameObject.SetActive (true);
		}

	}

	//Ejecutar efectos que estan mientras dure el poder
	public void DEffect (bool act)
	{
		monster.transform.Find ("Effects2").gameObject.SetActive (act);

	}


	//Función para añadir poder
	public void addPower()
	{
		//Si se carga el poder cuando esta en el tiempo de bonus se carga dos veces
		if (currentPower + powerCharge >= maxPower) {
			currentPower = maxPower;				
		} else {
			currentPower += powerCharge;
			if (rechargeOn) {
				arbitro.BonusStats ();
				//Si la carga con bonus sobrepasa el maximo poder entonces solo llena la barra
				if (currentPower + powerCharge >= maxPower) {
					currentPower = maxPower;
				} else {
					currentPower += powerCharge;

				}
			}
		}


		if (powerOn)
			OnceEffect ();
	}



	//Función para imprimir Velocidad
	public void addVelocity()
	{
		if (speed <= (maxSpeed) && controlActivated && controller.isGrounded && !powerOn)
			if (speed <= 1.5) 
			{
				speed += 1.6f;
			}
			else
			{
				speed += 0.5f;
			}
	}
		

	//Función para reducir velocidad
	public void SpeedDown (float speedRedux)
	{
	
		if (speed >= 1.5f) {
			speed -= speed / (speedRedux);

		} else {
			speed -= speed / 10f;
			if (speed <= 0.5f)
				speed = 0;
		}

	}
	//Función para activar salto por UI al pulsar el boton
	public void JumpUp ()
	{
		jumper = true;
	}

	//Función para activar salto por UI al pulsar el boton
	public void NoJumpUp ()
	{
		jumper = false;
	}


	


}
