using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject readyPannel;
    public Text scoreText;
    public Text bestScoreText;
    public Text messageText;

    public bool isRoundActivte = false;

    public int score = 0;

    public ShooterRotator shooterRotator;
    public CamFollow cam;

    void Awake() {
        instance = this;
        UpdateUI();
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateBestScore();
        UpdateUI();
    }

    void UpdateBestScore()
    {
        if(GetBestScore() < score)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
    }

    int GetBestScore()
    {
        int BestScore = PlayerPrefs.GetInt("BestScore");
        return BestScore;
    }

    void UpdateUI()
    {
        scoreText.text = "Score" + score;
        bestScoreText.text = "Best Score: " + GetBestScore();
    }

    public void OnBallDestroy()
    {
        UpdateUI();
        isRoundActivte = false;
    }

    public void Reset()
    {
        score = 0;
        UpdateUI();

        // 라운드를 다시 처음부터 시작
    }
}
