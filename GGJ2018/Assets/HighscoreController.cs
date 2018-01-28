using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreController : MonoBehaviour {

	RectTransform rect;

	Vector2 end = new Vector2(501, 276);
	Vector2 start = new Vector2(961, 276);
	Vector2 current;

	bool showing = false;

	public List<Text> scoreList;

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
	
		UpdateList ();
	}

	void UpdateList() {
	
		string scoreSave = PlayerPrefs.GetString ("scores");

		if (!string.IsNullOrEmpty (scoreSave)) {
		
			string[] scoreStrings = scoreSave.Split (',');
			List<int> scoreValues = new List<int> ();

			foreach (string s in scoreStrings) {

				int output;

				bool success = int.TryParse (s, out output);

				if (success)
					scoreValues.Add (output);
				else
					continue;
			}

			scoreValues.Sort ();
			scoreValues.Reverse ();

			for (int i = 0; i < scoreList.Count; i++) {

				if (i >= scoreValues.Count) {

					scoreList [scoreList.Count - 1 - i].text = (i + 1) + ". ???";
				} else {

					scoreList [scoreList.Count - 1 - i].text = (i + 1) + ". " + scoreValues [i];
				}
			}
		} else {

			for(int i = 0; i < scoreList.Count; i++)
				scoreList[scoreList.Count - 1 - i].text = (i + 1) + ". ???";
		}
	}

	public void HighscoreClicked() {

		SFXScript.Instance.PlayClickSound ();
		showing = !showing;
	}
}
