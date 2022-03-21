using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance;
    public List<bullet> Bullet;
    public List<bullet> BulletPlayer;
    int index;
    int indexP;
    private void Awake()
    {
        index = 0;
        indexP = 0;
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
    public void GunShot(GameObject par)
    {
        Bullet[index].gameObject.SetActive(true);
        Bullet[index].transform.position = par.transform.position;
        Bullet[index].transform.up = new Vector3(par.transform.forward.x, par.transform.forward.y, 0);
        index++;
        if(index>= Bullet.Count)
        {
            index = 0;
        }
    }
    public void GunShotPlayer(GameObject par)
    {
        BulletPlayer[indexP].gameObject.SetActive(true);
        BulletPlayer[indexP].transform.position = new Vector3(par.transform.position.x, par.transform.position.y,0);
        BulletPlayer[indexP].transform.up = new Vector3(par.transform.up.x, par.transform.up.y, 0);
        indexP++;
        if (indexP >= BulletPlayer.Count)
        {
            indexP = 0;
        }
    }
}
