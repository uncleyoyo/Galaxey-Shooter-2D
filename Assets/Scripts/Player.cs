using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    SpawnManager _spawnManager;
    UI _uiManager;
    CameraMover _cameraShake;

    [SerializeField] float _speed = 7;
    [SerializeField] float thusterSpeed = 14;
    [SerializeField] float _ThusterGauge = 1;
    [SerializeField] GameObject _wrench;
    [SerializeField] GameObject _tripleShot;
    [SerializeField] GameObject _speedFlame;
    [SerializeField] GameObject _shieldObj;

    [SerializeField] float _fireRate = 0.5f;
    [SerializeField] float _fireRateIncrese = 0.25f;
    float _canFire = -1;
    [SerializeField] int _lives = 3;
    [SerializeField] int _score = 0;
    [SerializeField] int _shields = 3;
    [SerializeField] int _wrenchCount = 15;

    [SerializeField] float duration = 1f;

    [SerializeField] AudioClip _audioClip;
    AudioSource _audioSource;
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] Sprite[] _spriteList;

    bool _isTripleShotOn = false;
    bool _isShieldsOn = false;
    bool _isRapidShotOn = false;
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI>();
        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraMover>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI manager is NULL");
        }
        if (_cameraShake == null)
        {
            Debug.LogError("Camrea shake is NULL");
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
        _uiManager.UpdateWrenchCount(_wrenchCount);
        Move();

        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire && _wrenchCount > 0)
        {
            Shoot();
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift) && _ThusterGauge > 0)
        {
            _uiManager.UpdayteThusterDisplay(_ThusterGauge);
            _ThusterGauge -= Time.deltaTime;   
            transform.Translate(new Vector2(horizontal, vertical) * thusterSpeed * Time.deltaTime);
            _speedFlame.SetActive(true);
        }
        else
        {
            transform.Translate(new Vector2(horizontal, vertical) * _speed * Time.deltaTime);
            _speedFlame.SetActive(false);
        }

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
        RemoveWreches();
        _canFire = Time.time + _fireRate;
        if (_isTripleShotOn == true)
        {
            Instantiate(_tripleShot, transform.position, Quaternion.identity);
        }
        else if (_isRapidShotOn == true)
        {
            Instantiate(_wrench, transform.position + new Vector3(1.3f, 0, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_wrench, transform.position + new Vector3(1.3f, 0, 0), Quaternion.identity);
        }
        _audioSource.Play();
    }
   
    public void Damage()
    {
        if (_isShieldsOn == true)
        {
            Shields();
            _shields--;

            if (_shields < 1)
            {
                _isShieldsOn = false;
                _shieldObj.SetActive(false);
                _shields = 3;
            }
            return;
        }

        _lives--;

        _uiManager.LoseLives(_lives);
        if (_lives < 1)
        {
            _spawnManager.PlayerDied();
            Destroy(gameObject);
        }
        StartCoroutine(_cameraShake.Mover(duration));
    }
    public void RetoreLife()
    {
        _uiManager.AddLives(_lives);
    }
        
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
    public void RemoveWreches()
    {
        _wrenchCount -= 1;
    }

    public void AddWrenches()
    {
        _wrenchCount += 5;
    }

    public void AddHealth()
    {
        _lives++;
        _uiManager.AddLives(_lives);
        if (_lives > 4)
        {
            _lives = 3;
        }
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

    public void RapidShot()
    {
        _isRapidShotOn = true;
        _fireRate = _fireRateIncrese;
        StartCoroutine(RapidShotPower());
    }

    IEnumerator RapidShotPower()
    {
        yield return new WaitForSeconds(5f);
        _isRapidShotOn = false;
        _fireRate = 0.5f;

    }
    public void Shields()
    {
        _isShieldsOn = true;
        _shieldObj.SetActive(true);

        if (_shields == 3)
        {
            _sprite.sprite = _spriteList[2];
        }
        else if (_shields == 2)
        {
            _sprite.sprite = _spriteList[1];
        }
        else
        {
            _sprite.sprite = _spriteList[0];
        }
    }
}