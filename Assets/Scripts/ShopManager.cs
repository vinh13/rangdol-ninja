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
    [SerializeField] GameObject select;
    [SerializeField] GameObject Char;
    [SerializeField] GameObject Weapon; 
    [SerializeField] GameObject CharBase;
    [SerializeField] GameObject WeaponBase;
  

    private void Awake()
    {
       
        setItemSkin();
        setItemWeapon();
        indexSkin = PlayerPrefs.GetInt(keysave.indexMaterial, 0);
        indexWeapon = PlayerPrefs.GetInt(keysave.indexWeapon, 0);
        reviewSkin(indexSkin,Price.Video);
        select.transform.parent = itemSkin[indexSkin].transform;
        select.transform.localPosition = Vector3.zero;
        select.transform.SetSiblingIndex(0);
        Char.SetActive(true);
        Weapon.SetActive(false);
        for (int i = 0; i < L_Weapon.Count; i++)
        {
            L_Weapon[i].SetActive(indexWeapon == i);
        }
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
    int indexBuySkin;
    private void reviewSkin(int val, Price price)
    {

        indexBuySkin = val;
        CharBase.SetActive(true);


        if (PlayerPrefs.GetInt(keysave.keySkin + indexBuySkin.ToString(), 0)==0)
        {
            for (int i = 0; i < L_skinMesh.Count; i++)
            {
                L_skinMesh[i].SetActive(false);
            }
          
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
          
            indexSkin = val;
            for (int i = 0; i < L_skinMesh.Count; i++)
            {
                L_skinMesh[i].SetActive(indexSkin == i);
            }
            CharBase.SetActive(false);
            btnBuySkin.SetActive(false);
            btnBuyWeapon.SetActive(false);
            PlayerPrefs.SetInt(keysave.indexMaterial, indexSkin);
            ActionBase.getMaterialPlayer();
            select.transform.parent = itemSkin[indexSkin].transform;
            select.transform.localPosition = Vector3.zero;
            select.transform.SetSiblingIndex(0);
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
                PlayerPrefs.SetInt(keysave.keySkin + indexBuySkin.ToString(), 1);
                PlayerPrefs.SetInt(keysave.indexMaterial, indexBuySkin);
                btnBuySkin.SetActive(false);
                ActionBase.getMaterialPlayer();
                itemSkin[indexBuySkin].Lock.SetActive(false);
                select.transform.parent = itemSkin[indexBuySkin].transform;
                select.transform.localPosition = Vector3.zero;
                select.transform.SetSiblingIndex(0);
                indexSkin = indexBuySkin;
                CharBase.SetActive(false);
                for (int i = 0; i < L_skinMesh.Count; i++)
                {
                    L_skinMesh[i].SetActive(indexSkin == i);
                }
            }
           
        }
      
    }
    void actionBuySkin()
    {
        PlayerPrefs.SetInt(keysave.keySkin + indexBuySkin.ToString(), 1);
        PlayerPrefs.SetInt(keysave.indexMaterial, indexBuySkin);
        ActionBase.getMaterialPlayer();
        btnBuySkin.SetActive(false);
        itemSkin[indexBuySkin].Lock.SetActive(false);
        select.transform.parent = itemSkin[indexBuySkin].transform;
        select.transform.localPosition = Vector3.zero;
        select.transform.SetSiblingIndex(0);
        indexSkin = indexBuySkin;
        CharBase.SetActive(false);
        for (int i = 0; i < L_skinMesh.Count; i++)
        {
            L_skinMesh[i].SetActive(indexSkin == i);
        }
    }
    int indexBuyWeapon;
    private void reviewWeapon(int val, Price price)
    {
        indexBuyWeapon = val;
        WeaponBase.SetActive(true);
        if (PlayerPrefs.GetInt(keysave.keyWeapon + indexBuyWeapon.ToString(), 0) == 0)
        {
            for (int i = 0; i < L_Weapon.Count; i++)
            {
                L_Weapon[i].SetActive(false);
            }
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
            WeaponBase.SetActive(false);
            indexWeapon = val;
            for (int i = 0; i < L_Weapon.Count; i++)
            {
                L_Weapon[i].SetActive(indexWeapon == i);
            }
            btnBuySkin.SetActive(false);
            btnBuyWeapon.SetActive(false);
            PlayerPrefs.SetInt(keysave.indexWeapon, indexWeapon);
            ActionBase.getWeaponPlayer();
            select.transform.parent = itemWeapon[indexWeapon].transform;
            select.transform.localPosition = Vector3.zero;
            select.transform.SetSiblingIndex(0);
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
                PlayerPrefs.SetInt(keysave.keyWeapon + indexBuyWeapon.ToString(), 1);
                PlayerPrefs.SetInt(keysave.indexWeapon, indexBuyWeapon);
                btnBuyWeapon.SetActive(false);
                ActionBase.getWeaponPlayer();
                itemWeapon[indexBuyWeapon].Lock.SetActive(false);
                select.transform.parent = itemWeapon[indexBuyWeapon].transform;
                select.transform.localPosition = Vector3.zero;
                select.transform.SetSiblingIndex(0);
                indexWeapon = indexBuyWeapon;
                WeaponBase.SetActive(false);
                for (int i = 0; i < L_Weapon.Count; i++)
                {
                    L_Weapon[i].SetActive(indexWeapon == i);
                }
            }
                
        }

    }
    void actionBuyWeapon()
    {
        PlayerPrefs.SetInt(keysave.keyWeapon + indexBuyWeapon.ToString(), 1);
        PlayerPrefs.SetInt(keysave.indexWeapon, indexBuyWeapon);
        btnBuyWeapon.SetActive(false);
        ActionBase.getWeaponPlayer();
        itemWeapon[indexBuyWeapon].Lock.SetActive(false);
        select.transform.parent = itemWeapon[indexBuyWeapon].transform;
        select.transform.localPosition = Vector3.zero;
        select.transform.SetSiblingIndex(0);
        indexWeapon = indexBuyWeapon;
        WeaponBase.SetActive(false);
        for (int i = 0; i < L_Weapon.Count; i++)
        {
            L_Weapon[i].SetActive(indexWeapon == i);
        }
    }

    public void choiceSkin()
    {
        Char.SetActive(true);
        Weapon.SetActive(false);
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
     
        select.transform.parent = itemSkin[indexSkin].transform;
        select.transform.localPosition = Vector3.zero;
        select.transform.SetSiblingIndex(0);



    }
   
    public void choiceWeapon()
    {
        Char.SetActive(false);
        Weapon.SetActive(true);
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
        select.transform.parent = itemWeapon[indexWeapon].transform;
        select.transform.localPosition = Vector3.zero;
        select.transform.SetSiblingIndex(0);


    }
    public void close()
    {
        gameObject.SetActive(false);
    }
}
