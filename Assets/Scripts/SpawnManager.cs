using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    UI _UiManger;

    [SerializeField] int _enemyCount;
    [SerializeField] GameObject[] _enemyPrefebs;
    [SerializeField] GameObject[] _powerups;
    [SerializeField] GameObject[] _food;
    [SerializeField] GameObject _rapidShot;
    [SerializeField] GameObject _slowDown;
    [SerializeField] GameObject _cop;
    [SerializeField] GameObject _boss;
    [SerializeField] Transform _enemyContainer;
    [SerializeField] int _wavecount;

    bool _isAlive = true;
    bool _isWaveStarted;

    void Start()
    {
        _UiManger = GameObject.Find("Canvas").GetComponent<UI>();

        if (_UiManger == null)
        {
            Debug.LogError("The UIManager is NULL");
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return) && _isWaveStarted == false)
        {
            StartWave();
            _UiManger.StartWave();
            _isWaveStarted = true;
        }
    }

    public void StartWave()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnCop());
        StartCoroutine(SpawnPowerUp());
        StartCoroutine(SpawnRarePowerUp());
        StartCoroutine(SpawnNegitivePowerUp());
        StartCoroutine(SpawnFood());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(2.0f);

        while (_isAlive)
        {
            for (int i = 0; i < _enemyCount; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(Random.Range(1, 8));
            }

            _enemyCount++;
            _wavecount++;
            _UiManger.ChangeWaveNumber();
            if (_wavecount > 10)
            {
                SpawnBoss();
            }
            yield return StartCoroutine(WaitUntilWaveDone());
        }
    }

    IEnumerator WaitUntilWaveDone()
    {
        while (_enemyContainer.transform.childCount > 0)
        {
            yield return null;
        }
    }
    void SpawnEnemy()
    {
        Vector2 posToSpawn = new Vector3(11f, Random.Range(-4f, 6f));
        GameObject enemy = Instantiate(_enemyPrefebs[Random.Range(0, _enemyPrefebs.Length)], posToSpawn, Quaternion.identity);
        enemy.transform.parent = _enemyContainer.transform;
    }

    void SpawnBoss()
    {
        StopAllCoroutines();
        StartCoroutine(SpawnPowerUp());
        StartCoroutine(SpawnRarePowerUp());
        StartCoroutine(SpawnFood());

        Instantiate(_boss, transform.position, Quaternion.identity);
    }

    IEnumerator SpawnPowerUp()
    {
        while (_isAlive)
        {
            Vector2 posToSpawn = new Vector3(11f, Random.Range(-4f, 6f));
            Instantiate(_powerups[Random.Range(0, _powerups.Length)], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    } 
    
    IEnumerator SpawnFood()
    {
        while (_isAlive)
        {
            Vector2 posToSpawn = new Vector3(11f, Random.Range(-4f, 6f));
            Instantiate(_powerups[Random.Range(0, _food.Length)], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5, 8));
        }
    }

    IEnumerator SpawnRarePowerUp()
    {
        while (_isAlive)
        {
            Vector2 posToSpawn = new Vector3(11f, Random.Range(-4f, 6f));
            Instantiate(_rapidShot, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5, 20));
        }
    }
    IEnumerator SpawnNegitivePowerUp()
    {
        while (_isAlive)
        {
            Vector2 posToSpawn = new Vector3(11f, Random.Range(-4f, 6f));
            Instantiate(_slowDown, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5, 20));
        }
    }

    IEnumerator SpawnCop()
    {
        while (_isAlive)
        {
            Vector2 posToSpawn = new Vector3(11f, Random.Range(-4f, 6f));
            Instantiate(_cop, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(1, 20));
        }
    }

    public void PlayerDied()
    {
        _isAlive = false;
    }
}