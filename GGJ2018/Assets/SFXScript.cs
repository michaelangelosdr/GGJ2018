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

	public void PlayClip(AudioClip newClip) {

		Unmute ();
		audioSource.PlayOneShot (newClip);
	}

	public void Mute() {

		audioSource.volume = 0;
	}

	public void Unmute() {

		audioSource.volume = originalVolume;
	}
}
