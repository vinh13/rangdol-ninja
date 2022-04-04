using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject cam;
    private GameObject Target;
    Vector3 disPC;
    Vector3 posCam;
    [SerializeField] Joystick joy;
    Vector3 velocity;
    float smoothMoveCam = 0;
    private void Awake()
    {
        smoothMoveCam = 0;
        posCam = cam.transform.localPosition;
    }
    private void OnEnable()
    {
        ActionBase.getTagetCamAction += getTarget;
        ActionBase.moveCam += effectCam;
    }
    private void OnDisable()
    {
        ActionBase.getTagetCamAction -= getTarget;
        ActionBase.moveCam -= effectCam;
    }
    private void Start()
    {
        disPC = new Vector3(0, 0, 3.2f);
      
    }
    private void Update()
    {
        if (Target != null)
        {
            if(joy.Direction.x< smoothMoveCam)
            {
                smoothMoveCam -= Time.deltaTime;
            }
            else
            {
                smoothMoveCam += Time.deltaTime;
            }
            transform.position = Vector3.SmoothDamp(transform.position,  disPC + Target.transform.position, ref velocity, 0.12f);

        }
    }
    private void getTarget(GameObject val)
    {
        Target = val;
    }

   private void effectCam()
    {
        cam.transform.DOKill();
        cam.transform.DOLocalMove(posCam + cam.transform.forward*1.5f, 0.4f).OnComplete(()=> {
            cam.transform.DOLocalMove(posCam, 0.4f);
        });
    }

}
