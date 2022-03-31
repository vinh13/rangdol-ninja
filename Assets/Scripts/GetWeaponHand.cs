﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public enum typeWeaponEne { 
     W_0,
    W_1,
    W_2,
    W_3,
    W_4,
    W_5,
    W_6,

}

public class GetWeaponHand : MonoBehaviour
{
    [SerializeField] typeWeaponEne typeWeapon;
    [SerializeField] typeSword typeHand;
    [SerializeField] int indexWeapon;
    [SerializeField] List<GameObject> L_Hand;
    public static Action A;
   
   
    private void Start()
    {
        Invoke("chooseWeapon", 1);
    }
    public void chooseWeapon()
    {
        indexWeapon = (int)typeWeapon;
        string NameSkin = "WeaponEne/" + indexWeapon;
        GameObject tempwea = Resources.Load(NameSkin) as GameObject;
        GameObject wea = Instantiate(tempwea);
        wea.transform.parent = L_Hand[(int)typeHand].transform;
        wea.transform.localEulerAngles = Vector3.zero;
        wea.transform.localPosition = Vector3.zero;
        wea.GetComponent<connectBody>().Join.connectedBody = L_Hand[(int)typeHand].GetComponent<Rigidbody>();
    }
}