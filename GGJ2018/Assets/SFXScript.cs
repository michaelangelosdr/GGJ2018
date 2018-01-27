using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour {

	private static SFXScript instance = null;

	public static SFXScript Instance {

		get { 

			return instance;
		}
	}

	public AudioSource audioSource;

	public AudioClip click1;
	public AudioClip click2;
	public AudioClip click3;
	public AudioClip dialUp;
	public AudioClip cling;
	public AudioClip hit;
	public AudioClip alarm;

	float originalVolume;

	void Awake() {

		if (!instance) {

			instance = this;
			DontDestroyOnLoad (gameObject);
			audioSource = GetComponent<AudioSource> ();
			audioSource.loop = true;
			originalVolume = audioSource.volume;
		} else {

			Destroy (gameObject);
		}
	}

	public void PlayClip(AudioClip newClip, float newVolume = 1) {

		Unmute ();
		audioSource.volume = newVolume;
		audioSource.PlayOneShot (newClip);
	}

	public void Mute() {

		audioSource.volume = 0;
	}

	public void Unmute() {

		audioSource.volume = originalVolume;
	}

	public void PlayClickSound() {

		int rand = Random.Range (0, 3);

		if (rand == 0)
			PlayClip (click1, 0.25f);
		if (rand == 1)
			PlayClip (click2, 0.25f);
		if (rand == 2)
			PlayClip (click3, 0.25f);
	}

	public void PlayAlarm() {

		PlayClip (alarm, 0.75f);
	}

	public void PlayDialUp() {

		PlayClip (dialUp);
	}

	public void PlayTransfer() {

		PlayClip (hit, 0.75f);
	}

	public void PlayCling() {

		PlayClip (cling);
	}
}
