using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreenScript : MonoBehaviour {

	public Image blackOverlay;

	void Awake() {

		StartCoroutine (SplashingScreen ());
	}

	IEnumerator SplashingScreen() {

		yield return ChangeColor (Color.black, Color.clear);

		yield return new WaitForSeconds (2);

		yield return ChangeColor (Color.clear, Color.black);

		SceneManager.LoadScene ("Menu_scene");
	}

	IEnumerator ChangeColor(Color start, Color end) {

		float timeElapsed = 0;

		while (timeElapsed < 1) {
		
			blackOverlay.color = Color.Lerp (start, end, timeElapsed / 1);
			timeElapsed += Time.deltaTime;

			yield return null;
		}

		blackOverlay.color = end;

		yield return null;
	}
}
