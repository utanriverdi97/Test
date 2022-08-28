using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using utility.singleton;

public class BallManager : Singleton<BallManager>
{
    public int totalBalls;
    public int ballsOut;
    public int ballsIn;
    private List<Ball> ballList=new List<Ball>();
    public Transform ballParent;
    public GameObject ball;

    private void Start()
    {
       
    }

    public void SpawnBalls(int ballCount,Transform tubeTr,Vector3 spawnPos)
    {
        for (int i = 0; i < ballCount; i++)
        {
            var go = Instantiate(ball, tubeTr,false);
            go.transform.position = spawnPos;
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
