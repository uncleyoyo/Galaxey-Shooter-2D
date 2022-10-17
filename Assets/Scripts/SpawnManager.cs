using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefeb;
    [SerializeField] Transform _enemyContainer;

    bool _isAlive= true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemies()
    {
        while (_isAlive)
        {
            Vector3 posToSpawn = new Vector3(11f, Random.Range(-4f, 6f), 0);
            GameObject enemy = Instantiate(_enemyPrefeb, posToSpawn, Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5f);
        }
    }

    public void PlayerDied()
    {
        _isAlive = false;
    }

}
