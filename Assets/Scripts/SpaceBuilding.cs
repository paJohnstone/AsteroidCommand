using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class SpaceBuilding : MonoBehaviour
{
    [SerializeField] Collider2D[] colliderComponents;
    [SerializeField] Sprite[] sprites;
    [SerializeField] float hitPoints = 3;
    [SerializeField] GameObject explosion;
    SpriteRenderer sr;
    
    // Start is called before the first frame update

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (hitPoints)
        {
            case 3:
                sr.sprite = sprites[3];
                break;
            case 2:
                sr.sprite = sprites[2];
                break;
            case 1:
                sr.sprite = sprites[1];
                break;
            case 0:
                sr.sprite = sprites[0];
                foreach (Collider2D collider in colliderComponents)
                {
                    collider.enabled = false;
                }
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            foreach (Collider2D collider in colliderComponents)
            {
                collider.enabled = false;
            }
            hitPoints = 0;
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
        if (collision.gameObject.CompareTag("SmallAsteroid"))
        {
            if (hitPoints > 0)
            {
                hitPoints--;
            }
        }
    }
}
