using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utility.singleton;

public class GameManager : Singleton<GameManager>
{
    public void Win()
    {
        UIManager.I.Win();
        TouchController.I.canControl = false;
    }
    
    public void Lose()
    {
        UIManager.I.Lose();
        TouchController.I.canControl = false;
    }

    public void StartNewLevel()
    {
        UIManager.I.InGame();
        TouchController.I.canControl = true;
    }
}
