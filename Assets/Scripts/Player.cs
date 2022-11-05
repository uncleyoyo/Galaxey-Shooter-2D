using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    UI _uiManager;
    [SerializeField] float _speed = 7;
    [SerializeField] GameObject _wrench;
    [SerializeField] GameObject _tripleShot;

    [SerializeField] float _fireRate = 0.5f;
    float _canFire = -1;
    [SerializeField] int _lives = 3;
    [SerializeField] int _score = 0;
    SpawnManager _spawnManager;
    [SerializeField] bool _isTripleShotOn = false;

    AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;

    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI manager is NULL");
        }
        if (_audioSource == null)
        {
            Debug.LogError("Player's Audio Souce is NULL");
        }
        else
        {
            _audioSource.clip = _audioClip;
        }
    }

    void Update()
    {
        Move();
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
            Shoot();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector2(horizontal, vertical) * _speed * Time.deltaTime);

        if (transform.position.y > 8)
        {
            transform.position = new Vector3(transform.position.x, -6, 0);
        }
        else if (transform.position.y < -6)
        {
            transform.position = new Vector3(transform.position.x, 8, 0);
        }

        if (transform.position.x >= -3)
        {
            transform.position = new Vector3(-3, transform.position.y, 0);
        }
        else if (transform.position.x <= -9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
    }

    void Shoot()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotOn == true)
        {
            Instantiate(_tripleShot, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_wrench, transform.position + new Vector3(1.3f, 0, 0), Quaternion.identity);
        }
        _audioSource.Play();
    }


    public void Damage()
    {
        _lives--;

        _uiManager.UpdateLives(_lives);
        if (_lives < 1)
        {
            _spawnManager.PlayerDied();
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
    public void TriplaShot()
    {
        _isTripleShotOn = true;
        StartCoroutine(TripleShotPower());
    }

    IEnumerator TripleShotPower()
    {
        yield return new WaitForSeconds(5f);
        _isTripleShotOn = false;
    }
}