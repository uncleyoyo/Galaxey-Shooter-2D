using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] float _speed = 3f;

    void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
        Destroy(gameObject, 4);
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
        }

        if (other.tag == "Wrench")
        {
            Destroy(other.gameObject);
        }

        if (other.tag == "Powerup")
            Destroy(other.gameObject);
    }
}
