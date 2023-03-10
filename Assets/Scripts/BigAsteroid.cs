using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class BigAsteroid : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] GameObject smallAsteroidPrefab;
    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider;
    bool isDead;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
    // Start is called before the first frame update
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
        if (collision.gameObject.CompareTag("Bullet"))
        {
            spriteRenderer.enabled = false;
            circleCollider.enabled = false;
            SpawnSmallies();
            Destroy(gameObject, 3f);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            spriteRenderer.enabled = false;
            circleCollider.enabled = false;
            SpawnSmallies();
            Destroy(gameObject, 3f);
        }
        if (collision.gameObject.CompareTag("Building"))
        {
            spriteRenderer.enabled = false;
            circleCollider.enabled = false;
            Destroy(gameObject, 3f);
        }
    }

    void SpawnSmallies()
    {
        Vector2 sm1Pos = Random.insideUnitCircle;
        Vector2 sm2Pos = Random.insideUnitCircle;
        Vector2 sm3Pos = Random.insideUnitCircle;

        sm1Pos = new Vector2(this.transform.position.x + sm1Pos.x, this.transform.position.y + sm1Pos.y);
        sm2Pos = new Vector2(this.transform.position.x + sm2Pos.x, this.transform.position.y + sm2Pos.y);
        sm3Pos = new Vector2(this.transform.position.x + sm3Pos.x, this.transform.position.y + sm3Pos.y);

        GameObject sm1 = Instantiate(smallAsteroidPrefab, sm1Pos, Quaternion.identity);
        GameObject sm2 = Instantiate(smallAsteroidPrefab, sm2Pos, Quaternion.identity);
        GameObject sm3 = Instantiate(smallAsteroidPrefab, sm3Pos, Quaternion.identity);

        sm1.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5, 5), 1f), ForceMode2D.Impulse);
        sm2.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5, 5), 1f), ForceMode2D.Impulse);
        sm3.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5, 5), 1f), ForceMode2D.Impulse);
    }
}
