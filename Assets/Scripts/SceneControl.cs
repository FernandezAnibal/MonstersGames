using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour {


	public static  SceneControl controlScene;

	//public Text carga;
	//public GameObject loadingImage;

	private AsyncOperation async;



	public void ClickAsync (int level)
	{
		//loadingImage.SetActive (true);
		StartCoroutine (LoadLevelWithBar (level));

	}

	IEnumerator LoadLevelWithBar (int level)
	{
		async = SceneManager.LoadSceneAsync (level);
		while (!async.isDone) 
		{
			//carga.text = (async.progress*100f).ToString ();
			yield return null;
		}
	}

	public void DisableBoolAnimator (Animator anim){
		anim.SetBool ("IsDisplayed", false);
	}

	public void EnableBoolAnimator (Animator anim){
		anim.SetBool ("IsDisplayed", true);
	}


	//Controles de Escena
	public void ResetScene ()
	{
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene (scene.name);
	}

	public void ExitGame()
	{
		Application.Quit ();
	}

	public void Pause ( ){

		if (Time.timeScale == 0) {
			Time.timeScale = 1;
		} else {
			Time.timeScale = 0;
		}

	}


}	
