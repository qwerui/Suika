using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int nowScore;
    public int NowScore
    {
        get{return nowScore;}
    }
    int[] topThree;
    public int[] TopThree
    {
        get{return topThree;}
    }

    [Header("UI")]
    public Text nowScoreText;
    public Text resultScoreText;
    public List<Text> topThreeText;

    private void Awake() 
    {
        topThree = new int[4];
        var container = JSONParser.ReadJSON<ScoreContainer>($"{Application.persistentDataPath}/Score.json");
        if(container != null)
        {
            topThree = container.scores;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetManager().OnGameStart += () => {
            nowScore = 0;
            nowScoreText.text = $"SCORE : {NowScore}";
        };
        GameManager.GetManager().OnFruitCombined += (newScore)=>{
            nowScore += newScore;
            nowScoreText.text = $"SCORE : {NowScore}";
        };
        GameManager.GetManager().OnGameEnd += CheckRank;
        GameManager.GetManager().OnGameEnd += () => resultScoreText.text = nowScore.ToString();
    }

    void CheckRank()
    {
        topThree[3] = nowScore;

        Array.Sort(topThree,(a, b)=>b-a);

        for(int i=0;i<3;i++)
        {
            topThreeText[i].text = topThree[i].ToString();
        }

        JSONParser.SaveJSON<ScoreContainer>($"{Application.persistentDataPath}/Score.json", new ScoreContainer(topThree));
    }

}
