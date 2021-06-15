using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int scoreValue = 0;

    public static ScoreManager instance;

    public int score, highScore;

    public Text scoreText, highScoreText, gameOverScoreText;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHighScore();

        scoreText.text = score.ToString();
        gameOverScoreText.text = score.ToString();

        scoreText.text = "Score:" + scoreValue;
        gameOverScoreText.text = "Your Score:" + scoreValue;
    }

    public void AddScore()
    {
        score += 10;

        UpdateHighScore();

        scoreText.text = score.ToString();
        gameOverScoreText.text = score.ToString();

        scoreText.text = "Score:" + scoreValue;
        gameOverScoreText.text = "Your Score:" + scoreValue;
    }

    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = scoreValue;

            highScoreText.text = highScore.ToString();

            highScoreText.text = "HighScore:" + scoreValue;

            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void ResetScore()
    {
        scoreValue = 0;
        score = 0;
        scoreText.text = "Score:" + scoreValue;
        gameOverScoreText.text = "Your Score:" + scoreValue;

        scoreText.text = score.ToString();
        gameOverScoreText.text = score.ToString();
    }

    public void ClearHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");

        highScore = 0;
        highScoreText.text = highScore.ToString();

        highScoreText.text = "HighScore:" + scoreValue;
    }

}
