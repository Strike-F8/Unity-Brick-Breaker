using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Awake()
    {
        FindObjectOfType<Canvas>().enabled = false;
    }

    public void Play()
    {
        FindObjectOfType<GameManager>().NewGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
