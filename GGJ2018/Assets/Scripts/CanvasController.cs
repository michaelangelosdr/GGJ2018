using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

	public MediatorScript ms;

	public Text timeleftUI;
	public Text scoreUI;
	public Text consoleUI;

	void Update () {
	
		timeleftUI.text = "Time Left: " + ms.timeLeft + "s";
		scoreUI.text = "Score: " + ((int)ms.score);

		SetConsoleText ();
	}

	void SetConsoleText() {
		
		if (ms.command.Length > 0)
			consoleUI.text = "> " + ms.command;
		else
			consoleUI.text = "> _";
	}
}
