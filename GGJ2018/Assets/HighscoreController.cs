using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreController : MonoBehaviour {

	RectTransform rect;

	Vector2 end = new Vector2(501, 276);
	Vector2 start = new Vector2(961, 276);
	Vector2 current;

	bool showing = false;

	void Awake () {

		current = start;
		rect = GetComponent<RectTransform> ();
	}

	void Update () {

		if (showing)
			current = end;
		else
			current = start;
		
		rect.anchoredPosition = Vector2.Lerp (rect.anchoredPosition, current, 0.125f);
	}

	public void HighscoreClicked() {
	
		showing = !showing;
	}
}
