using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] float _speed = 4;

    Animator _anim;
    Player _player;
    BoxCollider2D _boxCollider2D;

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
            Debug.LogError("Boc Collder is NULL");
        }
    }

    void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);

        if (transform.position.x < -11)
        {
            transform.position = new Vector2(11, Random.Range(-4f, 6f));
        }
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
                _player.AddScore(score);
            }
            _anim.SetTrigger("onEnemyDeath");
            _speed = 0;
            _boxCollider2D.enabled = false;
            Destroy(gameObject, 2.8f);
        }
    }
}
