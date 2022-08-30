using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutTrigger : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag("Ball"))
      {
         other.transform.parent = BallManager.I.ballParent;
         BallManager.I.CheckBallOut(other.GetComponent<Ball>());
      }
   }
}
