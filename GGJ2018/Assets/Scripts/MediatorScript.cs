using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediatorScript : MonoBehaviour {

	[SerializeField] TeamScript teamA;
	[SerializeField] TeamScript teamB;
	[SerializeField] TeamScript teamC;
	TeamScript receiverTeam;
	TeamScript secondaryReceiverTeam;

	[SerializeField] ConsoleScript consoleScript;

	float bandwidth;

	string command;

	void Awake() {

		command = "";
		bandwidth = 2.0f;

		Debug.Log ("Console Initialized");
	}

	void Update () {

		HandleKeyInput ();
	}

	void HandleKeyInput() {

		if (consoleScript) {
			if (command.Length > 0)
				consoleScript.SetText ("> " + command);
			else
				consoleScript.SetText ("> _");
		}

		foreach (char c in Input.inputString) {

			if (c == '\b') {

				if (command.Length != 0) {

					command =command.Substring(0, command.Length - 1);
				}

				Debug.Log (command);
			} else if ((c == '\n') || (c == '\r')) {

				Debug.Log ("Command: " + command);
				ProcessCommand ();
			} else {

				command += c;
				Debug.Log (command);
			}
		}
	}

	public void ProcessCommand() {

		ParseData ();

		command = "";
	}

	void ParseData() {

		string[] commandSplit = command.Split ();

		string action = commandSplit [0];

		if (string.IsNullOrEmpty (action)) {
		
			Debug.Log ("No Command Given");
			return;
		}

		action = action.ToLower ();

		if(commandSplit.Length > 1)
			SetReceiverTeam (commandSplit [1].ToLower());
	
		if(commandSplit.Length > 2)
			SetSecondaryReceiverTeam (commandSplit [2].ToLower());

		if (action.Equals ("route")) {

			if(receiverTeam && secondaryReceiverTeam)
				Route ();
		} else if (action.Equals ("maximum")) {
		
			if (receiverTeam)
				Maximum ();
		} else if (action.Equals ("spread")) {
		
			if (receiverTeam)
				Optimize ();
		} else if (action.Equals ("restart")) {
		
			Restart ();
		} else {

			Debug.Log ("INVALID COMMAND");
		}
	}

	void SetReceiverTeam(string receiver) {

		if (string.IsNullOrEmpty (receiver))
			return;

		if (receiver.Equals ("a")) {

			receiverTeam = teamA;
		} else if (receiver.Equals ("b")) {

			receiverTeam = teamB;
		} else if (receiver.Equals ("c")) {

			receiverTeam = teamC;
		} else {

			Debug.Log ("Receiver Team Null");
			receiverTeam = null;
		}

		Debug.Log ("Reciever Team Set!");
	}

	void SetSecondaryReceiverTeam(string secondaryReceiver) {

		if (string.IsNullOrEmpty (secondaryReceiver))
			return;
		
		if (secondaryReceiver.Equals ("a")) {

			secondaryReceiverTeam = teamA;
		} else if (secondaryReceiver.Equals ("b")) {

			secondaryReceiverTeam = teamB;
		} else if (secondaryReceiver.Equals ("c")) {

			secondaryReceiverTeam = teamC;
		} else {

			Debug.Log ("Secondary Receiver Team Null");
			secondaryReceiverTeam = null;
		}

		Debug.Log ("Secondary Reciever Team Set!");
	}

	void Route() {

		Debug.Log ("ROUTE");
	}

	void Maximum() {
		
		Debug.Log ("MAXIMUM");
	}

	void Optimize() {
		
		Debug.Log ("OPTIMIZE");
	}

	void Restart() {
		
		Debug.Log ("RESTARTING");
	}
}
