using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmScript : MonoBehaviour {

	void Awake() {


	}

	public void PlaySFX() {

		SFXScript.Instance.PlayAlarm ();
	}
}
