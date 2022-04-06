using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] GameObject box;
    [SerializeField] GameObject ob_Break;
    bool alive;
    private void Awake()
    {
        alive = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (alive)
        {
           
            if (collision.gameObject.tag == keysave.tagWeapon|| collision.gameObject.tag == keysave.tagPlayer)
            {
                AudioManager.Instance.playSound(AudioManager.Instance.thunggo);
                alive = false;
                ob_Break.SetActive(true);
                box.SetActive(false);
                StartCoroutine("timeDie");
            }
        }
    }
    IEnumerator timeDie()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
