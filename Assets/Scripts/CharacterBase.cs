using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterBase : MonoBehaviour
{
   
    
    [HideInInspector]
    public bool startGame;
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
    [HideInInspector]
    public bool setT_HP;
    private void OnEnable()
    {
        alive = true;
        setT_HP = true;
        startGame = false;
          pos = Hip.transform.localPosition;
        ActionBase.getChildAction(Hip);
        infoLvl = GameManager.Instance.ifLvl();
        checkAnim();
    }
    protected void setUIBlood()
    {
        float a = HP / HPBase;
        if(a<0.1f && a > 0)
        {
            uiBlood.fillAmount = 0.1f;
        }
        else if (a <= 0)
        {
            a = 0;
            uiBlood.fillAmount = a;
        }
        else
        {
            uiBlood.fillAmount = a;
        }
       
    }
    void checkAnim()
    {
        for (int i = 0; i < CopyAnim.Count; i++)
        {
            CopyAnim[i].target = AimationBase.Instance.L_Skelet[i];
        }
    }
    public IEnumerator timeDelayAttack()
    {
        setT_HP = false;
        yield return new WaitForSeconds(0.3f);
        setT_HP = true;
    }

}
