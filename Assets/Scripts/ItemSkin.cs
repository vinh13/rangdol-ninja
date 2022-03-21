using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemSkin : MonoBehaviour
{
    public Price price;
    Action<int, Price> a;
    [HideInInspector]
    public int index;
    public GameObject Lock;
    public void check(Action<int, Price> val,int i)
    {
        a = val;
        index = i;
        Lock.SetActive(PlayerPrefs.GetInt(keysave.keySkin + index.ToString(), 0) == 0);
    }
   public void click()
    {
        a.Invoke(index, price);
    }
}
