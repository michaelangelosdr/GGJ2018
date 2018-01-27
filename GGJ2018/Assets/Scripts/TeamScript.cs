using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamScript : MonoBehaviour {

	public float Patience_Value = 100;
	public float patienceBaseDecrease = 2.5f;
	public float Patience_Multiplier;
	public float time_sec;
	public Teamstate CurrentState;
	public GameObject SpriteObject;

	public static float StateChangeTime = 10;

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
		nameMesh.text = this.name + " - " + bandwidth.ToString();
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
				SpriteObject.GetComponent<SpriteAnimator> ().enabled = true;
			
			Patience_Multiplier = patienceBaseDecrease * 0.05f;
			break;
		case Teamstate.GAMING:
			SpriteObject.GetComponent<SpriteAnimator> ().enabled = true;
			Patience_Multiplier = patienceBaseDecrease * 0.1f;
			break;
		case Teamstate.UPLOADING:
				SpriteObject.GetComponent<SpriteAnimator> ().enabled = true;
			Patience_Multiplier = patienceBaseDecrease * 0.2f;
			break;
		case Teamstate.WATCHING:
			SpriteObject.GetComponent<SpriteAnimator> ().enabled = false;
			Patience_Multiplier = patienceBaseDecrease * 0.2f;
			break;
		}
			//		Debug.Log (this.name + " is currently: " + curState);
		//Isnert IfStatement(PowerFailure)
		//{
		//}
		//else
		//{

		if (!MediatorScript.gameStarted)
			return;

		if (MediatorScript.powerFailure) {
			
			//decrease by constant value
			Patience_Value -= 0.1f;
			return;
		}

		Be_Impatient ();
		IncreasePatience ();
	}

	private void Be_Impatient()
	{
		//For now this would be a constant rate
		if (Patience_Value < 0)
			Patience_Value = 0;

		Patience_Value -= Patience_Multiplier;
	}

	void IncreasePatience() {

		if (CurrentState == Teamstate.WORKING) {

			if (bandwidth == 0.0f) {

			} else if (bandwidth == 0.5f) {

				Patience_Value += Patience_Multiplier;
			} else if (bandwidth >= 1 && bandwidth <2) {

				Patience_Value += Patience_Multiplier * 2;
			} else if (bandwidth == 2) {

				Patience_Value += Patience_Multiplier * 2.5f;
			}
		} else if (CurrentState == Teamstate.GAMING) {

			if (bandwidth == 0.0f) {

			} else if (bandwidth == 0.5f) {

				Patience_Value += Patience_Multiplier * 0.75f;
			} else if (bandwidth >= 1 && bandwidth <2) {

				Patience_Value += Patience_Multiplier * 2f;
			} else if (bandwidth == 2) {

				Patience_Value += Patience_Multiplier * 2f;
			}
		} else if (CurrentState == Teamstate.UPLOADING) {

			if (bandwidth == 0.0f) {

				// 
			} else if (bandwidth == 0.5f) {

				Patience_Value += Patience_Multiplier * 0.5f;
			} else if (bandwidth >= 1 && bandwidth <2){

				Patience_Value += Patience_Multiplier * 1.5f;
			} else if (bandwidth == 2) {

				Patience_Value += Patience_Multiplier * 1.5f;
			}
		} else if (CurrentState == Teamstate.WATCHING) {

			if (bandwidth == 0.0f) {

				Patience_Value -= (Patience_Multiplier * 0.5f);
			} else if (bandwidth == 0.5f) {

				Patience_Value += Patience_Multiplier * 0.5f;
			} else if (bandwidth >= 1 && bandwidth <2) {

				Patience_Value += Patience_Multiplier * 1.5f;
			} else if (bandwidth == 2) {

				Patience_Value += Patience_Multiplier * 1.5f;
			}
		}

		if (Patience_Value > 100)
			Patience_Value = 100;
	}

	public void Patience_Depleted()
	{
//		Debug.Log ("Zero Patience of : " + this.name + " GAME OVER");
	}
		
	IEnumerator ChangingStates() {

		yield return new WaitForEndOfFrame ();

		yield return new WaitUntil (() => MediatorScript.gameStarted);

		while (true) {
		
//			Debug.Log (this.name + ": " + CurrentState.ToString ());

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
	WORKING = 0,
	GAMING = 1,
	UPLOADING = 2,
	WATCHING = 3,
}

