using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterBase : MonoBehaviour
{
   
    
    [HideInInspector]
    public bool alive;
    public GameObject Hip;
    public GameObject balence;
    public List<CopyAnim> CopyAnim;
    [HideInInspector]
    protected Vector3 pos;
    [HideInInspector]
    protected float HP;
    [HideInInspector]
    protected float HPBase;
    [HideInInspector]
    protected InfoLevel infoLvl;
    [SerializeField] Image uiBlood;

    private void OnEnable()
    {
        alive = true;
      
        pos = Hip.transform.localPosition;
        ActionBase.getChildAction(Hip);
        infoLvl = GameManager.Instance.ifLvl();
        checkAnim();
    }
    protected void setUIBlood()
    {
        float a = HP / HPBase;
        if (a < 0)
        {
            a = 0;
        }
        uiBlood.fillAmount = a;
    }
    void checkAnim()
    {
        for (int i = 0; i < CopyAnim.Count; i++)
        {
            CopyAnim[i].target = AimationBase.Instance.L_Skelet[i];
        }
    }

}
