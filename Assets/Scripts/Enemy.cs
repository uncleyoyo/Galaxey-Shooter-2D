using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed = 4; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

            if(player != null)
            {
                player.Damage();
            }
            Destroy(gameObject);
        }

        if (other.tag == "Laser" )
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }
}
