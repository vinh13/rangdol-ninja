using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class InfoUpdate
{
    public int HPbase;
    public int ATKbase;
    public int Price;
}
[CreateAssetMenu(fileName = "DataUpdate", menuName = "DataTable_Update/update", order = 1)]
public class DataUpdate : ScriptableObject
{
    public List<InfoUpdate> infoLevels;
}
