using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using utility.singleton;
using Random = System.Random;

public class BallManager : Singleton<BallManager>
{
    [Header("Properties")]
    [Range(0f, 1f)] public float bounciness=0.3f;
    [Range(0f, 100f)]public float ballWeight=1f;
    [Range(1f, 3f)] public float ballSize=1.3f;
    [Header("Requirements")] 
    public PhysicMaterial ballPM;
    public PhysicMaterial tubePM;
    public GameObject ball;
    public List<Material> ballMaterials;
    
    private int totalBalls;
    private int ballsOut;
    [NonSerialized]public int ballsIn;
    private List<Ball> ballList=new List<Ball>();
    public Transform ballParent;
    

    private void Awake()
    {
        ballPM.bounciness = bounciness;
        ballWeight = ball.GetComponent<Rigidbody>().mass = ballWeight;
    }

    public void SpawnBalls(int ballCount,Transform tubeTr,Vector3 spawnPos)
    {
        for (int i = 0; i < ballCount; i++)
        {
            var go = Instantiate(ball, tubeTr,false);
            go.transform.position = spawnPos;
            go.transform.localScale=Vector3.one*(ballSize/10);
            go.GetComponent<Renderer>().material = ballMaterials[UnityEngine.Random.Range(0,ballMaterials.Count)];
            ballList.Add(go.GetComponent<Ball>());
            totalBalls = ballList.Count;
        }
    }
    
    public void ClearBalls()
    {
        foreach (var obj in ballList)
        {
            Destroy(obj.gameObject);           
        }
        ballList.Clear();
        totalBalls = 0;
        ballsIn = 0;
        ballsOut = 0;
    }

    public void CheckBallOut(Ball ballObj)
    {
        if (ballObj.isOut)
        {
            return;
        }

        ballObj.isOut = true;
        ballsOut += 1;
        
        if (ballsOut>=totalBalls)
        {
            StartCoroutine(CheckForWin());
        }
    }
    
    private IEnumerator CheckForWin()
    {
        yield return new WaitForSeconds(2f);
        
        if (ballsIn>=LevelManager.I.goalForLevel)
        {
            GameManager.I.Win();
        }
        else
        {
            GameManager.I.Lose();
        }
    }
    
    public void CheckBallIn(Ball ballObj)
    {
        if (ballObj.isIn)
        {
            return;
        }
        
        ballObj.isIn = true;
        ballsIn += 1;
        UIManager.I.UpdateGoalText();
    }
}
