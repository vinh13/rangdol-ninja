using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    public AudioSource BG;
    public AudioClip arrowShoot;
    public List<AudioClip> L_Die;
    public List<AudioClip> L_HitEnemy;
    public List<AudioClip> L_HitPlayer;
    public AudioClip sawActive;
    public AudioClip spiderShoot;
    public AudioClip Ui;
    public AudioClip Win;
    public AudioClip thunggo;
    public AudioClip upgrade;
    public AudioClip tabtoplay;
    private void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }
    private void Start()
    {
        audioSource.volume = PlayerPrefs.GetInt(keysave.Sound, 0);
        BG.volume = PlayerPrefs.GetInt(keysave.Sound, 0);
    }
    public void setSound(int val)
    {
        audioSource.volume = val;
        BG.volume = val;
    }
    public void onBg(bool val)
    {
        BG.mute = val;
    }
    public void playSound(AudioClip _clip)
    {
        audioSource.PlayOneShot(_clip);
    }
    public void die()
    {
        int a = Random.Range(0, L_Die.Count);
        audioSource.PlayOneShot(L_Die[a]);
    }
    public void hitEnemy()
    {
        int a = Random.Range(0, L_HitEnemy.Count);
        audioSource.PlayOneShot(L_HitEnemy[a]);
    }
    public void hitPlayer()
    {
        int a = Random.Range(0, L_HitPlayer.Count);
        audioSource.PlayOneShot(L_HitPlayer[a]);
    }
}
