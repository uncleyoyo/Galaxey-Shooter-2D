using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] float _speed = 3;
    [SerializeField] int _poweupID;
    [SerializeField] AudioClip _powerUpSound;
 
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
                AudioSource.PlayClipAtPoint(_powerUpSound, transform.position);
                switch (_poweupID)
                {
                    case 0:
                        player.TriplaShot();
                        Debug.Log("collected triple shot powerup!");
                        break;
                    case 1:
                        player.Shields();
                        Debug.Log("collected shield boost powerup!");
                        break;
                    case 2: player.AddWrenches();
                        Debug.Log("Collected Ammo Poweup");
                        break;
                    case 3: player.AddHealth();
                        Debug.Log("Heatlth restore");
                        break;
                    case 4: player.RapidShot();
                        Debug.Log("rapid fire colleted");
                        break;
                }
            }
            Destroy(gameObject);
        }
    }
}
