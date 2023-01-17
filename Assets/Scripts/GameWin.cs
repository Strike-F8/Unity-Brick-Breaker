using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    public void Start()
    {
        var manager = FindObjectOfType<GameManager>();
        finalScoreText.text = $"Final Score: {manager.score}";
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
