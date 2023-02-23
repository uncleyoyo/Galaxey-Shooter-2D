using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    GameManager _gameManager;

    [SerializeField] TMP_Text _gameOver;
    [SerializeField] TMP_Text _restart;
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _startGameText;
    [SerializeField] TMP_Text _sayingText;
    [SerializeField] TMP_Text _wrenchCountText;
    [SerializeField] Image _thusterDisplay;
    [SerializeField] GameObject[] _livesImg;

    void Start()
    {
        _wrenchCountText.text = "x " + 15;
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

    public void UpdateWrenchCount(int wrenchCount)
    {
        _wrenchCountText.text = "x " + wrenchCount;
    }

    public void SartWave()
    {
        StartCoroutine(StartWave());
    }

    public void LoseLives(int currentLives)
    {
       _livesImg[currentLives].gameObject.SetActive(false);
       
        if (currentLives < 1)
        {
            _gameOver.gameObject.SetActive(true);
            _restart.gameObject.SetActive(true);
            _gameManager.GameOver();
            StartCoroutine(GameOverFlicker());
        }
    }

    public void AddLives(int currentLives)
    {
         if (currentLives == 1)
        {
            _livesImg[0].gameObject.SetActive(true);
            Debug.Log(currentLives);
        }
        else if (currentLives == 2)
        {
            _livesImg[1].gameObject.SetActive(true);
            Debug.Log(currentLives);
        }
        else if (currentLives == 3)
        {
            _livesImg[2].gameObject.SetActive(true);
        }
         else if (currentLives > 3)
        {
            currentLives = 3;
            Debug.Log(currentLives);
        }
    }

    public void UpdayteThusterDisplay(float currentAmount)
    {
        _thusterDisplay.fillAmount = currentAmount;
        // create an enemy drop to refill thuster.
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


