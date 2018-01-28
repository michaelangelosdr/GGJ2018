using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveByTime : MonoBehaviour {

	public float seconds;

	void Awake() {
	
		StartCoroutine (SettingInactive ());
	}

	IEnumerator SettingInactive() {

		yield return new WaitForSeconds (seconds);

		this.gameObject.SetActive (false);
	}
}
