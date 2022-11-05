using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] float _speed = 3;
    [SerializeField] int _poweupID;
    [SerializeField] AudioClip _powerUp;
    void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
        Destroy(gameObject, 9);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(_powerUp, transform.position);
                switch (_poweupID)
                {
                    case 0:
                        player.TriplaShot();
                        Debug.Log("collected triple shot powerup!");
                        break;
                    case 1:
                        Debug.Log("collected speed boost powerup!");
                        break;
                }
            }
            Destroy(gameObject);
        }
    }
}
