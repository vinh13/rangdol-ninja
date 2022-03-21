using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    int Coin;
    public Text txtCoin;
    int indexLevel;
    public Text txtLevel;
    public Text txtGetCoinAds;
    public GameObject MenuMain;
    public GameObject GamePlayUI;
    public GameObject WinUI;
    public GameObject LoseUI;
    public GameObject ShopUI;
    [Header("Update HP & ATK")]
    public DataUpdate dataUpgrape; 
    public GameObject updateHP;
    public GameObject btnUpgrapeHpCoin;
    public GameObject btnUpgrapeHpAds;
    public Text txtLevelHp;
    public Text txtLevelHpUpgrape;

    int levelHp;
    public GameObject updateATK;
    public GameObject btnUpgrapeATKCoin;
    public GameObject btnUpgrapeaTKAds;
    public Text txtLevelAtk;
    public Text txtLevelAtkUpgrape;
    int levelAtk;
    public GameObject removeAdsBtn;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        Coin = PlayerPrefs.GetInt(keysave.Coin, 0);
        indexLevel = PlayerPrefs.GetInt(keysave.Level, 0);
        levelHp = PlayerPrefs.GetInt(keysave.updateHP, -1);
        levelAtk = PlayerPrefs.GetInt(keysave.updateWeapon, -1);
    }
    private void OnEnable()
    {
        ActionBase.getCoinAction += getCoin;
        ActionBase.getLevelAction += getLevel;
        ActionBase.setLevelAction += setLevel;
        ActionBase.WinUIActon += onWin;
        ActionBase.LoseUIActon += onLose;
    }
    private void OnDisable()
    {
        ActionBase.getCoinAction -= getCoin;
        ActionBase.getLevelAction -= getLevel;
        ActionBase.setLevelAction -= setLevel;
        ActionBase.WinUIActon -= onWin;
        ActionBase.LoseUIActon -= onLose;
    }
    private void setLevel()
    {
        ++indexLevel;
        PlayerPrefs.SetInt(keysave.Level, indexLevel);
    }
    private void Start()
    {
        getCoin(0);
        getLevel(0);
        txtGetCoinAds.text = keysave.GetCoinAds.ToString();
        checkUpgapeHP();
        checkUpgapeATK();
        removeAdsBtn.SetActive(PlayerPrefs.GetInt(Purchaser.removeAds, 0) == 0);
    }
   private void onWin()
    {
        WinUI.SetActive(true);
    }
    private void onLose()
    {
        LoseUI.SetActive(true);
    }
    private void getCoin(int val)
    {
        Coin += val;
        txtCoin.text = Coin.ToString();
        PlayerPrefs.SetInt(keysave.Coin, Coin);
    }
    private void getLevel(int val)
    {
        indexLevel += val;
        txtLevel.text ="Level "+ (indexLevel+1).ToString();
        PlayerPrefs.SetInt(keysave.Level, indexLevel);
    }
    public void onGetCoinAds()
    {
        adsManager.Instance.showReward(actiongetCoin);
    }
    void actiongetCoin()
    {
        getCoin(keysave.GetCoinAds);
    }
    public void playGame()
    {
        MenuMain.SetActive(false);
        GamePlayUI.SetActive(true);
    }
    public void onShop()
    {
        ShopUI.SetActive(true);
    }
    public void onPause()
    {
        Time.timeScale = (Time.timeScale==1)?0:1;
    }
    private void checkUpgapeHP()
    {
        txtLevelHp.text = "Level " + (levelHp + 2).ToString();
        if (levelHp+2< dataUpgrape.infoLevels.Count)
        {
         
            if (Coin >= dataUpgrape.infoLevels[levelHp + 1].Price)
            {
                btnUpgrapeHpCoin.SetActive(true);
                txtLevelHpUpgrape.text = (dataUpgrape.infoLevels[levelHp + 1].Price).ToString();
                btnUpgrapeHpAds.SetActive(false);

            }
            else
            {
                btnUpgrapeHpCoin.SetActive(false);
                btnUpgrapeHpAds.SetActive(true);
            }
        }
        else
        {
            updateHP.SetActive(true);
            btnUpgrapeHpCoin.SetActive(false);
            btnUpgrapeHpAds.SetActive(false);
        }
        
    }
    public void BuyCoinUpgrapeHP()
    {
        int tempCoi = dataUpgrape.infoLevels[levelHp + 1].Price;
        if (Coin >= tempCoi)
        {
            getCoin(-tempCoi);
            ++levelHp;
            txtLevelHp.text = (levelHp + 2).ToString();
            PlayerPrefs.SetInt(keysave.updateHP, levelHp);
            ActionBase.HpUpgrape();

        }
        checkUpgapeHP();
        checkUpgapeATK();
    }
    public void BuyAdsUpgrapeHP()
    {
        adsManager.Instance.showReward(actionUpHp);
     
    }
    void actionUpHp()
    {
        ++levelHp;
        txtLevelHp.text = (levelHp + 2).ToString();
        PlayerPrefs.SetInt(keysave.updateHP, levelHp);
        ActionBase.HpUpgrape();
        checkUpgapeHP();
        checkUpgapeATK();
    }
    private void checkUpgapeATK()
    {
        txtLevelAtk.text = "Level "+(levelAtk + 2).ToString();
        if (levelAtk+2 < dataUpgrape.infoLevels.Count)
        {
           
            if (Coin >= dataUpgrape.infoLevels[levelAtk + 1].Price)
            {
                btnUpgrapeATKCoin.SetActive(true);
                txtLevelAtkUpgrape.text = dataUpgrape.infoLevels[levelAtk + 1].Price.ToString();
                btnUpgrapeaTKAds.SetActive(false);
            }
            else
            {
                btnUpgrapeATKCoin.SetActive(false);
                btnUpgrapeaTKAds.SetActive(true);
            }
        }
        else
        {
            updateATK.SetActive(true);
            btnUpgrapeATKCoin.SetActive(false);
            btnUpgrapeaTKAds.SetActive(false);
        }

    }
    public void BuyCoinUpgrapeAtk()
    {
        int tempCoi = dataUpgrape.infoLevels[levelAtk + 1].Price;
        if (Coin >= tempCoi)
        {
            getCoin(-tempCoi);
            ++levelAtk;
            txtLevelAtk.text = (levelAtk + 2).ToString();
            PlayerPrefs.SetInt(keysave.updateWeapon, levelAtk);
            ActionBase.AtkUpgrape();
        }
        checkUpgapeHP();
        checkUpgapeATK();
    }
    public void BuyAdsUpgrapeAtk()
    {
        adsManager.Instance.showReward(actioUpAtk);
    }
    void actioUpAtk()
    {
        ++levelAtk;
        txtLevelAtk.text = (levelAtk + 2).ToString();
        PlayerPrefs.SetInt(keysave.updateWeapon, levelAtk);
        ActionBase.AtkUpgrape();
        checkUpgapeHP();
        checkUpgapeATK();
    }
    public void btnremoveads()
    {
        Purchaser.Instance.PruductID(Purchaser.removeAds, actionRemoveAds);
    }
    void actionRemoveAds()
    {
        PlayerPrefs.SetInt(Purchaser.removeAds, 1);
        removeAdsBtn.SetActive(false);
    }
}
