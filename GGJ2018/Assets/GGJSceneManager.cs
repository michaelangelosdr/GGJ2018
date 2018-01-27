using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GGJSceneManager : MonoBehaviour {



	private static GGJSceneManager instance = null;
	public static GGJSceneManager Instance
	{
		get {
			return instance;
		}
	}
		
	void Awake()
	{
		instance = this;
		DontDestroyOnLoad (this.gameObject);
	}


	public void LoadScene(string SceneName)
	{
		SceneManager.LoadScene (SceneName);
	}

}
