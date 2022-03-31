using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SworkManagerEnemy : WeaponBase
{
    [SerializeField] GameObject Blood;

   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopCoroutine("timeBlood");
            StartCoroutine("timeBlood");

        }
        else if (collision.gameObject.tag == "Cut")
        {
            StopCoroutine("timeBlood");
            StartCoroutine("timeBlood");
          
        }
    }
   
    IEnumerator timeBlood()
    {
        Blood.SetActive(true);
        yield return new WaitForSeconds(1);
        Blood.SetActive(false);
    }
}
