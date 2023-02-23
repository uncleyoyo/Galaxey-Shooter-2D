using UnityEngine;

public class Wrench : MonoBehaviour
{
    [SerializeField] float _speed = 8;
    
    void Update()
    {
        transform.Rotate(Vector2.right * _speed);
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(gameObject, 5);
    }
}
