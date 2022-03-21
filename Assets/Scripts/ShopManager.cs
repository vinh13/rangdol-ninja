using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Price
{
    Video,
    Coin
}
public class ShopManager : MonoBehaviour
{
    public List<GameObject> L_skinMesh;
    public List<ItemSkin> itemSkin;
    public List<ItemWeapon> itemWeapon;
    public List<GameObject> L_Weapon;

    public GameObject btnSkin;
    public GameObject btnWeapon;
    public GameObject btnChoice;

    public GameObject btnTableSkin;
    public GameObject btnTableWeapon;
    int indexSkin;
    int indexWeapon;
    Price tempPrice;
    [Header("Price Skin")]
    public GameObject btnBuySkin;
    public GameObject PriceSkinVideo;
    public GameObject PriceSkinCoin;
    public Text txtCoinSkin;
    [Header("Price Weapon")]
    public GameObject btnBuyWeapon;
    public GameObject PriceWeaponVideo;
    public GameObject PriceWeaponCoin;
    public Text txtCoinWeapon;

    private void Awake()
    {
        setItemSkin();
        setItemWeapon();
        indexSkin = PlayerPrefs.GetInt(keysave.indexMaterial, 0);
        indexWeapon = PlayerPrefs.GetInt(keysave.indexWeapon, 0);
        reviewSkin(indexSkin,Price.Video);
    }

    private void OnEnable()
    {
        choiceSkin();
    }

    private void setItemSkin()
    {
        for (int i = 0; i < itemSkin.Count; i++)
        {
            itemSkin[i].check(reviewSkin, i );
        }
    }
    private void setItemWeapon()
    {
        for (int i = 0; i < itemWeapon.Count; i++)
        {
            itemWeapon[i].check(reviewWeapon, i );
        }
    }
    private void reviewSkin(int val, Price price)
    {

        indexSkin = val;
        for(int i = 0; i < L_skinMesh.Count; i++)
        {
            L_skinMesh[i].SetActive(indexSkin==i);
        }
       

        if (PlayerPrefs.GetInt(keysave.keySkin + indexSkin.ToString(), 0)==0)
        {
            tempPrice = price;
            btnBuySkin.SetActive(true);
            btnBuyWeapon.SetActive(false);
            if ((int)price == 0)
            {
                PriceSkinVideo.SetActive(true);
                PriceSkinCoin.SetActive(false);
            }
            else
            {
                PriceSkinVideo.SetActive(false);
                PriceSkinCoin.SetActive(true);
                txtCoinSkin.text = (keysave.Price).ToString();
            }
        }
        else
        {
            
            btnBuySkin.SetActive(false);
            btnBuyWeapon.SetActive(false);
            PlayerPrefs.SetInt(keysave.indexMaterial, indexSkin);
            ActionBase.getMaterialPlayer();
        }
       

    }
    public void BuySkin()
    {
        if ((int)tempPrice == 0)
        {
            adsManager.Instance.showReward(actionBuySkin);
        }
        else
        {
            if(PlayerPrefs.GetInt(keysave.Coin, 0)>= keysave.Price)
            {
                ActionBase.getCoinAction(-(keysave.Price));
                PlayerPrefs.SetInt(keysave.keySkin + indexSkin.ToString(), 1);
                PlayerPrefs.SetInt(keysave.indexMaterial, indexSkin);
                btnBuySkin.SetActive(false);
                ActionBase.getMaterialPlayer();
                itemSkin[indexSkin].Lock.SetActive(false);
            }
           
        }
      
    }
    void actionBuySkin()
    {
        PlayerPrefs.SetInt(keysave.keySkin + indexSkin.ToString(), 1);
        PlayerPrefs.SetInt(keysave.indexMaterial, indexSkin);
        ActionBase.getMaterialPlayer();
        btnBuySkin.SetActive(false);
        itemSkin[indexSkin].Lock.SetActive(false);
    }
    private void reviewWeapon(int val, Price price)
    {
        indexWeapon = val;
        for (int i = 0; i < L_Weapon.Count; i++)
        {
            L_Weapon[i].SetActive(indexWeapon == i);
        }
        if (PlayerPrefs.GetInt(keysave.keyWeapon + indexWeapon.ToString(), 0) == 0)
        {
            tempPrice = price;
            btnBuySkin.SetActive(false);
            btnBuyWeapon.SetActive(true);
            if ((int)price == 0)
            {
                PriceWeaponVideo.SetActive(true);
                PriceWeaponCoin.SetActive(false);
            }
            else
            {
                PriceWeaponVideo.SetActive(false);
                PriceWeaponCoin.SetActive(true);
                txtCoinWeapon.text = (keysave.Price).ToString();
            }
        }
        else
        {
            btnBuySkin.SetActive(false);
            btnBuyWeapon.SetActive(false);
            PlayerPrefs.SetInt(keysave.indexWeapon, indexWeapon);
            ActionBase.getWeaponPlayer();
        }
           
    }
    public void BuyWeapon()
    {
        if ((int)tempPrice == 0)
        {
            adsManager.Instance.showReward(actionBuyWeapon);
        }
        else
        {
            if (PlayerPrefs.GetInt(keysave.Coin, 0) >= keysave.Price)
            {
                ActionBase.getCoinAction(-(keysave.Price));
                PlayerPrefs.SetInt(keysave.keyWeapon + indexWeapon.ToString(), 1);
                PlayerPrefs.SetInt(keysave.indexWeapon, indexWeapon);
                btnBuyWeapon.SetActive(false);
                ActionBase.getWeaponPlayer();
                itemWeapon[indexWeapon].Lock.SetActive(false);
            }
                
        }

    }
    void actionBuyWeapon()
    {
        PlayerPrefs.SetInt(keysave.keyWeapon + indexWeapon.ToString(), 1);
        PlayerPrefs.SetInt(keysave.indexWeapon, indexWeapon);
        btnBuyWeapon.SetActive(false);
        ActionBase.getWeaponPlayer();
        itemWeapon[indexWeapon].Lock.SetActive(false);
    }
    public void choiceSkin()
    {
        btnChoice.transform.parent = btnSkin.transform;
        btnChoice.transform.localPosition = Vector3.zero;
        btnChoice.transform.SetSiblingIndex(0);
        if (PlayerPrefs.GetInt(keysave.keySkin + indexSkin.ToString(), 0) == 0)
        {
            btnBuySkin.SetActive(true);
            btnBuyWeapon.SetActive(false);
        }
        else
        {
            btnBuySkin.SetActive(false);

            btnBuyWeapon.SetActive(false);
        }
            btnTableSkin.SetActive(true);
    
        btnTableWeapon.SetActive(false);
       
    }
    public void choiceWeapon()
    {
        btnChoice.transform.parent = btnWeapon.transform;
        btnChoice.transform.localPosition = Vector3.zero;
        btnChoice.transform.SetSiblingIndex(0);
        if (PlayerPrefs.GetInt(keysave.keyWeapon + indexWeapon.ToString(), 0) == 0)
        {
            btnBuySkin.SetActive(false);
            btnBuyWeapon.SetActive(true);
        }
        else
        {
            btnBuySkin.SetActive(false);

            btnBuyWeapon.SetActive(false);
        }
        btnTableSkin.SetActive(false);
     
        btnTableWeapon.SetActive(true);
       
    }
    public void close()
    {
        gameObject.SetActive(false);
    }
}
