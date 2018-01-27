using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Canvass_controller : MonoBehaviour {

	[SerializeField] List<Button> Buttons;
	[SerializeField] GGJSceneManager Scenemanager;


	public void Start_Clicked()
	{Debug.Log ("Load Game");
		Scenemanager.LoadScene ("Game");

	}

	public void Show_Option()
	{
		//	Scenemanager.LoadScene ("Options");
		Debug.Log ("Show Option");

	}

	public void Show_Highscores()
	{
		Debug.Log ("High_Score");
		//Scenemanager.LoadScene ("HighScore");
	}

	public void Exit_Game()
	{
		Application.Quit ();
	}

}
