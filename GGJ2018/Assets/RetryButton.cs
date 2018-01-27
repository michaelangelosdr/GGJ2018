using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RetryButton : MonoBehaviour {

	public void Retry() {

		SFXScript.Instance.PlayClickSound ();
		StartCoroutine (Retrying ());
	}

	IEnumerator Retrying() {
		
		BlackOverlay.Instance.FadeIn ();

		yield return new WaitForSeconds (1);

		GGJSceneManager.Instance.LoadScene ("Game");
	}

	public void ReturnToMenu() {

		SFXScript.Instance.PlayClickSound ();
		StartCoroutine (ReturningToMenu ());
	}

	IEnumerator ReturningToMenu() {
	
		BlackOverlay.Instance.FadeIn ();

		yield return new WaitForSeconds (1);

		GGJSceneManager.Instance.LoadScene ("Menu_scene");
	}
}
