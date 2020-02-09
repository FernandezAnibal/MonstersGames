using UnityEngine;
using System.Collections;


public class Monsters {

	public string name;
	public float speed;
	public float power;
	public float powerCharge;
	public float powerForce;
	public float statsPoints;
	public float availableM;
	public float price;

	public Monsters(string newName, float newSpeed, float newPower, float newPowerCharge, float newPowerForce, float newStatsPoints, float newAvailableM, float newPrice){
	
		name = newName;
		speed = newSpeed;
		power = newPower;
		powerCharge = newPowerCharge;
		powerForce = newPowerForce;
		statsPoints = newStatsPoints;
		availableM = newAvailableM;
		price = newPrice;
	
	}

}
