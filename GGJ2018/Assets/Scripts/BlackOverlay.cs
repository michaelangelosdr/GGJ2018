using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackOverlay : MonoBehaviour {

	private static BlackOverlay instance = null;

	public static BlackOverlay Instance {

		get { 

			return instance;
		}
	}

	public Image image;

	void Awake() {

		if (!instance) {

			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {

			Destroy (gameObject);
		}
	}

	public void FadeIn() {

		StartCoroutine (Fade (Color.clear, Color.black));
	}

	public void FadeOut() {

		StartCoroutine (Fade (Color.black, Color.clear));
	}

	IEnumerator Fade(Color startC, Color endC) {
	
		float timeElapsed = 0;

		image.color = startC;

		while (timeElapsed < 1) {
		
			image.color = Color.Lerp (startC, endC, timeElapsed / 1);
			timeElapsed += Time.deltaTime;

			yield return null;
		}

		image.color = endC;
	}
}
