using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradesManager : MonoBehaviour {

	public Text clocksChance;
	public Text clocksChanceP;

	public Text coinsChance;
	public Text coinsChanceP;

	public Text timePerClock;
	public Text timePerClockP;

	public Text moneyPerCoin;
	public Text moneyPerCoinP;

	public Text ITIT;
	public Text ITITP;

	public Text RT;
	public Text RTP;

	public Text moneyT;

	private float initialValue;

	// Use this for initialization
	void Start () {
	
		PrintallValues ();

	}
	
	// Update is called once per frame
	public void upClockChance(){
		if (AllData.data.upgradeData [0] < 40f && AllData.data.money >= (AllData.data.upgradeData [0]*3) ){
			AllData.data.upgradeData [0] += 5f;
			AllData.data.money -= ((AllData.data.upgradeData [0]-5f) * 3);
			AllData.data.saveMoney ();
			AllData.data.saveUpgrades ();
			PrintallValues ();
		}
	}

	public void upCoinsChance(){
		if (AllData.data.upgradeData [1] < 40f && AllData.data.money >= (AllData.data.upgradeData [1]*3) ){
			AllData.data.upgradeData [1] += 5f;
			AllData.data.money -= ((AllData.data.upgradeData [1]-5f) * 3);
			AllData.data.saveMoney ();
			AllData.data.saveUpgrades ();
			PrintallValues ();
		}
	}

	public void upTimePerClock(){
		if (AllData.data.upgradeData [2] < 3f && AllData.data.money >= (AllData.data.upgradeData [2]*20) ){
			AllData.data.upgradeData [2] += 1f;
			AllData.data.money -= ((AllData.data.upgradeData [2]-1f) * 20);
			AllData.data.saveMoney ();
			AllData.data.saveUpgrades ();
			PrintallValues ();
		}
	}

	public void upMoneyPerCoin(){
		if (AllData.data.upgradeData [3] < 10f && AllData.data.money >= (AllData.data.upgradeData [3]*20) ){
			AllData.data.upgradeData [3] += 1f;
			AllData.data.money -= ((AllData.data.upgradeData [3]-1f) * 20);
			AllData.data.saveMoney ();
			AllData.data.saveUpgrades ();
			PrintallValues ();
		}
	}

	public void upITIT(){
		if (AllData.data.upgradeData [4] < 30f && AllData.data.money >= (AllData.data.upgradeData [4]*4) ){
			AllData.data.upgradeData [4] += 3f;
			AllData.data.money -= ((AllData.data.upgradeData [4]-3f) * 4);
			AllData.data.saveMoney ();
			AllData.data.saveUpgrades ();
			PrintallValues ();
		}
	}

	public void upRT(){
		if (AllData.data.upgradeData [5] < 12f && AllData.data.money >= (AllData.data.upgradeData [5]*4) ){
			AllData.data.upgradeData [5] += 3f;
			AllData.data.money -= ((AllData.data.upgradeData [5]-3f) * 4);
			AllData.data.saveMoney ();
			AllData.data.saveUpgrades ();
			PrintallValues ();
		}
	}



	void Update () {
	
	}

	public void PrintallValues(){
		moneyT.text = "Coins: " + AllData.data.money.ToString ();

		clocksChanceP.text = (AllData.data.upgradeData [0]*3).ToString();
		coinsChanceP.text =  (AllData.data.upgradeData [1]*3).ToString();
		timePerClockP.text = (AllData.data.upgradeData [2]*20).ToString();
		moneyPerCoinP.text = (AllData.data.upgradeData [3]*20).ToString();
		ITITP.text = (AllData.data.upgradeData [4]*4).ToString();
		RTP.text = (AllData.data.upgradeData [5]*4).ToString();

		clocksChance.text = AllData.data.upgradeData [0].ToString ()+ " %";
		coinsChance.text = AllData.data.upgradeData [1].ToString ()+ " %";
		timePerClock.text = AllData.data.upgradeData [2].ToString ();
		moneyPerCoin.text = AllData.data.upgradeData [3].ToString ();
		ITIT.text = AllData.data.upgradeData [4].ToString ()+" s";
		RT.text = AllData.data.upgradeData [5].ToString () +" s";
	}

 	public void AddMoney2(){
		AllData.data.addMoney (100f);
		AllData.data.saveMoney ();
		PrintallValues ();
	}
		
}
