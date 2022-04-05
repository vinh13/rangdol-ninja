using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum typeCut
{
    None,
    Cut
}
public enum typeAttack {
    None,
    Head,
    Body,
    Hand,
    Boss

}


public class CopyAnim : MonoBehaviour
{
    public typeAttack TypeAttack;
    public typeCut type;
    public Transform target;
    ConfigurableJoint join;
    Quaternion startingRotation;
    [HideInInspector]
    public bool setFly;
    [HideInInspector]
    public bool alive;
    Action<float, typeAttack> a;
    Action b;
    string tag;
    int layer;
    [SerializeField] CopyAnim skelet;
    [SerializeField] GameObject Weapon;
    [HideInInspector]
    public Rigidbody rig;
    public GameObject WEAPON;
    private void Awake()
    {
        rig = this.GetComponent<Rigidbody>();
    }
    void Start()
    {
        join = this.GetComponent<ConfigurableJoint>();
        startingRotation = transform.rotation;
        alive = true;
        if ((int)type == 1)
        {
            gameObject.tag = type.ToString();
        }
        tag = gameObject.tag;
        layer = gameObject.layer;
    }

    public void _Update()
    {
        if (alive)
        {
            if (!setFly)
            {
                join.SetTargetRotationLocal(target.rotation, startingRotation);
            }
            else
            {
                join.SetTargetRotationLocal(startingRotation, startingRotation);
            }
        }
    }
    public void checkSkelet(Action<float,typeAttack> val_0,Action val_1)
    {
        a = val_0;
        b = val_1;
    }
    public void setDie()
    {
        alive = false;
        gameObject.tag = keysave.tagDie;
        gameObject.layer = 11;
        JointDrive drive = new JointDrive();
        drive.mode = JointDriveMode.Position;
        drive.positionSpring = 0;
        drive.positionDamper = 10;
        drive.maximumForce = 3.4f * Mathf.Pow(10, 38);
        join.angularXDrive = drive;
        join.angularYZDrive = drive;
        if ((int)type == 0)
        {
            rig.AddForce(Vector3.up * 100);
        }
    }

    public void setAlive()
    {
        alive = true;
        gameObject.tag = tag;
        gameObject.layer = layer;

        JointDrive drive = new JointDrive();
        drive.mode = JointDriveMode.Position;
        drive.positionSpring = 500;
        drive.positionDamper = 10;
        drive.maximumForce = 3.4f * Mathf.Pow(10, 38);
        join.angularXDrive = drive;
        join.angularYZDrive = drive;

    }
   public void breakSkelet()
    {
        if ((int)type == 1)
        {
            alive = false;
            if (WEAPON != null)
            {
                 WEAPON.GetComponent<Rigidbody>().useGravity = true;
                ConfigurableJoint tempW = WEAPON.GetComponent<ConfigurableJoint>();
                tempW.connectedBody = null;
                tempW.xMotion = ConfigurableJointMotion.Free;
                tempW.yMotion = ConfigurableJointMotion.Free;
                tempW.zMotion = ConfigurableJointMotion.Free;
                tempW.angularXMotion = ConfigurableJointMotion.Free;
                tempW.angularYMotion = ConfigurableJointMotion.Free;
                tempW.angularZMotion = ConfigurableJointMotion.Free;
                /////////////////
                ActionBase.getChildAction(WEAPON);
                b.Invoke();
            }
            ActionBase.getChildAction(this.gameObject);
            join.connectedBody = null;
            join.xMotion = ConfigurableJointMotion.Free;
            join.yMotion = ConfigurableJointMotion.Free;
            join.zMotion = ConfigurableJointMotion.Free;
            join.angularXMotion = ConfigurableJointMotion.Free;
            join.angularYMotion = ConfigurableJointMotion.Free;
            join.angularZMotion = ConfigurableJointMotion.Free;
            gameObject.layer = 11;
            gameObject.GetComponent<Rigidbody>().AddForce(transform.right * 500);

            if (skelet != null)
            {
                skelet.breakSkelet();
            }
            if (Weapon != null)
            {
                Weapon.layer = 11;
            }
          
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (alive)
        {
            if (collision.gameObject.tag ==keysave.tagWeapon)
            {
                a.Invoke(collision.gameObject.GetComponent<WeaponBase>().Damp, TypeAttack); 
                if (collision.gameObject.GetComponent<bullet>() != null)
                {
                    collision.gameObject.SetActive(false);
                }
                breakSkelet();
                if (Weapon != null)
                {
                    Weapon.layer = 11;
                }

            }
            else if (collision.gameObject.tag == keysave.tagSaw)
            {
                
                a.Invoke(collision.gameObject.GetComponent<WeaponBase>().Damp, TypeAttack);
                if (collision.gameObject.GetComponent<bullet>() != null)
                {
                    collision.gameObject.SetActive(false);
                }
                breakSkelet();
                if (Weapon != null)
                {
                    Weapon.layer = 11;
                }
                AudioManager.Instance.playSound(AudioManager.Instance.sawActive);
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (alive)
        {
            if (other.gameObject.tag == keysave.tagBarrel)
            {
                a.Invoke(other.gameObject.GetComponent<WeaponBase>().Damp, TypeAttack);
                if (other.gameObject.GetComponent<bullet>() != null)
                {
                    other.gameObject.SetActive(false);
                }
                breakSkelet();
                if (Weapon != null)
                {
                    Weapon.layer = 11;
                }

            }
        }
    }


}
