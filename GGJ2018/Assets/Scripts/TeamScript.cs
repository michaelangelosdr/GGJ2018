using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamScript : MonoBehaviour {

	public float Patience_Value = 100;
	public float Patience_Multiplier;
	public float time_sec;
	public Teamstate CurrentState;

	public static float StateChangeTime = 1;

	void Awake() {


		StartCoroutine (ChangingStates ());
	}

	public void SetBandwith(float bandwith)
	{
//		Debug.Log ("Bandwith of " + this.name + " Is now: " + bandwith);
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
//		Debug.Log (this.name + " is currently: " + curState);
		Be_Impatient ();
	}

	private void Be_Impatient()
	{
		//For now this would be a constant rate
		Patience_Value -= Patience_Multiplier;
	}

	public void Patience_Depleted()
	{
//		Debug.Log ("Zero Patience of : " + this.name + " GAME OVER");
	}
		
	IEnumerator ChangingStates() {

		while (true) {
		
			Debug.Log (this.name + ": " + CurrentState.ToString ());

			int rand = Random.Range (0, 4);

//			Debug.Log ("Chosen Random: " + rand);

			foreach (Teamstate t in System.Enum.GetValues(typeof(Teamstate))) {
			
				if (rand == (int)t)
					CurrentState = t;
			}

			yield return new WaitForSeconds (StateChangeTime);
		}
	}

}
public enum Teamstate
{
	WORKING_STATE = 0,
	GAMING_STATE = 1,
	UPLOADING_STATE = 2,
	WATCHINGCORN_STATE = 3,
}

