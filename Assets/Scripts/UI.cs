using System.Collections;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    GameManager _gameManager;
    [SerializeField] TMP_Text _gameOver;
    [SerializeField] TMP_Text _restart;
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _startGameText;
    [SerializeField] TMP_Text _sayingText;
    [SerializeField] GameObject[] _livesImg;
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("gameManager is NULL");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void ZeroScore()
    {
        _scoreText.text = "Score: " + 0;
    }

    public void SartWave()
    {
        StartCoroutine(StartWave());
    }

    public void UpdateLives(int currentLives)
    {
        Destroy(_livesImg[currentLives].gameObject);
        if (currentLives < 1)
        {
            _gameOver.gameObject.SetActive(true);
            _restart.gameObject.SetActive(true);
            _gameManager.GameOver();
            StartCoroutine(GameOverFlicker());
        }
    }

    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameOver.text = "GAME OVER!!";
            _restart.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOver.text = "";
            _restart.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _gameOver.text = "GAME OVER!!";
            _restart.gameObject.SetActive(true);
            _gameOver.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
            _restart.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
        }
    }
    IEnumerator StartWave()
    {
        _startGameText.gameObject.SetActive(false);
        _sayingText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        _sayingText.gameObject.SetActive(false);
    }
}


