using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimationBase : MonoBehaviour
{
    public static AimationBase Instance;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    public List<Transform> L_Skelet;
}
