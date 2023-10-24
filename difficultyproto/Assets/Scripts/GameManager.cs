using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int score;
    private int highScore = 0;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            score = 0;
            highScore = 0;
        }
        else
        {
            Destroy(gameObject);
            return;  // Exit the Awake method if another instance exists
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            highScore = 0;
        }
    }

    public void AddScore()
    {
        score++;
        if (score > highScore)
        {
            highScore = score;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
    }

    public void RestartGame()
    {
        score = 0;  // Reset the current score, but not the high score
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
