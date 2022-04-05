using System.Collections;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject Blood;
    public AudioClip sound;

  
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Vector3 globalPositionOfContact = collision.contacts[0].point;
            Vector3 tempForw = (globalPositionOfContact - transform.position).normalized;
            ActionBase.setForceAc(tempForw * 50);
            AudioManager.Instance.playSound(sound);
            StopCoroutine("timeBlood");
            StartCoroutine("timeBlood");
        }
        else if (collision.gameObject.tag == "Cut")
        {
            Vector3 globalPositionOfContact = collision.contacts[0].point;
            Vector3 tempForw = (globalPositionOfContact - transform.position).normalized;
            ActionBase.setForceAc(tempForw * 50);
            AudioManager.Instance.playSound(sound);
            StopCoroutine("timeBlood");
            StartCoroutine("timeBlood");
            StopCoroutine("timeScale");
            StartCoroutine("timeScale");
        }
    }
    IEnumerator timeScale()
    {
        ActionBase.moveCam();
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(0.8f);
        Time.timeScale = 1;

    }
    IEnumerator timeBlood()
    {
        Blood.SetActive(true);
        yield return new WaitForSeconds(1);
        Blood.SetActive(false);
    }

}
