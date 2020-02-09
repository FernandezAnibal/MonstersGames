using UnityEngine;
using System.Collections;

public class EffectB : MonoBehaviour {

	public GameObject monster;
	public GameObject Effects;

	// Use this for initialization
	void OnEnable () {
		// cuando se hablilita desaparece al player
		monster.GetComponent<SkinnedMeshRenderer> ().enabled = false;
		PuffEffect ();

	}

	void OnDisable (){
		
		// cuando se hablilita aparece al player
		monster.GetComponent<SkinnedMeshRenderer> ().enabled = true;
		PuffEffect ();
	}

		//Nubes para aparecer
	public void PuffEffect ()
	{
		
		if (!Effects.gameObject.activeSelf) {
			Effects.gameObject.SetActive (true);
		} else {
			Effects.gameObject.SetActive (false);
			Effects.gameObject.SetActive (true);
		}

	}
	
}
