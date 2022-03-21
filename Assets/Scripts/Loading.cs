using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
	
	public Image LoadingImage;

	AsyncOperation async;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("FirstGame", 0) == 0)
        {
			PlayerPrefs.GetInt("Skin_0", 0);
			PlayerPrefs.SetInt("Skin_0", 1);
			PlayerPrefs.GetInt("Weapon_0", 0);
			PlayerPrefs.SetInt("Weapon_0", 1);
			PlayerPrefs.SetInt("FirstGame", 1);
			PlayerPrefs.GetInt(keysave.Music, 0);
			PlayerPrefs.SetInt(keysave.Music, 1);
			PlayerPrefs.GetInt(keysave.Sound, 0);
			PlayerPrefs.SetInt(keysave.Sound, 1);
			PlayerPrefs.GetInt(keysave.Vibrate, 0);
			PlayerPrefs.SetInt(keysave.Vibrate, 1);
			
		}
		
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

		while (LoadingImage.fillAmount < 1.0)
		{
			LoadingImage.fillAmount += 1.0f / 2 * Time.deltaTime;
			percent = Mathf.Floor(LoadingImage.fillAmount * 100);


			yield return null;
		}
		async.allowSceneActivation = true;
	}
}
