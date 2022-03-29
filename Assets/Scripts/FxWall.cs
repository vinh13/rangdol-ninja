using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxWall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == keysave.wall)
        {
          
            Vector3 globalPositionOfContact = collision.contacts[0].point;
            Vector3 tempForw = (collision.transform.position - transform.position).normalized;
          
            if (tempForw.y >=0.8f)
            {
                tempForw = Vector3.up ;
            }
            else
            {
                tempForw = Vector3.right;
            }
           
            EffectBloodManager.Instance.showEffectWall(globalPositionOfContact, tempForw);


        }
    }
   
}
