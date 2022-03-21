
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "Data", menuName = "DataTable/Level", order = 1)]
[Serializable]
public class InfoLevel
{
    public int HPbase;
    public  int ATKbase;
    public int COINbase;
}
public class DataTable : ScriptableObject
{
    public List<InfoLevel> infoLevels;
}
