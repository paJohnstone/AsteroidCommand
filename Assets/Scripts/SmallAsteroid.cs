using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class SmallAsteroid : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] GameObject smallAsteroidExplosion;
    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider;
    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        spriteRenderer.enabled = false;
        circleCollider.enabled = false;
        Instantiate(smallAsteroidExplosion, transform.position, Quaternion.identity);
        if (collision.gameObject.CompareTag("Bullet"))
        {
            FindObjectOfType<GameManager>().currentScore += 150;
        }
        Destroy(gameObject, 3f);

    }
}
