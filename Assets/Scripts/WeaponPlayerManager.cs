using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayerManager : WeaponBase
{
    Rigidbody Rig;
    ConfigurableJoint Join;
    private void Awake()
    {
       Rig= this.GetComponent<Rigidbody>();
        Join = this.GetComponent<ConfigurableJoint>();
    }
    private void Start()
    {
        Join.connectedBody = PlayerController.Instance.LeftArmRig;
        PlayerController.Instance.RigWEAPON = Rig;
        setAtk();
    }
    private void OnEnable()
    {
        ActionBase.AtkUpgrape += setAtk;
    }
    private void OnDisable()
    {
        ActionBase.AtkUpgrape -= setAtk;
    }
    private void setAtk()
    {
        int levelAtk = PlayerPrefs.GetInt(keysave.updateWeapon, -1);
        float atkUpgrape = 0;
        if (levelAtk >= 0)
        {
            atkUpgrape = CanvasManager.Instance.dataUpgrape.infoLevels[levelAtk].ATKbase;
        }

        Damp = Damp + atkUpgrape;
       
    }
}
