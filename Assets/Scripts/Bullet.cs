using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    Rigidbody2D bulletRB;
    [SerializeField] float speed = 500f;
    [SerializeField] float maxLifetime = 1.5f;
    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody2D>();
    }
    public void LaunchProjectile(Vector2 direction)
    {
        bulletRB.AddForce(direction * speed);
        Destroy(gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
