using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public int level = 1;
    public int score = 0;
    public int lives = 3;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI LivesText;

    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public Brick[] bricks { get; private set; }

    [SerializeField] private AudioSource hitBrickSoundEffect;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(FindObjectOfType<Canvas>());
        SceneManager.sceneLoaded += OnLevelLoaded;
        SceneManager.LoadScene("MainMenu");
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
        this.bricks = FindObjectsOfType<Brick>();
    }

    public void NewGame()
    {
        ResetStats();

        LoadLevel(this.level);
        FindObjectOfType<Canvas>().enabled = true;
    }

    public void LoadLevel(int level)
    {
        FindObjectOfType<Canvas>().enabled = true;
        this.level = level;
        this.levelText.text = $"Level {this.level}";
        SceneManager.LoadScene("Level" + level);
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void ResetLevel()
    {
        this.ball.ResetBall();
        this.paddle.ResetPaddle();
    }

    private void ResetStats()
    {
        this.score = 0;
        this.level = 1;
        this.lives = 3;

        scoreText.text = $"Score: {this.score}";
        levelText.text = $"Level {this.level}";
        LivesText.text = $"Lives: {this.lives}";
    }

    private void GameOver()
    {
        this.level = -1;
        FindObjectOfType<Canvas>().enabled = false;
        SceneManager.LoadScene("GameOver");
        // NewGame();
    }

    private void CompleteLevel()
    {
        this.score += 1000; // Give the player 1000 points for completing the level
        this.scoreText.text = $"Score: {this.score}";
        // Load the next level
        if (level < 3)
            LoadLevel(++level);
        else
        {
            FindObjectOfType<Canvas>().enabled = false;
            SceneManager.LoadScene("GameWin");
        }
    }

    public void Hit(Brick brick)
    {
        hitBrickSoundEffect.Play();
        this.score += brick.points;
        this.scoreText.text = $"Score: {this.score}";
        // Check if the level has been completed
        CheckGameStatus();
    }

    private void CheckGameStatus()
    {
        if (this.level > 0) // Check if the player is currently playing a level
            if (GetBricksRemaining() <= 0)
                CompleteLevel();
    }
    private int GetBricksRemaining()
    {
        int numBricks = 0;
        foreach (Brick brick in bricks)
            if (!brick.unbreakable && brick.health > 0)
                numBricks++;
        return numBricks;
    }

    public void Miss()
    {
        this.lives--;
        this.LivesText.text = $"Lives: {this.lives}";

        if (this.lives > 0)
            ResetLevel();
        else
            GameOver();
    }
}