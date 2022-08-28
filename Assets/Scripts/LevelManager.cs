using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using utility.singleton;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels=new List<Level>();
    private Vector3 LevelSpawnPos = new Vector3(0, 3f, 0);
    [NonSerialized]public int LevelCount=1;
    private GameObject currentLevelprefab;
    [NonSerialized]public int goalForLevel;

    private void Start()
    {
        SpawnLevel(LevelCount);
    }

    public void LoadNextLevel()
    {
        BallManager.I.ClearBalls();
        Destroy(currentLevelprefab);
        LevelCount += 1;
        
        if (LevelCount>levels.Count)
        {
            LevelCount = 1;
            SpawnLevel(LevelCount);
        }
        else
        {
            SpawnLevel(LevelCount);
        }
    }

    public void ReloadLevel()
    {
        BallManager.I.ClearBalls();
        Destroy(currentLevelprefab);
        SpawnLevel(LevelCount);
    }
    
    private void SpawnLevel(int levelCount)
    {
        currentLevelprefab = Instantiate(levels[levelCount-1].levelPrefab, LevelSpawnPos, Quaternion.identity);
        var ballSpawnPos = currentLevelprefab.transform.GetChild(0).position;
        goalForLevel = levels[levelCount - 1].cupTreshold;
        UIManager.I.goal = goalForLevel;
        UIManager.I.UpdateGoalText();
        BallManager.I.SpawnBalls(levels[levelCount-1].ballCountToSpawn,currentLevelprefab.transform,ballSpawnPos);
        TouchController.I.objectToRotate = currentLevelprefab.transform;
        GameManager.I.StartNewLevel();
    }
    
    
    [Serializable]
    public class Level
    {
        public int ballCountToSpawn;
        public int cupTreshold;
        public GameObject levelPrefab;
    }
}
