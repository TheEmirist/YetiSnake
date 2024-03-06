using System;
using System.Collections;
using System.Collections.Generic;
using Dan.Main;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    int score = 0;
    string publicLeaderboardKey = "d5552d4773abaec302e20eaf0089332c96f55d91e55315362a30639301ddb545";
    TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI gameOverScoreText;
    [SerializeField] TextMeshProUGUI gameOverHighScoreText;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        if (highScoreText != null) highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        GlobalEventManager.OnFoodEaten.AddListener(FoodEaten);
        GlobalEventManager.OnSnakeDeath.AddListener(ChangeHighScore);
        GlobalEventManager.OnSnakeDeath.AddListener(ChangeGameOverTable);
    }

    void FoodEaten()
    {
        score++;
        scoreText.text = score.ToString();
    }

    void ChangeHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            string username = PlayerPrefs.GetString("userName", "anonymous");
            LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score);
        }

        
    }

    void ChangeGameOverTable()
    {
        gameOverScoreText.text = score.ToString();
        gameOverHighScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
