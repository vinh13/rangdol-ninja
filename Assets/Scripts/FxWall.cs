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
            Vector3 tempForw = (globalPositionOfContact - transform.position).normalized;

            EffectBloodManager.Instance.showEffectWall(globalPositionOfContact, tempForw);


        }
    }
   
}
