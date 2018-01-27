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

	public float baseTime;
	public float timeLeft;
	public float score;
	float powerFailureInterval = 15;
	float powerFailureChance = 20;
	float bandwidth;

	public string previousCommand = "";
	public string command;

	public Text timeleftUI;
	public Text scoreUI;
	public Text consoleUI;
	public Text instructionUI;

	public List<GameObject> tutorialPages;

	int tutorialCounter;

	bool showing;
	bool tutorial;
	public static bool gameStarted = false;

	public static bool powerFailure = false;

	public static bool gameOver = false;

	public GameObject powerFailureOverlay;
	public GameObject gameOverOverlay;

	public bool usingHelp = false;
	public RectTransform helpWindow;

	Vector2 outPosition = new Vector2(-496, 0);
	Vector2 inPosition = new Vector2(-1100, 0);

	Vector2 currentPosition;

	void Awake() {

		timeLeft = baseTime;
		score = 0;

		command = "";
		bandwidth = 2.0f;

		teamA.bandwidth = 2;
		teamB.bandwidth = 0f;
		teamC.bandwidth = 0f;

		Debug.Log ("Console Initialized");

		StartCoroutine (ShowTutorial ());
	}

	void Update () {

		if (Input.GetKey (KeyCode.LeftShift)) {
		
			Time.timeScale = 3;
		} else {
		
			Time.timeScale = 1;
		}

		ComputeBandwidth ();
		HandleKeyInput ();

		string secondsPart = (timeLeft % 60).ToString ();

		if (secondsPart.Length < 2)
			secondsPart = "0" + secondsPart;

		timeleftUI.text = Mathf.Floor(timeLeft/60.0f) + ":" + secondsPart;
		scoreUI.text = "Score: " + ((int)score);

		powerFailureOverlay.SetActive (powerFailure);

		if (showing)
			command = "";

		if (command.Length > 0)
			consoleUI.text = "> " + command;
		else
			consoleUI.text = "> _";

		if (timeLeft < 0 || (teamA.Patience_Value <= 0 || teamB.Patience_Value <= 0 || teamC.Patience_Value <= 0))
			GameOver ();

		if (Input.GetKeyDown (KeyCode.Tab))
			HelpPressed ();

		LerpHelpWindow ();

//		Debug.Log (teamA.Patience_Value + ":" + teamB.Patience_Value + ":" + teamC.Patience_Value);
	}

	void LerpHelpWindow() {

		if (usingHelp && !gameOver)
			currentPosition = outPosition;
		else
			currentPosition = inPosition;

		helpWindow.anchoredPosition = Vector2.Lerp(helpWindow.anchoredPosition, currentPosition, 0.25f);
	}

	IEnumerator ShowTutorial() {

		gameStarted = false;
		tutorialCounter = 0;
		tutorial = true;

		ShowInstruction ("Show tutorial? (y or n)");

		yield return new WaitUntil (() => (Input.GetKeyDown (KeyCode.Return) && (command.ToLower().Equals ("y") || command.ToLower().Equals ("n"))));

		Debug.Log (command);

		if (command.ToLower ().Equals ("y")) {
		
			command = "";

//			yield return MessageWithSpecificConfirmation ("type 'boi'", "boi");

			//TODO: TELL ABOUT HINT AND UP BUTTON

			yield return MachineTyping ("Hi!");
			yield return MachineTyping ("You must be the new intern at PLDC!");
			yield return MachineTyping ("You're job is simply to TRANSMIT BANDWIDTH...", 0.05f);
			yield return MachineTyping ("When you see the arrow (->)..");
			yield return MessageWithConfirmation ("..press enter to move on, ok?");
			yield return MachineTyping ("Nice. ;)");
			yield return MachineTyping ("I'll show you what teams are all about!", 0.5f);
			yield return MessageWithConfirmation ("Each team has a PATIENCE BAR.");
			yield return MachineTyping ("If even one of them loses their patience..");
			yield return MessageWithConfirmation ("It's GAME OVER");
			yield return MessageWithConfirmation ("This shows the amount of bandwidth a team has.");
			yield return MessageWithConfirmation ("This represents the team's state.");
			yield return MachineTyping ("Each team has 4 states.");
			yield return MessageWithConfirmation ("WORKING -> don't need much bandwidth.");
			yield return MessageWithConfirmation ("GAMING -> need average bandwidth.");
			yield return MessageWithConfirmation ("UPLOADING -> need all the bandwidth they can get.");
			yield return MessageWithConfirmation ("WATCHING -> must get all it can.");

			yield return MachineTyping ("Next up. Commands.");
			/*yield return MessageWithConfirmation ("Commands are made up of two parts.");
			yield return MessageWithConfirmation ("These are the METHOD and the PARAMETER");
			yield return MessageWithConfirmation ("The METHOD is the action you want to do.");
			yield return MessageWithConfirmation ("The PARAMETER is which object to apply the action to.");
			yield return MessageWithConfirmation ("One sample command is 'get x'.");
			yield return MessageWithConfirmation ("In this example, 'get' is the METHOD...");
			yield return MessageWithConfirmation ("and 'x' is the PARAMETER");
			yield return MessageWithConfirmation ("The first command, 'get x', gets bandwidth from team x.");*/
			yield return MachineTyping ("Team A has is taking up too much bandwidth!");
			yield return MessageWithSpecificConfirmation ("Type 'get a' to get bandwidth from team A.", "get a");

			teamA.bandwidth -= 0.5f;

//			yield return MachineTyping ("Good Job! :D You're pretty good at this. ;)");
//			yield return MachineTyping ("(Not. :P)", 0.5f);
//			yield return MachineTyping ("ANYWAAaaay...");

//			yield return MachineTyping ("The next command is 'tunnel x'");
			//yield return MessageWithConfirmation ("'tunnel x' transmits bandwith to team x");
			//yield return MessageWithConfirmation ("NOTE: Check to see how much you can give out.");
			yield return MessageWithConfirmation ("Looks like team B needs some bandwidth!");
			yield return MessageWithSpecificConfirmation ("Type 'tunnel b' to transmit some to team B.", "tunnel b");

			teamB.bandwidth += 0.5f;

//			yield return MachineTyping ("Another job well done! ^_^");
//			yield return MachineTyping ("... for a noob", 0.5f);
//			yield return MachineTyping ("Just kidding. ;)");

//			yield return MachineTyping ("The next command is 'spread x'");
//			yield return MachineTyping ("'spread x' spreads the bandwidth among the teams...");
//			yield return MessageWithConfirmation ("...with team X getting more than the other teams");
			yield return MachineTyping ("Team C could use a bit of lovin' ;)");
			yield return MessageWithSpecificConfirmation ("Type 'spread c' to distribute the bandwidth", "spread c");

			teamA.bandwidth = 0.5f;
			teamB.bandwidth = 0.5f;
			teamC.bandwidth = 1f;
					
			yield return MachineTyping ("'spread x' distributes bandwidth among the teams..");
			yield return MessageWithConfirmation ("..with team X getting more than the others");

//			yield return MachineTyping ("Nice! Now all the teams are loving you. :D");
////		yield return MachineTyping ("Too bad your crush doesn't. ;')", 0.5f);
////		yield return MachineTyping ("Just kidding.");
//			yield return MachineTyping ("Hahahahaha...", 1f);
//			yield return MachineTyping ("Ha...", 2f);

//			yield return MachineTyping ("The next command is 'max x'");
//			yield return MessageWithConfirmation ("'max x' transmits all the bandwith team x.");
//			yield return MachineTyping ("Heckin' ridiculous, if you ask me.");
//			yield return MachineTyping ("Let's give team A all the bandwith now.");
			yield return MachineTyping ("If needed, you can give a team all the bandwidth..");
			yield return MessageWithConfirmation ("..by using the 'max' command.");
			yield return MessageWithSpecificConfirmation ("Type 'max a' give team A all the bandwidth.", "max a");

			teamA.bandwidth = 2;
			teamB.bandwidth = 0;
			teamC.bandwidth = 0;

//			yield return MachineTyping ("I bet they're heckin happy now. :/");
			//yield return MachineTyping ("Oops!", 0.5f);
			//yield return MachineTyping ("They're**");
			//yield return MachineTyping ("Sorry. Autocorrect", 1f);

			//TODO: Add power failure
			// ADD POWER FAILURE

			yield return MachineTyping ("The last command is-", 0.5f);

			powerFailure = true;

			yield return MachineTyping ("Oh no! A power failure!", 0.75f);

			yield return MessageWithSpecificConfirmation ("Quick! Type 'restart' to fix everything!", "restart");

			Restart ();

			yield return MachineTyping ("*Phew* That was a close one.");
			yield return MessageWithConfirmation ("Just do that everytime a power failure happens, ok?");

			yield return MachineTyping ("A few last notes:");
			yield return MessageWithConfirmation ("Press the 'up arrow' to retype your last command.");
			yield return MessageWithConfirmation ("Press 'tab' to bring out a cheat sheet.");

			yield return MachineTyping ("That's all, Make sure you transmit appropriately!");
			yield return MessageWithConfirmation ("I'm gonna start the game now, okay? :D" );
		}

		command = "";

		yield return MachineTyping ("Starting game in", 0.25f);

		yield return MachineTyping ("3...", 0.5f);

		yield return MachineTyping ("2...", 0.5f);

		yield return MachineTyping ("1...", 0.5f);

		SetInstruction ("ENTER COMMAND");

		tutorial = false;
		StartCoroutine (StartGame ());
	}

	IEnumerator WaitingConfirmation() {

		yield return new WaitUntil (() => (Input.GetKeyDown (KeyCode.Return) && !showing));
		command = "";
	}

	IEnumerator WaitingSpecificConfirmation(string message) {

		yield return new WaitUntil (() => (Input.GetKeyDown (KeyCode.Return) && command.ToLower().Equals (message)));
		command = "";
	}

	IEnumerator MessageWithConfirmation(string message) {

		yield return MachineTyping (message + " ->");
		yield return WaitingConfirmation ();
	}

	IEnumerator MessageWithSpecificConfirmation(string message, string conf) {

		yield return MachineTyping (message);
		yield return WaitingSpecificConfirmation (conf);
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

	public void HelpPressed() {

		usingHelp = !usingHelp;
	}

	void HandleKeyInput() {

		if (usingHelp || gameOver)
			return;

		if (Input.GetKeyDown (KeyCode.UpArrow))
			command = previousCommand;

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

		previousCommand = command;
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
	
		if (action.Equals ("tunnel")) {

			if (powerFailure)
				return;

			if(receiverTeam)
				Tunnel ();
		} else if (action.Equals ("max")) {

			if (powerFailure)
				return;
			
			if (receiverTeam)
				Maximum ();
		} else if (action.Equals ("spread")) {

			if (powerFailure)
				return;
			
			if (receiverTeam)
				Spread ();
		} else if (action.Equals ("get")) {

			if (powerFailure)
				return;
			
			if(receiverTeam)
				GetBandwidth ();
		} else if (action.Equals ("restart")) {

			Restart ();
		}  else {

			Debug.Log ("INVALID COMMAND");
			TemporaryMessage ("INVALID COMMAND");
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

	void Tunnel() {

		Debug.Log ("TUNNEL");

		if(bandwidth >= 0.5f)
			receiverTeam.bandwidth += 0.5f;
	}

	void Maximum() {

		teamA.bandwidth = 0;
		teamB.bandwidth = 0;
		teamC.bandwidth = 0;

		receiverTeam.bandwidth = 2;
	}

	void Spread() {
		
		Debug.Log ("OPTIMIZE");

		teamA.bandwidth = 0.5f;
		teamB.bandwidth = 0.5f;
		teamC.bandwidth = 0.5f;

		receiverTeam.bandwidth = 1;
	}

	void GetBandwidth() {

		if (receiverTeam.bandwidth >= 0.5f) {
		
			receiverTeam.bandwidth -= 0.5f;
		}
	}

	void Restart() {
		
		Debug.Log ("RESTARTING");
		powerFailure = false;
	}

	void ShowBandwidthValues() {

		Debug.Log ("Team A: " + teamA.bandwidth);
		Debug.Log ("Team B: " + teamB.bandwidth);
		Debug.Log ("Team C: " + teamC.bandwidth);
	}

	IEnumerator StartGame() {

		gameStarted = true;

		StartCoroutine (PowerFailing ());

		while (timeLeft > 0 && !gameOver) {
			
			yield return new WaitForSeconds (1);

			timeLeft--;

			AddScore ();
		}

		Debug.Log ("TIME'S UP!!");

	}

	void StartPowerFailure() {

		TemporaryMessage ("POWER FAILURE!");
		Debug.Log ("POWER FAILURE");

		//TODO: Add overlay
		powerFailure = true;
	}

	IEnumerator PowerFailing() {

		while (timeLeft > 0 && !gameOver) {
		
			yield return new WaitForSeconds (powerFailureInterval);

			int rand = Random.Range (0, 101);

			if(rand <= powerFailureChance)
				StartPowerFailure ();
		}

		yield return null;
	}

	void AddScore() {

		score += teamA.Patience_Value / 100.0f;
		score += teamB.Patience_Value / 100.0f;
		score += teamC.Patience_Value / 100.0f;

		Debug.Log ("Score: " + score);
	}

	void GameOver() {

		gameOver = true;

		usingHelp = false;
		gameOverOverlay.SetActive (true);
	}

	void TemporaryMessage(string temp) {

		StartCoroutine (ShowingTemporaryMessage (temp));
	}

	IEnumerator ShowingTemporaryMessage(string temp) {
		
		SetInstruction (temp);

		yield return new WaitForSeconds (1);

		SetInstruction ("ENTER COMMAND");
	}
}
