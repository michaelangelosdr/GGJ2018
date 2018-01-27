using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MediatorScript : MonoBehaviour {

	//Make List
	[SerializeField] TeamScript teamA;
	[SerializeField] TeamScript teamB;
	[SerializeField] TeamScript teamC;
	TeamScript receiverTeam;
	TeamScript secondaryReceiverTeam;

	public float baseTime;
	public float timeLeft;
	public float score;
	float bandwidth;

	public string command;

	public Text timeleftUI;
	public Text scoreUI;
	public Text consoleUI;
	public Text instructionUI;

	public List<GameObject> tutorialPages;

	int tutorialCounter;

	bool showing;
	bool tutorial;

	void Awake() {

		timeLeft = baseTime;
		score = 0;

		command = "";
		bandwidth = 2.0f;

		teamA.bandwidth = 1;
		teamB.bandwidth = 0.5f;
		teamC.bandwidth = 0.5f;

		Debug.Log ("Console Initialized");

		StartCoroutine (ShowTutorial ());
	}

	void Update () {

		ComputeBandwidth ();
		HandleKeyInput ();

		timeleftUI.text = "Time Left: " + timeLeft + "s";
		scoreUI.text = "Score: " + ((int)score);

		if (showing)
			command = "";

		if (command.Length > 0)
			consoleUI.text = "> " + command;
		else
			consoleUI.text = "> _";

//		Debug.Log (teamA.Patience_Value + ":" + teamB.Patience_Value + ":" + teamC.Patience_Value);
	}

	IEnumerator ShowTutorial() {

		tutorialCounter = 0;
		tutorial = true;

		ShowInstruction ("Show tutorial? y or n");

		yield return new WaitUntil (() => (Input.GetKeyDown (KeyCode.Return) && (command.ToLower().Equals ("y") || command.ToLower().Equals ("n"))));

		Debug.Log (command);

		if (command.ToLower ().Equals ("y")) {
		
			command = "";

			yield return MachineTyping ("Hi!");
			yield return MachineTyping ("You must be the new intern at PLDC!");
			yield return MachineTyping ("You're job is to distribute bandwidth...", 0.05f);
			yield return MachineTyping ("or something...");
			yield return MachineTyping ("When you see the arrow (->)...");
			yield return MessageWithConfirmation ("...type 'ok' then press enter to move on, ok?");
			yield return MachineTyping ("Nice. ;)");
			yield return MachineTyping ("I'll show you what teams are all about!", 0.5f);
			yield return MessageWithConfirmation ("Each team has a patience bar.");
			yield return MessageWithConfirmation ("The bar represents how close they are to rage quitting.");
			yield return MachineTyping ("This symbol represents... ");
			yield return MessageWithConfirmation ("...how much bandwith each team is getting.");
			yield return MachineTyping ("This represents... ");
			yield return MessageWithConfirmation ("...what state the team is currently in.");
			yield return MessageWithConfirmation ("Each team has 3 states.");
			yield return MessageWithConfirmation ("WORKING, GAMING, and UPLOADING.");
			yield return MessageWithConfirmation ("WORKING states don't need much bandwith.");
			yield return MessageWithConfirmation ("GAMING states need average bandwith.");
			yield return MessageWithConfirmation ("UPLOADING states need all the bandwith they can get.");

			yield return MachineTyping ("That's all, I guess.");
			yield return MessageWithConfirmation ("I'm gonna start the game now, okay?");
		}

		command = "";

		yield return MachineTyping ("Starting game in");

		yield return MachineTyping ("3...", 0.5f);

		yield return MachineTyping ("2...", 0.5f);

		yield return MachineTyping ("1...", 0.5f);

		yield return new WaitForSeconds (1);

		SetInstruction ("Enter Command");

		tutorial = false;
		StartCoroutine (StartGame ());
	}

	IEnumerator WaitingConfirmation() {

		yield return new WaitUntil (() => (Input.GetKeyDown (KeyCode.Return) && command.ToLower().Equals ("ok")));
		command = "";
	}

	IEnumerator MessageWithConfirmation(string message) {

		yield return MachineTyping (message + " ->");
		yield return WaitingConfirmation ();
	}

	void SetInstruction(string message) {
	
		instructionUI.text = message;
	}

	void ShowInstruction(string message) {

		StartCoroutine (MachineTyping (message));
	}

	IEnumerator MachineTyping(string message, float secondsToTake = 0.75f) {
	
		showing = true;

		int counter = 0;

		while(counter <= message.Length) {

			instructionUI.text = message.Substring (0, counter++);

			yield return new WaitForSeconds (0.025f);
		}

		instructionUI.text = message;

		showing = false;

		yield return new WaitForSeconds (secondsToTake);

	}

	void ComputeBandwidth() {

		bandwidth = 2 - teamA.bandwidth - teamB.bandwidth - teamC.bandwidth;
	}

	void HandleKeyInput() {

		foreach (char c in Input.inputString) {

			if (c == '\b') {

				if (command.Length != 0) {

					command =command.Substring(0, command.Length - 1);
				}

//				Debug.Log (command);
			} else if ((c == '\n') || (c == '\r')) {

				//Debug.Log ("Command: " + command);

				if(!tutorial)
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
		} else if (action.Equals ("max")) {
		
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
