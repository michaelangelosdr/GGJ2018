using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Canvass_controller : MonoBehaviour {

	[SerializeField] List<Button> Buttons;
	[SerializeField] GGJSceneManager Scenemanager;
	[SerializeField] GameObject Highscore_Window;


	public void Start_Clicked()
	{
		Debug.Log ("Load Game");

		StartCoroutine (GoingToGame ());
	}

	IEnumerator GoingToGame() {

		BlackOverlay.Instance.FadeIn ();

		yield return new WaitForSeconds (1);

		Scenemanager.LoadScene ("Game");
	}

	public void Show_Option()
	{
		//	Scenemanager.LoadScene ("Options");
		Debug.Log ("Show Option");

	}
		

	public void Exit_Game()
	{

		Application.Quit ();
	}

	public void Show_HighScores()
	{
		if (Highscore_Window.gameObject.activeInHierarchy == false) {
			Highscore_Window.SetActive (true);	
		}
	}
	public void Hide_HighScore()
	{
		if (Highscore_Window.gameObject.activeInHierarchy == true) {
			Highscore_Window.SetActive (false);
		}
	}

}
