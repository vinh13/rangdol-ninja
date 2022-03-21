using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Setting : MonoBehaviour
{
    public GameObject icon;
    [SerializeField] List<GameObject> L_btn;
    [SerializeField] List<Image> L_img;
    [SerializeField] List<GameObject> L_iconMusic;
    int indexMusic;
    [SerializeField] List<GameObject> L_iconSound;
    int indexSound;
    [SerializeField] List<GameObject> L_iconVibrate;
    int indexVibrate;
    bool setTouch;
    int rota;
    int pos;
    int fadeImg;
    private void Awake()
    {
        fadeImg = 1;
        rota = 180;
        pos = 100;
        setTouch = false;
        indexMusic = PlayerPrefs.GetInt(keysave.Music, 0);
        indexSound = PlayerPrefs.GetInt(keysave.Sound, 0);
        indexVibrate = PlayerPrefs.GetInt(keysave.Vibrate, 0);
    }
    private void Start()
    {
        for (int i = 0; i < L_iconMusic.Count; i++)
        {
            L_iconMusic[i].SetActive(i == indexMusic);
        }
        PlayerPrefs.SetInt(keysave.Sound, indexSound);
        for (int i = 0; i < L_iconSound.Count; i++)
        {
            L_iconSound[i].SetActive(i == indexSound);
        }
        for (int i = 0; i < L_iconVibrate.Count; i++)
        {
            L_iconVibrate[i].SetActive(i == indexVibrate);
        }
    }
    public void setMusic()
    {
        indexMusic = (indexMusic == 1) ?0:1;
        PlayerPrefs.SetInt(keysave.Music, indexMusic);
        for (int i = 0; i < L_iconMusic.Count; i++)
        {
            L_iconMusic[i].SetActive(i == indexMusic);


        }
    }
    public void setSound()
    {
        indexSound = (indexSound == 1) ? 0 : 1;
        PlayerPrefs.SetInt(keysave.Sound, indexSound);
        for (int i = 0; i < L_iconSound.Count; i++)
        {
            L_iconSound[i].SetActive(i == indexSound);


        }
    }
    public void setVibrate()
    {
        indexVibrate = (indexVibrate == 1) ? 0 : 1;
        PlayerPrefs.SetInt(keysave.Vibrate, indexVibrate);
        for (int i = 0; i < L_iconVibrate.Count; i++)
        {
            L_iconVibrate[i].SetActive(i == indexVibrate);


        }
    }
    public void click()
    {
        if (!setTouch)
        {
            for(int i=0;i< L_btn.Count; i++)
            {
                L_btn[i].transform.DOLocalMove((Vector3.right * (pos * (i+1))), 0.5f);
            }
            for (int i = 0; i < L_img.Count; i++)
            {

                L_img[i].DOFade(fadeImg, 0.5f);
            }
            fadeImg = (fadeImg == 1) ? 0 : 1;
            pos = (pos == 100) ? 0 : 100;
            setTouch = true;
            icon.transform.DOLocalRotate(Vector3.forward * (rota), 0.5f).OnComplete(() => {
                setTouch = false;
                rota = (rota == 180) ? 0 : 180;
            });
        }
       

    }
    public void btnRestore()
    {
#if UNITY_ANDROID

        Purchaser.Instance.BtnRestore();

#else
        Purchaser.Instance.RestorePurchases();
#endif

    }
}
