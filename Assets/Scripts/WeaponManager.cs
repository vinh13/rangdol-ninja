using System.Collections;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject Blood;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            StopCoroutine("timeBlood");
            StartCoroutine("timeBlood");
          
        }
        else if (collision.gameObject.tag == "Cut")
        {
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
