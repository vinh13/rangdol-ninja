using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyManager : MonoBehaviour
{
    private Joystick joy;
    private void OnEnable()
    {
        joy = this.GetComponent<Joystick>();
        ActionBase.getJoyStickAction(joy);
     
    }
   
}
