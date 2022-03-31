using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : WeaponBase
{
    int speed;
    [SerializeField] bool setGunPlayer;
  
    private void Start()
    {
        speed = keysave.SpeedBullet;
    }
    private void Update()
    {
        transform.position += transform.up*(speed * Time.deltaTime);
    }
   IEnumerator timeOff()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if (setGunPlayer)
        {
           // AudioManager.Instance.playSound(AudioManager.Instance.spiderShoot);
        }
        else
        {
           // AudioManager.Instance.playSound(AudioManager.Instance.arrowShoot);
        }
        StartCoroutine("timeOff");
    }
    private void OnDisable()
    {
        StopCoroutine("timeOff");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == keysave.Ground)
        {
            gameObject.SetActive(false);
        }
    }


}
