using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject gameOverMenuUI;
    [SerializeField] TextMeshProUGUI menuHighScoreText;
    float speed;

    void Start()
    {
        try
        {
            menuHighScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
        catch (System.Exception) {}

        GlobalEventManager.OnSnakeDeath.AddListener(GameOver);
    }

    void GetHighScore()
    {
        GetComponent<TextMeshProUGUI>().SetText(PlayerPrefs.GetInt("HighScore", 0).ToString());
    }

    public void PlayChallenge()
    {
        SceneManager.LoadScene("Challenge");
    }

    public void PlayLevel(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        speed = Time.timeScale;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = speed;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverMenuUI.SetActive(true);
    }
}
