using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    int CountEnemy;
    private void Awake()
    {
        CountEnemy = 0;
        ActionBase.getCountInGamePlay += Getenemy;
        ActionBase.checkLevelAction += checkLevel;
    }
    private void OnDisable()
    {
        ActionBase.getCountInGamePlay -= Getenemy;
        ActionBase.checkLevelAction -= checkLevel;
    }
    private void Getenemy()
    {
        ++CountEnemy;
    }
    private void checkLevel()
    {
        --CountEnemy;
        if (CountEnemy <= 0)
        {
            ActionBase.WinUIActon();
            ActionBase.winPlayer();
        }
    }
}
