using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] _enemyPrefebs;
    [SerializeField] GameObject _cop;
    // [SerializeField] GameObject _tripleShot; 
    [SerializeField] GameObject _rapidShot;
    [SerializeField] GameObject _shield;
    [SerializeField] GameObject _moreWrenches; 
    [SerializeField] GameObject _moreHealth;
    // uncomment and populate when done with the game [SerializeField] GameObject _powerups
    [SerializeField] Transform _enemyContainer;

    bool _isAlive = true;
    bool _isWaveStarted;

    UI _UiManger;
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
            StratWave();
            _UiManger.SartWave();
            _isWaveStarted = true;
        }
    }

    public void StratWave()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnCop());
        StartCoroutine(SpawnPowerUp());
        StartCoroutine(SpawnRarePowerUp());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(2.0f);
        while (_isAlive)
        {
            Vector3 posToSpawn = new Vector3(11f, Random.Range(-4f, 6f), 0);
            GameObject enemy = Instantiate(_enemyPrefebs[Random.Range(0, _enemyPrefebs.Length)], posToSpawn, Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(1, 8));
        }
    }


    IEnumerator SpawnPowerUp()
    {
        while (_isAlive)
        {
            Vector3 posToSpawn = new Vector3(11f, Random.Range(-4f, 6f), 0);
            Instantiate(_moreHealth, transform.position, Quaternion.identity);
            //Instantiate(_powerups, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    IEnumerator SpawnRarePowerUp()
    {
        while (_isAlive)
        {
            Vector3 posToSpawn = new Vector3(11f, Random.Range(-4f, 6f), 0);
            Instantiate(_rapidShot, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5, 20));
        }
    }


    IEnumerator SpawnCop()
    {
        while (_isAlive)
        {
            Vector3 posToSpawn = new Vector3(11f, Random.Range(-4f, 6f), 0);
            Instantiate(_cop, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(1, 20));
        }
    }

    public void PlayerDied()
    {
        _isAlive = false;
    }
}