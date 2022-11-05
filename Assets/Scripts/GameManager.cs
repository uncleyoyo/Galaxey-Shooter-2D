using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool _isGameOver;
    void Update()
    {
        Restart();
        Menu();
    }

    void Menu()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void Restart()
    {
        if (Input.GetKey(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}

