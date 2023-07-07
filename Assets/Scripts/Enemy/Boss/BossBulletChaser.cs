using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletChaser : MonoBehaviour
{
    [SerializeField] float _speed = 4;
    Transform _target;
  
    void Update()
    {
        if(_target != null)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            transform.position += direction * _speed * Time.deltaTime;
            Destroy(gameObject, 4);
        }
    }
    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;
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
