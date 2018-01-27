using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Canvass_controller : MonoBehaviour {

	[SerializeField] List<Button> Buttons;
	[SerializeField] GGJSceneManager Scenemanager;
	[SerializeField] GameObject Highscore_Window;

	void Start() {

		BGMScript.Instance.PlayMainMenuBGM ();
		BlackOverlay.Instance.FadeOut ();
	}

	public void Start_Clicked()
	{
		Debug.Log ("Load Game");

		SFXScript.Instance.PlayClickSound ();
		StartCoroutine (GoingToGame ());
	}

	IEnumerator GoingToGame() {

		BlackOverlay.Instance.FadeIn ();

		yield return new WaitForSeconds (1);

		BGMScript.Instance.PlayGameBGM ();

		Scenemanager.LoadScene ("Game");
	}

	public void Show_Option()
	{
		//	Scenemanager.LoadScene ("Options");
		Debug.Log ("Show Option");

	}
		

	public void Exit_Game()
	{

		SFXScript.Instance.PlayClickSound ();
		Application.Quit ();
	}

}
