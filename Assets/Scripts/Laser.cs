using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float _speed = 8;
  
    void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
        Destroy(gameObject, 5);
    }
}
