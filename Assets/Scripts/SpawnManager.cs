using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] _enemyPrefebs;
    [SerializeField] GameObject cop;
   // [SerializeField] GameObject _tripleShot; 
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
            //Instantiate(_tripleShot, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }


    IEnumerator SpawnCop()
    {
        while (_isAlive)
        {
            Vector3 posToSpawn = new Vector3(11f, Random.Range(-4f, 6f), 0);
            Instantiate(cop, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(1, 10));
        }
    }

    public void PlayerDied()
    {
        _isAlive = false;
    }
}