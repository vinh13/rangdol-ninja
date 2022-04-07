using System.Collections;
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
    W_7,
    W_8,
    W_9,
    W_10,
    W_11,

}
public enum typeSwordHand
{
   Left,
   Right,
   All

}

public class GetWeaponHand : MonoBehaviour
{
    [SerializeField] typeWeaponEne typeWeapon;
    [SerializeField] typeSwordHand typeHand;
    int indexWeapon;
    [SerializeField] List<GameObject> L_Hand;
    public static Action A;
    [SerializeField] bool setBoss;
   
    private void Start()
    {
        Invoke("chooseWeapon", 1);
    }
    public void chooseWeapon()
    {
        indexWeapon = (int)typeWeapon;
        string NameSkin = "WeaponEne/" + indexWeapon;
        GameObject tempwea = Resources.Load(NameSkin) as GameObject;
        if ((int)typeHand!=2){
            GameObject wea = Instantiate(tempwea);
            if (setBoss)
            {
                wea.transform.localScale = Vector3.one * 2;
            }
            wea.transform.parent = L_Hand[(int)typeHand].transform;
            wea.transform.localEulerAngles = Vector3.zero;
            wea.transform.localPosition = Vector3.zero;
            wea.GetComponent<connectBody>().Join.connectedBody = L_Hand[(int)typeHand].GetComponent<Rigidbody>();

        }
        else
        {
            GameObject wea = Instantiate(tempwea);
            if (setBoss)
            {
                wea.transform.localScale = Vector3.one * 2;
            }
            wea.transform.parent = L_Hand[0].transform;
            wea.transform.localEulerAngles = Vector3.zero;
            wea.transform.localPosition = Vector3.zero;
            wea.GetComponent<connectBody>().Join.connectedBody = L_Hand[0].GetComponent<Rigidbody>();
            GameObject wea1 = Instantiate(tempwea);
            if (setBoss)
            {
                wea1.transform.localScale = Vector3.one * 2;
            }
            wea1.transform.parent = L_Hand[1].transform;
            wea1.transform.localEulerAngles = Vector3.zero;
            wea1.transform.localPosition = Vector3.zero;
            wea1.GetComponent<connectBody>().Join.connectedBody = L_Hand[1].GetComponent<Rigidbody>();
        }
       
       
    }
}
