using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleScript : MonoBehaviour {
	
	Text consoleUI;

	void Awake() {

		consoleUI = GetComponent<Text> ();
	}

	public string GetText() {

		return consoleUI.text;
	}

	public void SetText(string command) {

		consoleUI.text = command;
	}
}
