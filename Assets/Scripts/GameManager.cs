using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    GameObject TempParent;
    int levelnow;
    GameObject SpawnLevel;
    GameObject OldLevel;
    GameObject NewLevel;
    [SerializeField] DataTable data;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        ActionBase.getChildAction += getChild;
        ActionBase.nextLevelAction += nextLevel;
        ActionBase.replayLevelAction += ReloadLevel;

        
    }
    private void OnDisable()
    {
        ActionBase.getChildAction -= getChild;
        ActionBase.nextLevelAction -= nextLevel;
        ActionBase.replayLevelAction -= ReloadLevel;


    }
    private void Start()
    {
        OnCreateParent();
        int templevel = PlayerPrefs.GetInt(keysave.Level, 0);

        if (templevel < keysave.TotalLevel)
        {
            levelnow = templevel;
        }
        else
        {
            levelnow = (templevel % keysave.TotalLevel) ;
        }
        string NameLevel = "Level/" + (levelnow).ToString();
      
        SpawnLevel = Resources.Load(NameLevel) as GameObject;
        NewLevel = Instantiate(SpawnLevel);
    }
    public int getcoininWin(int level)
    {
        int countdata = data.infoLevels.Count;
        if (level>= countdata)
        {
            level = level % countdata;
        }
        return data.infoLevels[level].COINbase;
    }
    public  InfoLevel ifLvl()
    {
        return data.infoLevels[levelnow];
    }
    void OnCreateParent()
    {
        TempParent = new GameObject();
        TempParent.name = "Parent";
    }
    private void getChild(GameObject val)
    {
        val.transform.parent = TempParent.transform;
    }
    private void ReloadLevel()
    {
        OldLevel = NewLevel;
        OldLevel.SetActive(false);
        Destroy(TempParent);
        Destroy(OldLevel);
        StartCoroutine(loadlevel());
    }
    private void nextLevel()
    {
        OldLevel = NewLevel;
        ActionBase.getLevelAction(0);
      levelnow = PlayerPrefs.GetInt(keysave.Level, 0);
        OldLevel.SetActive(false);
        Destroy(TempParent);
        Destroy(OldLevel);
        StartCoroutine(loadlevel());


    }
    private IEnumerator loadlevel()
    {

        bool temp = true;
        yield return new WaitForSeconds(0.0f);
        for (; temp;)
        {
            if (OldLevel == null && TempParent==null)
            {
                OnCreateParent();
                yield return new WaitForSeconds(0.1f);
                int templevel = PlayerPrefs.GetInt(keysave.Level, 0);
                if (templevel < keysave.TotalLevel)
                {
                    levelnow = templevel;
                }
                else
                {
                    levelnow = (templevel % keysave.TotalLevel) ;
                }
                string NameLevel = "Level/" + (levelnow).ToString();



                SpawnLevel = Resources.Load(NameLevel) as GameObject;
                NewLevel = Instantiate(SpawnLevel);
                temp = false;
                break;
            }
        }
    }
}
