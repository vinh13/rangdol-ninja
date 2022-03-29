using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBloodManager : MonoBehaviour
{
    public static EffectBloodManager Instance;
    public List<GameObject> effDie;
    public List<GameObject> effBlood;
    public List<GameObject> effWall;
    int index_Die;
    int index_Blood;
    int index_Wall;
    private void Awake()
    {
        index_Die = 0;
        index_Blood = 0;
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
    public void showEffectDie(GameObject par)
    {

        if (index_Die >= effDie.Count)
        {
            index_Die = 0;
        }
        effDie[index_Die].transform.position = par.transform.position + Vector3.up  ;
        effDie[index_Die].gameObject.SetActive(true);


        index_Die++;
        if (index_Die >= effDie.Count)
        {
            index_Die = 0;
        }
    }
    public void showEffectBlood(GameObject par)
    {
        if (index_Blood >= effBlood.Count)
        {
            index_Blood = 0;
        }
        effBlood[index_Blood].transform.position = par.transform.position + Vector3.up;
        effBlood[index_Blood].gameObject.SetActive(true);


        index_Blood++;
        if (index_Blood >= effBlood.Count)
        {
            index_Blood = 0;
        }
    }
    public void showEffectWall(Vector3 pos, Vector3 forw)
    {
        if (index_Wall >= effWall.Count)
        {
            index_Wall = 0;
        }
        effWall[index_Wall].transform.position = pos;
        effWall[index_Wall].transform.forward= forw;
        effWall[index_Wall].gameObject.SetActive(true);


        index_Wall++;
        if (index_Wall >= effWall.Count)
        {
            index_Wall = 0;
        }
    }
}
