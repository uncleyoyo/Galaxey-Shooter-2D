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
    [SerializeField] TMP_Text _waveText;
    [SerializeField] TMP_Text _sayingText;
    [SerializeField] TMP_Text _wrenchCountText;
    [SerializeField] TMP_Text _eneimesRemaing;
    [SerializeField] Image _thusterDisplay;
    [SerializeField] GameObject[] _livesImg;

    void Start()
    {
        _wrenchCountText.text = "x " + 15 + "/" + "20";
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
        _wrenchCountText.text = "x " + wrenchCount + "/" + "20";
    }

    public void StartWave()
    {
        StartCoroutine(StartWaveCO());
    }

    public void ChangeWaveNumber()
    {
        StartCoroutine(ChangeWaveNumberCo());
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
        }
        else if (currentLives == 2)
        {
            _livesImg[1].gameObject.SetActive(true);
        }
        else if (currentLives == 3)
        {
            _livesImg[2].gameObject.SetActive(true);
        }
        else if (currentLives > 3)
        {
            currentLives = 3;
        }
    }

    public void UpdayteThusterDisplay(float currentAmount)
    {
        _thusterDisplay.fillAmount = currentAmount;
    }

    public void YouWinTheGamer()
    {
        StartCoroutine(YouWin());
    }
    IEnumerator YouWin()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _gameOver.gameObject.SetActive(true);
            _gameOver.text = "You Win!!";
            yield return new WaitForSeconds(0.5f);
            _gameOver.text = "";
            yield return new WaitForSeconds(0.5f);
            _gameOver.text = "You Win!!";
            _gameOver.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
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

    IEnumerator StartWaveCO()
    {
        _sayingText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        _sayingText.gameObject.SetActive(false);
    }
    IEnumerator ChangeWaveNumberCo()
    {
        int currrentwave = int.Parse(_waveText.text.Substring(5));
        currrentwave++;
        _waveText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _waveText.gameObject.SetActive(false);
        _waveText.text = "Wave: " + currrentwave.ToString();
        yield return new WaitForSeconds(0.5f);
        _waveText.gameObject.SetActive(true);

        if (currrentwave == 10)
        {
            _waveText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _waveText.gameObject.SetActive(false);
            _waveText.text = "Boss Time";
            yield return new WaitForSeconds(0.5f);
            _waveText.gameObject.SetActive(true);
        }
    }
}