using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            BallManager.I.CheckBallIn(other.gameObject.GetComponent<Ball>());
        }
    }
}
