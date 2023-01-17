using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    public void LoadLevel(int level)
    {
        GameManager manager = FindObjectOfType<GameManager>();
        manager.LoadLevel(level);
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
