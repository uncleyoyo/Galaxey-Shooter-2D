using UnityEngine;

public class Cop : MonoBehaviour
{
    [SerializeField] int _speed = 2;
    UI _uiManager;
    Animator _anim;
    BoxCollider2D _boxCollider2D;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UI>();
        if (_uiManager == null)
        {
            Debug.LogError("UIManager is NULL");
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
        transform.Translate(Vector2.left * _speed * Time.deltaTime) ;
        //transform.position = new Vector2(11, Random.Range(-4f, 6f));
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wrench")
        {
            Destroy(other.gameObject);

            if (_uiManager != null)
            {
                _uiManager.ZeroScore();
            }
            _anim.SetTrigger("onEnemyDeath");
            _speed = 0;
            Destroy(_boxCollider2D);
            Destroy(gameObject, 2.8f);
        }
    }
}
