using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{


	float tece;
	AsyncOperation async;
    private void Awake()
    {
		tece = 0;

		if (PlayerPrefs.GetInt("FirstGame", 0) == 0)
        {
			PlayerPrefs.GetInt(keysave.Coin, 0);
			PlayerPrefs.GetInt("Skin_0", 0);
			PlayerPrefs.SetInt("Skin_0", 1);
			PlayerPrefs.GetInt("Weapon_0", 0);
			PlayerPrefs.SetInt("Weapon_0", 1);
			PlayerPrefs.SetInt("FirstGame", 1);
		
			PlayerPrefs.GetInt(keysave.Sound, 0);
			PlayerPrefs.SetInt(keysave.Sound, 1);
			PlayerPrefs.GetInt(keysave.Vibrate, 0);
			PlayerPrefs.SetInt(keysave.Vibrate, 1);
			
		}
		Application.targetFrameRate = 60;
		
    }
    private void Start()
	{
		StartCoroutine(LoadScene());
	}
	private IEnumerator LoadScene()
	{
		async = SceneManager.LoadSceneAsync(1);
		async.allowSceneActivation = false;
		float percent = 0;

		while (tece < 1.0)
		{
			tece += 1.0f / 1 * Time.deltaTime;


			yield return null;
		}
		async.allowSceneActivation = true;
	}
}
