using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Finish : MonoBehaviour {

	//Para cualquier meta solo hay que incluir este scrip que detendra automaticamente al player y parara el reloj del arbitro

	Control control;

	Arbitro arbitro;

	public GameObject monster;

	public Text distance;
	public float distanceToFinish;

	private bool gameOver;

	// Use this for initialization
	void Start () {
		gameOver = false;
		arbitro = GameObject.FindGameObjectWithTag("Arbitro").GetComponent<Arbitro> ();
		control = GameObject.Find ("Control").GetComponent<Control> ();
		monster = GameObject.FindGameObjectWithTag ("Player");
		distance.text = Mathf.FloorToInt (transform.position.x -1 - Mathf.Floor (monster.transform.position.x)).ToString();
	}
	
	// Update is called once per frame
	void Update () {
		distanceToFinish = transform.position.x - Mathf.Floor (monster.transform.position.x);
		if (distanceToFinish > 0 && distanceToFinish < (transform.position.x - 3)) {
			distance.gameObject.SetActive (true);
			distance.text = (Mathf.FloorToInt (distanceToFinish)).ToString ();
		} else {
			distance.gameObject.SetActive (false);
		}

		if (monster.transform.position.x >= transform.position.x+0.06) {
			control.stopChar ();
			arbitro.timeStop = true;
			if (!gameOver) {
				AllData.data.money += Mathf.FloorToInt (transform.position.x / 10f);
				arbitro.FinishGame ();
				gameOver = true;
			}
		}

	}


		
}



