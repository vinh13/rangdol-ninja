using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    bool alive;
    [SerializeField] GameObject Model;
    [SerializeField] GameObject Ex;
    [SerializeField] GameObject Pari;
    private void Awake()
    {
        alive = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (alive)
        {
            if (other.gameObject.tag == keysave.tagBarrel)
            {
                alive = false;
                Ex.SetActive(true);
                Pari.SetActive(true);
                Model.SetActive(false);
                StartCoroutine("timeDie");

            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (alive)
        {
            if (collision.gameObject.tag == keysave.tagWeapon || collision.gameObject.tag == keysave.tagPlayer || collision.gameObject.tag == keysave.tagSpendPlayer || collision.gameObject.tag == keysave.enemy ||collision.gameObject.tag == keysave.tagCut )
            {
                alive = false;
                Ex.SetActive(true);
                Pari.SetActive(true);
                Model.SetActive(false);
                StartCoroutine("timeDie");
              
            }
        }
    }
    IEnumerator timeDie()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
