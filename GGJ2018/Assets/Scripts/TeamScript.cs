using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamScript : MonoBehaviour {

	public float Patience_Value = 100;
	public float Patience_Multiplier;
	public float time_sec;
	public float Patience_Increase = 0.2f;
	public Teamstate CurrentState;

	public static float StateChangeTime = 1;

	public float bandwidth;

	void Awake() {


		StartCoroutine (ChangingStates ());
	}

//	public void GetBandwith(float givingBandwith)
//	{
//		
//		bandwidth = givingBandwith;
//	}
//
//	public void GiveBandwidth(float getBandwith) 
//	{
//
//		bandwidth += getBandwith;
//	}

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
			Patience_Multiplier = 0.05f;
			break;
		case Teamstate.GAMING_STATE:
			Patience_Multiplier = 0.075f;
			break;
		case Teamstate.UPLOADING_STATE:
			Patience_Multiplier = 0.1f;
			break;
		}
//		Debug.Log (this.name + " is currently: " + curState);
		Be_Impatient ();
	}

	private void Be_Impatient()
	{
		//For now this would be a constant rate
		if (Patience_Value < 0)
			Patience_Value = 0;

		Patience_Value -= Patience_Multiplier;

		if (CurrentState == Teamstate.WORKING_STATE) {

			if (bandwidth == 0.0f) {
			
				Patience_Value += (Patience_Multiplier * 0.5f);
			} else if (bandwidth == 0.5f) {
			
				Patience_Value += Patience_Multiplier;
			} else if (bandwidth == 1) {
			
				Patience_Value += Patience_Multiplier * 1.125f;
			} else if (bandwidth == 2) {
			
				Patience_Value += Patience_Multiplier * 1.25f;
			}
		} else if (CurrentState == Teamstate.GAMING_STATE) {

			if (bandwidth == 0.0f) {

				Patience_Value += (Patience_Multiplier * 0.25f);
			} else if (bandwidth == 0.5f) {

				Patience_Value += Patience_Multiplier * 0.5f;
			} else if (bandwidth == 1) {

				Patience_Value += Patience_Multiplier * 1.0625f;
			} else if (bandwidth == 2) {

				Patience_Value += Patience_Multiplier * 1.125f;
			}
		} else if (CurrentState == Teamstate.UPLOADING_STATE) {

			if (bandwidth == 0.0f) {

				// 
			} else if (bandwidth == 0.5f) {

				Patience_Value += Patience_Multiplier * 0.25f;
			} else if (bandwidth == 1) {

				Patience_Value += Patience_Multiplier * 1.125f;
			} else if (bandwidth == 2) {

				Patience_Value += Patience_Multiplier * 1.25f;
			}
		} else if (CurrentState == Teamstate.WATCHINGCORN_STATE) {

			if (bandwidth == 0.0f) {

				Patience_Value += (Patience_Multiplier * 0.5f);
			} else if (bandwidth == 0.5f) {

				Patience_Value += Patience_Multiplier;
			} else if (bandwidth == 1) {

				Patience_Value += Patience_Multiplier * 1.25f;
			} else if (bandwidth == 2) {

				Patience_Value += Patience_Multiplier * 1.5f;
			}
		}
	}

	public void Patience_Depleted()
	{
//		Debug.Log ("Zero Patience of : " + this.name + " GAME OVER");
	}
		
	IEnumerator ChangingStates() {

		while (true) {
		
//			Debug.Log (this.name + ": " + CurrentState.ToString ());

			int rand = Random.Range (0, 3);

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

