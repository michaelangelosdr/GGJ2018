using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamScript : MonoBehaviour {

	public float Patience_Value;
	public string JammerName;
	public float Patience_Multiplier;
	public float time_sec;
	public Teamstate CurrentState;

	[SerializeField] Text Textt;

	public TeamScript()
	{
		Patience_Value = 100.0f;
		JammerName = "Default_Jammer";
		CurrentState = Teamstate.WORKING_STATE;
	}
		

	public void SetBandwith(float bandwith)
	{
		Debug.Log ("Bandwith of " + JammerName + " Is now: " + bandwith);
	}



	void Update()
	{
		//InsertDatingGoSkreaaa
		if (Patience_Value != 0)
			Run_State (CurrentState);
		else
			Patience_Depleted ();
	}

	public void Run_State(Teamstate curState)
	{
		switch (curState) {
		case Teamstate.WORKING_STATE:
			Patience_Multiplier = 0.25f;
			break;
		case Teamstate.GAMING_STATE:
			Patience_Multiplier = 0.5f;
			break;
		case Teamstate.UPLOADING_STATE:
			Patience_Multiplier = 1.0f;
			break;
		}
		Debug.Log (JammerName + " is currently: " + curState);
		Be_Impatient ();
	}



	private void Be_Impatient()
	{
		//For now this would be a constant rate
		Patience_Value -= Patience_Multiplier;
		Textt.text = Patience_Value.ToString ();
	}


	public void Patience_Depleted()
	{
		Debug.Log ("Zero Patience of : " + JammerName + " GAME OVER");
	}

		
}
public enum Teamstate
{
	WORKING_STATE,
	GAMING_STATE,
	UPLOADING_STATE,
	WATCHINGCORN_STATE
}

