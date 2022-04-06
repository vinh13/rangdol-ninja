using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : CharacterBase
{
    public Rate rate;
    public SkinnedMeshRenderer skinMesh;
    [SerializeField]
    private Rigidbody RigWEAPON;
    [SerializeField]
    int Speed;
    PlayerController player;
    bool setPlayer;
    bool vwang;
    Rigidbody RigHip;
    private void Awake()
    {
        RigHip = Hip.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        player = PlayerController.Instance;
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
            CopyAnim[i].checkSkelet(Die, null,false);
        }
    }
    void Update()
    {
        if (setPlayer && player.alive && !vwang)
        {
            Vector3 nor = (transform.position - player.transform.position).normalized;
            RigWEAPON.velocity = -(new Vector3(nor.x, nor.y, 0)) * Speed;
           
            balence.transform.position = Hip.transform.position + -(new Vector3(nor.x, nor.y, 0));
        }
        else
        {
            pos = transform.position;
            balence.transform.position = pos + Vector3.up * 2.5f;
        }
        transform.position = new Vector3(Hip.transform.position.x, Hip.transform.position.y - 0.9f, Hip.transform.position.z);
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < CopyAnim.Count; i++)
        {
            CopyAnim[i]._Update();

        }
    }
    public void Die(float damp, typeAttack valType)
    {
        if (alive && setT_HP)
        {
            StartCoroutine(timeDelayAttack());
            int val = (int)valType;
            if (val == 1)
            {
                HP -= HPBase;
            }
            else if (val == 2)
            {
                HP -= ((HPBase / 100.0f) * 50);
            }
            else if (val == 3)
            {
                HP -= ((HPBase / 100.0f) * 35);
            }
            else if (val == 4)
            {
                HP -= ((HPBase / 100.0f) * 10);
            }
            else
            {
                HP -= infoLvl.ATKbase * damp;
            }
           
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
                StopCoroutine("timedelayGun");
                ActionBase.removeEnemy(gameObject);
                AudioManager.Instance.die();
                gameObject.SetActive(false);
            }
            else
            {
                Vector3 nor = (transform.position - player.transform.position).normalized;
                setForce((new Vector3(nor.x, nor.y, 0)) * 25);
                AudioManager.Instance.hitEnemy();
            }

        }


    }
    void animDie()
    {
        for (int i = 0; i < CopyAnim.Count; i++)
        {
            CopyAnim[i].setDie();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == keysave.tagPlayer)
        {
            setPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == keysave.tagPlayer)
        {
            setPlayer = false;
        }
    }
    private void setForce(Vector3 val)
    {
        StopCoroutine("timeSetForce");
        RigHip.velocity = val;
        StartCoroutine("timeSetForce");
    }
    IEnumerator timeSetForce()
    {
        vwang = true;
        yield return new WaitForSeconds(0.5f);
        vwang = false;
    }
}
