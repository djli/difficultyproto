using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public AudioSource backgroundMusic;
    public AudioSource deathSound;

    private void Awake()
    {
        LoadHighScore();
    }

    private void Start()
    {
        ResetScore();
        UpdateUI();
        backgroundMusic.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.Save();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    public void AddScore()
    {
        score++;
        SaveHighScore();
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    private void SaveHighScore()
    {
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    private void LoadHighScore()
    {
        PlayerPrefs.GetInt("HighScore", 0);
    }

    private void ResetScore()
    {
        score = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SaveHighScore();
        ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayDeathSound()
    {
        deathSound.Play();
    }
}
