using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 7;
    [SerializeField] GameObject _laser;
    [SerializeField] float _fireRate = 0.5f;
    float _canFire = -1;
    [SerializeField] int _lives = 3;
    SpawnManager _spawnManager;
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
    }

    void Update()
    {
        Move();
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
            Shoot();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector2(vertical, -horizontal) * _speed * Time.deltaTime);

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
        _canFire = Time.time + _fireRate;
        Instantiate(_laser, transform.position + new Vector3(.3f, 0, 0), Quaternion.identity);
    }


    public void Damage()
    {
        _lives--;
        if (_lives < 1)
        {
            _spawnManager.PlayerDied();
            Destroy(gameObject);
        }
    }
}