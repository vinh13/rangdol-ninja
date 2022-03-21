using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpider : WeaponBase
{

    [SerializeField] GameObject pos;
    RaycastHit hit;
    bool setShot;
    private void Start()
    {
        setShot = true;
       
    }

    IEnumerator timedelayGun()
    {
        BulletManager.Instance.GunShotPlayer(pos);
        setShot = false;
        yield return new WaitForSeconds(0.4f);
        setShot = true;
      
    }
    private void Update()
    {
        if (setShot)
        {
            if (Physics.Raycast(pos.transform.position, pos.transform.up, out hit,10))
            {
                Debug.DrawRay(pos.transform.position, pos.transform.up * 10, Color.green);

                if (hit.transform.gameObject.layer==9)
                {
                    startShot();

                }
               
            }
        }
    }
    public void stopShot()
    {
        setShot = false;
        StopCoroutine("timedelayGun");
    }
    public void startShot()
    {
        setShot = true;
        StartCoroutine("timedelayGun");
    }
   
}
