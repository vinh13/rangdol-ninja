using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoseManager : MonoBehaviour
{
    public GameObject LoseBase;
    public GameObject MainMenu;
    public GameObject GamePlayUI;
    public Image countDown;
    public Text txtTimecountDown;
    public GameObject btnRevive;
    private void OnEnable()
    {
        GamePlayUI.SetActive(false);
        StartCoroutine("timedelaybase");
    }
  
   
    IEnumerator timedelaybase()
    {
        LoseBase.SetActive(false);
        yield return new WaitForSeconds(1);
        btnRevive.SetActive(true);
        countDown.DOKill();
        countDown.fillAmount = 1;
        StartCoroutine("timeCountDown");
        countDown.DOFillAmount(0, 10).OnComplete(() =>
        {
            // btnRevive.SetActive(false);
            MainMenu.SetActive(true);
            ActionBase.replayLevelAction();
            gameObject.SetActive(false);
        });
        LoseBase.SetActive(true);
    }
    IEnumerator timeCountDown()
    {
        float time = keysave.timeCountDownLose;
        while (time > 0)
        {
            txtTimecountDown.text = ((int)time).ToString();
            time -= Time.deltaTime;
            yield return null;
        }
        time = 0;
        txtTimecountDown.text = ((int)time).ToString();

    }
    public void onRevive()
    {
        
        adsManager.Instance.showReward(actionRevive);

    }
    void actionRevive()
    {
        ActionBase.ReviveAction();
        GamePlayUI.SetActive(true);
        gameObject.SetActive(false);
    }
    public void nothank()
    {
        MainMenu.SetActive(true);
        ActionBase.replayLevelAction();
        gameObject.SetActive(false);
        
    }
    private void OnDisable()
    {
        countDown.DOKill();
    }
}
