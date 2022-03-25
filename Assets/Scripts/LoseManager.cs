using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoseManager : MonoBehaviour
{
    [SerializeField] GameObject txtLevel;
    public GameObject LoseBase;
    public GameObject MainMenu;
    public GameObject GamePlayUI;
    public GameObject BoxStart;
    public Image countDown;
    public Text txtTimecountDown;
    public GameObject btnRevive;
    [SerializeField] GameObject btnNothanks;
    private void OnEnable()
    {
        GamePlayUI.SetActive(false);
        BoxStart.SetActive(false);
        txtLevel.SetActive(false);
        btnNothanks.SetActive(false);
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
        Invoke("timeothanks", 2);
        countDown.DOFillAmount(0, 10).OnComplete(() =>
        {
            // btnRevive.SetActive(false);
            MainMenu.SetActive(true);
            ActionBase.replayLevelAction();
            gameObject.SetActive(false);
        });
        LoseBase.SetActive(true);
    }
    private void timeothanks()
    {
        btnNothanks.SetActive(true);
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
        BoxStart.SetActive(true);
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
        txtLevel.SetActive(true);
        countDown.DOKill();
    }
}
