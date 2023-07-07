using UnityEngine;

public class Powerup : MonoBehaviour
{
    Player _player;

    [SerializeField] float _speed = 3;
    [SerializeField] int _poweupID;
    [SerializeField] AudioClip _powerUpSound;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL");
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            MoveTowardsPlayer();
            Debug.Log('C');
        }
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
        Destroy(gameObject, 9);
    }

    public void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(_powerUpSound, transform.position);
                switch (_poweupID)
                {
                    case 0:
                        player.TriplaShot();
                        break;
                    case 1:
                        player.Shields();
                        break;
                    case 2:
                        player.AddWrenches();
                        break;
                    case 3:
                        player.AddHealth();
                        break;
                    case 4:
                        player.RapidShot();
                        break;
                    case 5:
                        player.SlowDown();
                        break;
                    case 6:
                        player.AddFartPower();
                        break;
                }
            }
            Destroy(gameObject);
        }
    }
}
