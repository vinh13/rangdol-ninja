using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    GameObject TempParent;
    int levelnow;
    GameObject SpawnLevel;
    GameObject OldLevel;
    GameObject NewLevel;
    [SerializeField] DataTable data;
    public List<GameObject> L_Enemy;
    public List<RectTransform> L_Rada;
    [SerializeField]  RectTransform CanvasRect;
    [SerializeField]
    Camera CAM;
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
        ActionBase.GetEnemy += getEnrmy;
        ActionBase.removeEnemy += removeEnrmy;

        
    }
    private void OnDisable()
    {
        ActionBase.getChildAction -= getChild;
        ActionBase.nextLevelAction -= nextLevel;
        ActionBase.replayLevelAction -= ReloadLevel;
        ActionBase.removeEnemy -= removeEnrmy;


    }
    void getEnrmy(GameObject val)
    {
        L_Enemy.Add(val);
    }
    void removeEnrmy(GameObject val)
    {
        L_Rada[L_Enemy.Count-1].gameObject.SetActive(false);
        L_Enemy.Remove(val);
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
    private void Update()
    {
       for(int i=0;i< L_Enemy.Count; i++)
        {
            rada(L_Enemy[i].transform, i);
        }
    }
    void rada( Transform val,int a)
    {
        Vector3 screenPoint = CAM.WorldToViewportPoint(val.transform.position);
       bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        if (!onScreen)
        {
            L_Rada[a].gameObject.SetActive(true);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((screenPoint.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.45f)),
            ((screenPoint.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.45f)));
            Vector2 tempWorldObject_ScreenPosition = WorldObject_ScreenPosition;

            if (tempWorldObject_ScreenPosition.y >= (CanvasRect.sizeDelta.y * 0.3f))
            {

                tempWorldObject_ScreenPosition.y = CanvasRect.sizeDelta.y * 0.3f;
            }
            else if (tempWorldObject_ScreenPosition.y <= -(CanvasRect.sizeDelta.y * 0.45f))
            {

                tempWorldObject_ScreenPosition.y = -(CanvasRect.sizeDelta.y * 0.45f);
            }

            if (tempWorldObject_ScreenPosition.x >= (CanvasRect.sizeDelta.x * 0.45f) )
            {
                if (val.transform.position.x > CAM.transform.position.x)
                {
                    tempWorldObject_ScreenPosition.x = CanvasRect.sizeDelta.x * 0.45f;
                    Vector2 tempAr = (tempWorldObject_ScreenPosition - WorldObject_ScreenPosition).normalized;

                    L_Rada[a].up = tempAr;
                }
                else
                {
                    tempWorldObject_ScreenPosition.x = -(CanvasRect.sizeDelta.x * 0.45f);
                    Vector2 tempAr = (tempWorldObject_ScreenPosition - WorldObject_ScreenPosition).normalized;

                    L_Rada[a].up = -tempAr;
                }
               
            }
            else if (tempWorldObject_ScreenPosition.x <= -(CanvasRect.sizeDelta.x * 0.45f))
            {

                if (val.transform.position.x < CAM.transform.position.x)
                {
                    tempWorldObject_ScreenPosition.x = -(CanvasRect.sizeDelta.x * 0.45f);
                    Vector2 tempAr = (tempWorldObject_ScreenPosition - WorldObject_ScreenPosition).normalized;

                    L_Rada[a].up = tempAr;

                }
                else
                {
                    tempWorldObject_ScreenPosition.x = CanvasRect.sizeDelta.x * 0.45f;
                    Vector2 tempAr = (tempWorldObject_ScreenPosition - WorldObject_ScreenPosition).normalized;

                    L_Rada[a].up = -tempAr;
                }
            } 


            L_Rada[a].anchoredPosition = tempWorldObject_ScreenPosition;
           
        }
        else
        {
           
            L_Rada[a].gameObject.SetActive(false);
        }
      

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
        L_Enemy.Clear();
       
        OldLevel = NewLevel;
        OldLevel.SetActive(false);
        Destroy(TempParent);
        Destroy(OldLevel);
        StartCoroutine(loadlevel());
        for (int i = 0; i < L_Rada.Count; i++)
        {
            L_Rada[i].gameObject.SetActive(false);
        }
    }
    private void nextLevel()
    {
        L_Enemy.Clear();
        OldLevel = NewLevel;
        ActionBase.getLevelAction(0);
      levelnow = PlayerPrefs.GetInt(keysave.Level, 0);
        OldLevel.SetActive(false);
        Destroy(TempParent);
        Destroy(OldLevel);
        StartCoroutine(loadlevel());
        for (int i = 0; i < L_Rada.Count; i++)
        {
            L_Rada[i].gameObject.SetActive(false);
        }


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
