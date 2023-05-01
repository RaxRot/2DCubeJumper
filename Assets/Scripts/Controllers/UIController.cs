using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    
    public Slider jumpPowerSlider;
    public static UIController Instance;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text fruitText;

    [SerializeField] private GameObject goPanel;
    [SerializeField] private TMP_Text goScoreText;
    [SerializeField] private TMP_Text goHighScoreText;
    

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        goPanel.SetActive(false);
    }

    public void SetScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void SetFruits(int fruits)
    {
        fruitText.text = "Fruits:" + fruits;
    }

    public void ShowGOPanel()
    {
        goPanel.SetActive(true);
    }

    public void SetFinalScore(int score)
    {
        goScoreText.text = "Score:" + score;
    }

    public void SetFinalHighScoreText(int highScore)
    {
        goHighScoreText.text = "HighScore: " + highScore;
    }

    public void GameOver()
    {
        scoreText.enabled = false;
        fruitText.enabled = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToShop()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
