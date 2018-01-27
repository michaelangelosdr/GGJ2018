using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour {

	private static BGMScript instance = null;

	public static BGMScript Instance {

		get { 
		
			return instance;
		}
	}

	public AudioClip MainMenuBGM;
	public AudioClip GameBGM;

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
		audioSource.clip = newClip;
		audioSource.Play ();
	}

	public void PlayMainMenuBGM() {

		PlayClip (MainMenuBGM);
	}

	public void PlayGameBGM() {
	
		PlayClip (GameBGM);
	}

	public void Mute() {

		audioSource.volume = 0;
	}

	public void Unmute() {

		audioSource.volume = originalVolume;
	}
}
