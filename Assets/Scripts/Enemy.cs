using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public enum Rate
{
    nomal,
    boss
}
public enum typeSword
{
    Left,
    Right
}

public class Enemy : CharacterBase
{
    [SerializeField] typeSword type;
    [SerializeField] List<GameObject> L_Weapon;
    [SerializeField] List<LineRenderer> L_Laser;
    public SkinnedMeshRenderer skinMesh;
    public Rate rate;
    GameObject Gun;
    bool setGun;
    RaycastHit hit;
    LineRenderer laserLine;
    float timeGun;
    [SerializeField] bool SetGun;


    private void Start()
    {
        ActionBase.GetEnemy(gameObject);
        int val_rate = 1;
        if ((int)rate == 1)
        {
            val_rate = 4;
        }

        HP = infoLvl.HPbase * val_rate;
        HPBase = HP;
        ActionBase.getCountInGamePlay();
        for (int i = 0; i < CopyAnim.Count; i++)
        {
            CopyAnim[i].checkSkelet(Die, StopGun);
        }

        //       Sword
        for (int i = 0; i < L_Weapon.Count; i++)
        {
            L_Weapon[i].SetActive(i == ((int)type));
        }
        if (SetGun)
        {
            Gun = L_Weapon[((int)type)];
            laserLine = L_Laser[((int)type)];
        }
        setStartGun = false;



    }
    void Update()
    {
        shotGun();
        transform.position = new Vector3(Hip.transform.position.x, Hip.transform.position.y - 0.9f, Hip.transform.position.z);
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < CopyAnim.Count; i++)
        {
            CopyAnim[i]._Update();

        }
    }
    void animDie()
    {
        for (int i = 0; i < CopyAnim.Count; i++)
        {
            CopyAnim[i].setDie();
        }
    }
    private void StopGun()
    {
        if (laserLine != null)
        {
            laserLine.gameObject.SetActive(false);
        }

        StopCoroutine("timedelayGun");
    }
    private void shotGun()
    {
        if (setGun)
        {
            if (Physics.Raycast(Gun.transform.position, Gun.transform.forward, out hit))
            {

                laserLine.SetPosition(0, new Vector3(Gun.transform.position.x, Gun.transform.position.y, 0));
                if (hit.collider)
                {
                    laserLine.SetPosition(1, new Vector3(hit.point.x, hit.point.y, 0));

                }
            }
        }

    }
    public void Die(float damp)
    {
        if (alive)
        {
            HP -= infoLvl.ATKbase * damp;
            setUIBlood();
            if (HP <= 0)
            {
                EffectBloodManager.Instance.showEffectDie(gameObject);
                alive = false;
                string NameLevel = "Material/Die";
                Material mar = Resources.Load(NameLevel) as Material;
                skinMesh.material = mar;
                animDie();
                ActionBase.getChildAction(skinMesh.gameObject);
                ActionBase.checkLevelAction();
                Hip.gameObject.layer = 11;
                gameObject.tag = keysave.tagDie;
                if (setGun)
                {
                    laserLine.gameObject.SetActive(false);
                }
                StopCoroutine("timedelayGun");
                ActionBase.removeEnemy(gameObject);
                AudioManager.Instance.die();
                gameObject.SetActive(false);
            }
            else
            {
                AudioManager.Instance.hitEnemy();
            }
          
        }


    }
    IEnumerator timedelayGun()
    {
        yield return new WaitForSeconds(timeGun);
        BulletManager.Instance.GunShot(Gun);
        StartCoroutine("timedelayGun");
    }
    bool setStartGun;

    private void OnTriggerEnter(Collider other)
    {
        if (!setStartGun)
        {
          
            if (other.gameObject.tag == keysave.play)
            {
                if (Gun != null)
                {
                    setStartGun = true;
                    timeGun = keysave.timeGun;
                    setGun = true;
                }
                if (Gun != null)
                {

                    StartCoroutine("timedelayGun");
                }
                else
                {
                    setStartGun = true;
                }
            }

        }

    }

}
