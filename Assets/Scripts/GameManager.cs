using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager GetManager() => instance;
    
    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Action OnFruitDropped;
    public Action<int> OnFruitCombined;
    public Action OnGameStart;
    public Action OnGameEnd;
    public Action OnRequsetNewGame;
    public Action OnPause;
    public Action OnResume;
}
