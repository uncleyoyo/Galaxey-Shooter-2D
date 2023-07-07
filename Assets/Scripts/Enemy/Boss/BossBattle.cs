using System.Collections;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    Player _player;
    GameManager _gameManager;
    UI _uiManager;

    [SerializeField] int _score = 100;
    [SerializeField] int _maxHealth = 20;
    [SerializeField] float _speed = 3f;
    [SerializeField] float _attackRange = 3f;
    [SerializeField] float _cicrleRadius;
    [SerializeField] float _attackTimer = 2f;
    [SerializeField] GameObject[] _strightBullet;
    [SerializeField] GameObject[] _chaserBullet;

    int _currentHeath;
    Transform _playerTransform;
    Vector2 _centerPos;
    float _currentAngle;

    bool _isAttacking = false;

    SpriteRenderer _spriteRenderer;
    CircleCollider2D _circleCollider2D;
    Animator _anim;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_player == null)
        {
            Debug.LogError("Game Manager is NULL");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UI>();
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }

        _playerTransform = GameObject.Find("Player").transform;
        if (_playerTransform == null)
        {
            Debug.LogError("Player is NULL");
        }

        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }

        _circleCollider2D = GetComponent<CircleCollider2D>();
        if (_circleCollider2D == null)
        {
            Debug.LogError("circle Collider is NULL");
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("Sprite Renderer is NULL");
        }
        _currentHeath = _maxHealth;
        _currentAngle = 0f;
    }

    void Update()
    {
        if (_isAttacking == true)
        {
            Attack();
        }
        else
        {
            _attackTimer -= Time.deltaTime;
            if (_attackTimer <= 0)
            {
                _isAttacking = true;
                Attack();
                _attackTimer = 2f;
            }
            CircleMovement();
        }
    }

    private void CircleMovement()
    {
        _currentAngle += _speed * Time.deltaTime;

        float x = Mathf.Cos(_currentAngle) * _cicrleRadius;
        float y = Mathf.Sin(_currentAngle) * _cicrleRadius;
        Vector2 newPosition = _centerPos + new Vector2(x, y);

        transform.position = newPosition;
    }

    public void Attack()
    {
        if (_player != null)
        {
            int attacktype = Random.Range(0, 2);

            Vector2 direction = _playerTransform.position - transform.position;

            if (direction.magnitude <= _attackRange)
            {
                if (attacktype == 0)
                {
                    StraightShot();
                }
                if (attacktype == 1)
                {
                    ChaseShot();
                }
            }
            _isAttacking = false;
        }
    }

    void ChaseShot()
    {
        GameObject chaser = Instantiate(_chaserBullet[Random.Range(0, _chaserBullet.Length)], transform.position, Quaternion.identity);
        chaser.GetComponent<BossBulletChaser>().SetTarget(_playerTransform);
    }

    void StraightShot()
    {
        GameObject straightShot = Instantiate(_strightBullet[Random.Range(0, _strightBullet.Length)], transform.position, Quaternion.identity);
    }

    public void TakeDamge(int damage)
    {
        _currentHeath -= damage;
        _speed += 0.25f;
        _attackRange += 1f;
        _cicrleRadius += 1f;
        if (_cicrleRadius >= 5f)
        {
            _cicrleRadius = 5f;
        }
        StartCoroutine(Flash());
        if (_currentHeath <= 0)
        {
            Die();
            _gameManager.YouWin();
            _uiManager.YouWinTheGamer();
        }
    }

    IEnumerator Flash()
    {
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.color = Color.white;
    }

    void Die()
    {
        _player.AddScore(_score);
        _anim.SetTrigger("onEnemyDeath");
        _speed = 0;
        _circleCollider2D.enabled = false;
        Destroy(gameObject, 2.8f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            TakeDamge(2);
        }

        if (other.tag == "Wrench")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                TakeDamge(2);
            }
        }

        if (other.tag == "Powerup")
            Destroy(other.gameObject);
    }
}