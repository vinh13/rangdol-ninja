using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public Text txtCoin;
    public GameObject MainMenu;
    public GameObject GamePlayUI;
    public GameObject WinBase;
    public List<GameObject> Contine_L;
    int index_x5;
    int coin;
    private void Awake()
    {
        index_x5 = 0;
    }
    private void OnEnable()
    {
        coin = GameManager.Instance.getcoininWin(PlayerPrefs.GetInt(keysave.Level, 0));
        StartCoroutine("timedelaybase");
        txtCoin.text = coin.ToString();
        GamePlayUI.SetActive(false);
        ActionBase.setLevelAction();
    }
    IEnumerator timedelaybase()
    {
        WinBase.SetActive(false);
        yield return new WaitForSeconds(1);
        WinBase.SetActive(true);
        Contine_L[0].SetActive(0 == (index_x5 % 2));
        Contine_L[1].SetActive(0 != (index_x5 % 2));
        index_x5++;

    }
    public void Contine()
    {
        ActionBase.getCoinAction(coin);
        MainMenu.SetActive(true);
        ActionBase.nextLevelAction();
        gameObject.SetActive(false);
    }
    public void onClaim_X5()
    {
        adsManager.Instance.showReward(actionClaim);
    }
    void actionClaim()
    {
        ActionBase.getCoinAction(coin * keysave.claimVal);
        MainMenu.SetActive(true);
        ActionBase.nextLevelAction();
        gameObject.SetActive(false);
    }
}
