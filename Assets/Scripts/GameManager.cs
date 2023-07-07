using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool _isGameOver;
    bool _youWin;

    void Update()
    {
        Restart();
        Menu();
        YouWinTheGame();
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

    void YouWinTheGame()
    {
        if (_youWin == true)
        {
            StartCoroutine(BackToMenu());
        }
    }
    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void YouWin()
    {
        _youWin = true;
    }
}

