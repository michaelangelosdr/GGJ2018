﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediatorScript : MonoBehaviour {

	//Make List
	[SerializeField] TeamScript teamA;
	[SerializeField] TeamScript teamB;
	[SerializeField] TeamScript teamC;
	TeamScript receiverTeam;
	TeamScript secondaryReceiverTeam;

	[SerializeField] ConsoleScript consoleScript;

	public float baseTime;
	float timeLeft;
	float bandwidth;
	float score;

	string command;

	void Awake() {

		timeLeft = baseTime;
		score = 0;

		command = "";
		bandwidth = 2.0f;

		teamA.bandwidth = 1;
		teamB.bandwidth = 0.5f;
		teamC.bandwidth = 0.5f;

		Debug.Log ("Console Initialized");

		StartCoroutine (StartGame ());
	}

	void Update () {

		ComputeBandwidth ();
		HandleKeyInput ();

//		Debug.Log (teamA.Patience_Value + ":" + teamB.Patience_Value + ":" + teamC.Patience_Value);
	}

	void ComputeBandwidth() {

		bandwidth = 2 - teamA.bandwidth - teamB.bandwidth - teamC.bandwidth;
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

//				Debug.Log (command);
			} else if ((c == '\n') || (c == '\r')) {

				//Debug.Log ("Command: " + command);
				ProcessCommand ();
			} else {

				command += c;
				//Debug.Log (command);
			}
		}
	}

	public void ProcessCommand() {

		ParseData ();

		command = "";
	}

	void ParseData() {

		string[] commandSplit = command.Split (null);

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
			return;
		}

		ShowBandwidthValues ();
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

		receiverTeam.bandwidth += secondaryReceiverTeam.bandwidth;
		secondaryReceiverTeam.bandwidth = 0;
	}

	void Maximum() {

		teamA.bandwidth = 0;
		teamB.bandwidth = 0;
		teamC.bandwidth = 0;

		receiverTeam.bandwidth = 2;
	}

	void Optimize() {
		
		Debug.Log ("OPTIMIZE");

		teamA.bandwidth = 0.5f;
		teamB.bandwidth = 0.5f;
		teamC.bandwidth = 0.5f;

		receiverTeam.bandwidth = 1;
	}

	void Restart() {
		
		Debug.Log ("RESTARTING");
	}

	void ShowBandwidthValues() {

		Debug.Log ("Team A: " + teamA.bandwidth);
		Debug.Log ("Team B: " + teamB.bandwidth);
		Debug.Log ("Team C: " + teamC.bandwidth);
	}

	IEnumerator StartGame() {

		while (timeLeft > 0) {
			
			yield return new WaitForSeconds (1);

			timeLeft--;

			AddScore ();
		}

		Debug.Log ("TIME'S UP!!");

	}

	void AddScore() {

		score += teamA.Patience_Value / 100.0f;
		score += teamB.Patience_Value / 100.0f;
		score += teamC.Patience_Value / 100.0f;

		Debug.Log ("Score: " + score);
	}
}
