using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using utility.singleton;

public class UIManager : Singleton<UIManager>
{
    public GameObject winUI;
    public GameObject loseUI;
    public GameObject inGameUI;

    public TextMeshProUGUI goalText;

    [NonSerialized]public int goal;
    // Start is called before the first frame update
    void Start()
    {
        inGameUI.SetActive(true);
    }

    public void Win()
    {
        inGameUI.SetActive(false);
        winUI.SetActive(true);
    }
    
    public void Lose()
    {
        inGameUI.SetActive(false);
        loseUI.SetActive(true);
    }

    public void InGame()
    {
        winUI.SetActive(false);
        loseUI.SetActive(false);
        inGameUI.SetActive(true);
    }

    public void UpdateGoalText()
    {
        goalText.text = BallManager.I.ballsIn + "/" + goal;
    }
}
