using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using DG.Tweening;
using UnityEngine;
using utility.singleton;

public class TouchController : Singleton<TouchController>
{
    private float rotationFromX;
    private float rotationFromY;
    [NonSerialized]public Transform objectToRotate;
    public float sensitivity;
    [Range(0f, .95f)] public float smoothness = .9f;
    
    [NonSerialized]public float lerpedRotateAmount;
    [NonSerialized]public bool canControl;
    
    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!canControl)
        {
            return;
        }
        
        var fingers = LeanTouch.Fingers;

        if (fingers.Count>0)
        {
            if (fingers[0].Set)
            {
                if ( fingers[0].ScreenPosition.x > Screen.width/2f)
                {
                    rotationFromY = -(fingers[0].StartScreenPosition.y - fingers[0].LastScreenPosition.y);
                }
                else
                {
                    rotationFromY = fingers[0].StartScreenPosition.y - fingers[0].LastScreenPosition.y;
                }

                if (fingers[0].ScreenPosition.y > Screen.height/2f)
                {
                    rotationFromX = fingers[0].StartScreenPosition.x - fingers[0].LastScreenPosition.x;
                }
                else
                {
                    rotationFromX =   -(fingers[0].StartScreenPosition.x - fingers[0].LastScreenPosition.x);
                }
                
                var rotateAmount = ((rotationFromX+rotationFromY) * Time.deltaTime*sensitivity);
                lerpedRotateAmount = Mathf.Lerp(lerpedRotateAmount, rotateAmount, 1 - smoothness);
                
                fingers[0].StartScreenPosition = fingers[0].LastScreenPosition;
            }
        }
        
        else
        {
            lerpedRotateAmount = Mathf.Lerp(lerpedRotateAmount, 0, 1 - smoothness);
        }

        objectToRotate.localEulerAngles +=  Vector3.forward * lerpedRotateAmount;
    }
}
