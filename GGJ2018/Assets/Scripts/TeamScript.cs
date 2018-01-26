using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamScript : MonoBehaviour {

	public float Patience_Value = 100;
	public float patienceBaseDecrease = 10;
	public float Patience_Multiplier;
	public float time_sec;
	public Teamstate CurrentState;

	public static float StateChangeTime = 1;

	public float bandwidth;

	public Transform meterFillContainer;

	public TextMesh textMesh;
	public TextMesh nameMesh;

	public SpriteRenderer fillSR;

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

		UpdateMeter ();

		textMesh.text = CurrentState.ToString ();
		nameMesh.text = this.name;
	}

	void UpdateMeter() {
		
		if(meterFillContainer)
			meterFillContainer.localScale = new Vector3 (Patience_Value / 100.0f, 1, 1);

		float multiplier = Patience_Value / 100.0f;

		if (multiplier >= 0.5f)
			fillSR.color = new Color (1 - (Patience_Value - 50) / 50, 1.0f, 0.0f);
		else
			fillSR.color = new Color (1.0f, Patience_Value / 50, 0);
	}

	public void Run_State(Teamstate curState)
	{
		switch (curState) {
		case Teamstate.WORKING:
			Patience_Multiplier = patienceBaseDecrease * 0.05f;
			break;
		case Teamstate.GAMING:
			Patience_Multiplier = patienceBaseDecrease * 0.075f;
			break;
		case Teamstate.UPLOADING:
			Patience_Multiplier = patienceBaseDecrease * 0.1f;
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

		if (CurrentState == Teamstate.WORKING) {

			if (bandwidth == 0.0f) {
			
				Patience_Value += (Patience_Multiplier * 0.5f);
			} else if (bandwidth == 0.5f) {
			
				Patience_Value += Patience_Multiplier;
			} else if (bandwidth == 1) {
			
				Patience_Value += Patience_Multiplier * 1.125f;
			} else if (bandwidth == 2) {
			
				Patience_Value += Patience_Multiplier * 1.25f;
			}
		} else if (CurrentState == Teamstate.GAMING) {

			if (bandwidth == 0.0f) {

				Patience_Value += (Patience_Multiplier * 0.25f);
			} else if (bandwidth == 0.5f) {

				Patience_Value += Patience_Multiplier * 0.5f;
			} else if (bandwidth == 1) {

				Patience_Value += Patience_Multiplier * 1.0625f;
			} else if (bandwidth == 2) {

				Patience_Value += Patience_Multiplier * 1.125f;
			}
		} else if (CurrentState == Teamstate.UPLOADING) {

			if (bandwidth == 0.0f) {

				// 
			} else if (bandwidth == 0.5f) {

				Patience_Value += Patience_Multiplier * 0.25f;
			} else if (bandwidth == 1) {

				Patience_Value += Patience_Multiplier * 1.125f;
			} else if (bandwidth == 2) {

				Patience_Value += Patience_Multiplier * 1.25f;
			}
		} else if (CurrentState == Teamstate.WATCHING) {

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
	WORKING = 0,
	GAMING = 1,
	UPLOADING = 2,
	WATCHING = 3,
}

