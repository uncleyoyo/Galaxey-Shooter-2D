using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveEnemy : MonoBehaviour
{
    [SerializeField] int _score = 80;
    [SerializeField] float _speed = 1f;
    [SerializeField] float _amplitude = 0.3f;

    Animator _anim;
    Player _player;
    BoxCollider2D _boxCollider2D;
    SpriteRenderer _spriteRenderer;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }

        _boxCollider2D = GetComponent<BoxCollider2D>();
        if (_boxCollider2D == null)
        {
            Debug.LogError("Box Collder is NULL");
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("Sprite Renderer is NULL");
        }
    }

    void Update()
    {

        float y = Mathf.Sin(Time.time) * _amplitude;
        transform.Translate(new Vector2(-1, y) * _speed * Time.deltaTime);

        if (transform.position.x < -11)
        {
            transform.position = new Vector2(11, Random.Range(-4f, 6f));
        }
        if (_player != null)
        {
            if (Vector2.Distance(_player.transform.position, transform.position) <= 4f)
            {
                Ram();
            }
            else
            {
                NormalState();
            }
        }
    }

    private void NormalState()
    {
        _speed = 1f;
        _amplitude = .3f;
    }

    public void Ram()
    {
        _speed = 8;
        _amplitude = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            _anim.SetTrigger("onEnemyDeath");
            _speed = 0;
            _boxCollider2D.enabled = false;
            Destroy(gameObject, 2.8f);
        }

        if (other.tag == "Wrench")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(_score);

                _anim.SetTrigger("onEnemyDeath");
                _speed = 0;
                _boxCollider2D.enabled = false;
                Destroy(gameObject, 2.8f);
            }
        }

        if (other.tag == "Powerup")
            Destroy(other.gameObject);
    }
}
