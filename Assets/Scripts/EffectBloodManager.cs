using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBloodManager : MonoBehaviour
{
    public static EffectBloodManager Instance;
    public List<GameObject> effDie;
    public List<GameObject> effBlood;
    int index_Die;
    int index_Blood;
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
}
