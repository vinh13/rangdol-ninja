using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerController : CharacterBase
{
    public static PlayerController Instance;

    GameObject Weapon;
    private Joystick joy;
    public Rigidbody LeftArmRig;
    [HideInInspector]
    public Rigidbody RigWEAPON;
    int Speed;
    bool setanim;
    bool setWin;
    bool setRevive;
    [SerializeField] List<SkinnedMeshRenderer> L_Skin;
    int indexMae;
    int indexWeapon;
    Vector3 celocity;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        setanim = true;
        setanim = false;
        setRevive = false;
        Speed = 32;
    }
    public void chooseWeapon()
    {
        indexWeapon = PlayerPrefs.GetInt(keysave.indexWeapon, 0);
        GameObject oldWeapon = Weapon;
        Destroy(oldWeapon);
        string NameSkin = "Weapon/" + indexWeapon;
        GameObject tempwea = Resources.Load(NameSkin) as GameObject;
        GameObject wea = Instantiate(tempwea);
        wea.transform.parent = LeftArmRig.transform;
        wea.transform.localEulerAngles = Vector3.zero;
        wea.transform.localPosition = Vector3.zero;
        Weapon = wea;
    }

    private void Start()
    {
        setHp();

        ActionBase.getTagetCamAction(gameObject);
        ActionBase.getJoyStickAction = getJoy;
        ActionBase.ReviveAction = Revive;
        ActionBase.winPlayer = win;
        ActionBase.HpUpgrape = setHp;
        ActionBase.getMaterialPlayer = setMaterial;
        ActionBase.getWeaponPlayer = chooseWeapon;
        for (int i = 0; i < CopyAnim.Count; i++)
        {
            CopyAnim[i].checkSkelet(Die, null);
        }
        setMaterial();
        Invoke("chooseWeapon", 1);
    }

    private void setHp()
    {
        int levelHp = PlayerPrefs.GetInt(keysave.updateHP, -1);
        float hpUpgrape = 0;
        if (levelHp >= 0)
        {

            hpUpgrape = CanvasManager.Instance.dataUpgrape.infoLevels[levelHp].HPbase;
        }

        HP = infoLvl.HPbase + hpUpgrape;
        HPBase = HP;
    }
    private void setMaterial()
    {
        indexMae = PlayerPrefs.GetInt(keysave.indexMaterial, 0);
        string NameSkin = "Skin/skin";
        Material mar = Resources.Load(NameSkin) as Material;
        L_Skin[indexMae].material = mar;
        for (int i = 0; i < L_Skin.Count; i++)
        {
            L_Skin[i].gameObject.SetActive(i == indexMae);
        }
    }

    private void OnDisable()
    {
        ActionBase.getJoyStickAction -= getJoy;
        ActionBase.ReviveAction -= Revive;
        ActionBase.winPlayer -= win;
        ActionBase.HpUpgrape -= setHp;
        ActionBase.getMaterialPlayer -= setMaterial;
        ActionBase.getWeaponPlayer -= chooseWeapon;
    }

    private void Update()
    {
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        if (alive)
        {
            if (joy != null && joy.Direction != Vector2.zero)
            {

                if (RigWEAPON != null)
                {
                    RigWEAPON.velocity = joy.Direction * Speed;
                }
                if (!setanim)
                {
                    setRig(setanim);
                    setanim = true;
                    checkAnimfly(setanim);
                }
                balence.transform.position = Hip.transform.position + (new Vector3(joy.Direction.x, joy.Direction.y, 0));


            }
            else
            {
                Vector3 targetAngle = new Vector3(0f, 180, 0f);
                Vector3 currentAngle = transform.eulerAngles;
                currentAngle = new Vector3(
           Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime*50),
           Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime*50),
           Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime*50));
                transform.eulerAngles = currentAngle;
                if (setanim)
                {
                    setRig(setanim);
                    setanim = false;
                    checkAnim(setanim);
                }
                pos = transform.position;
                balence.transform.position = pos + Vector3.up * 2.5f;
            }
        }
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(Hip.transform.position.x, Hip.transform.position.y - 0.9f, Hip.transform.position.z), ref celocity, 0);

    }
    private void FixedUpdate()
    {
        for (int i = 0; i < CopyAnim.Count; i++)
        {
            CopyAnim[i]._Update();
        }
    }

    private void win()
    {
        setWin = true;
    }

    void checkAnim(bool setVal)
    {
        for (int i = 0; i < CopyAnim.Count; i++)
        {
            CopyAnim[i].setFly = setVal;

        }
    }
    void checkAnimfly(bool setVal)
    {
        for (int i = 0; i < CopyAnim.Count; i++)
        {
            CopyAnim[i].setFly = ((i > 4));
        }
    }
    void setRig(bool val)
    {
        for (int i = 0; i < CopyAnim.Count; i++)
        {
            CopyAnim[i].rig.useGravity = val;
        }
    }

    public void getJoy(Joystick val)
    {
        joy = val;
    }
    void animDie()
    {
        for (int i = 0; i < CopyAnim.Count; i++)
        {
            CopyAnim[i].setDie();
        }
    }
    void setAlive()
    {
        for (int i = 0; i < CopyAnim.Count; i++)
        {
            CopyAnim[i].setAlive();
        }
    }
    public void Die(float damp)
    {
        if (!setRevive)
        {
            if (alive && !setWin)
            {
                HP -= infoLvl.ATKbase * damp;
                setUIBlood();
                if (HP <= 0)
                {
                    EffectBloodManager.Instance.showEffectDie(gameObject);
                    ActionBase.LoseUIActon();
                    alive = false;
                    string NameLevel = "Material/Die";
                    Material mar = Resources.Load(NameLevel) as Material;
                    L_Skin[indexMae].material = mar;
                    animDie();
                    Hip.gameObject.layer = 11;
                    gameObject.tag = keysave.tagDie;
                    if (Weapon.GetComponent<WeaponSpider>() != null)
                    {
                        Weapon.GetComponent<WeaponSpider>().stopShot();
                    }

                }
                else
                {

                    EffectBloodManager.Instance.showEffectBlood(gameObject);
                }
            }
        }



    }
    private void Revive()
    {
        StopCoroutine("timeRevive");
        StartCoroutine("timeRevive");
        setHp();
        setUIBlood();
        setAlive();
        alive = true;
        Hip.gameObject.layer = 8;
        gameObject.tag = "Player";
        Hip.transform.localEulerAngles = new Vector3(0, 180, 0);
        Hip.transform.localPosition += Vector3.up * 1;
        balence.gameObject.SetActive(true);
        setMaterial();
        if (Weapon.GetComponent<WeaponSpider>() != null)
        {
            Weapon.GetComponent<WeaponSpider>().startShot();
        }
    }
    IEnumerator timeRevive()
    {
        setRevive = true;
        ; yield return new WaitForSeconds(2);
        setRevive = false;
    }
   


}
