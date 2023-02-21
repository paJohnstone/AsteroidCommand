using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float thrustAmount = 1f;
    public bool thrusting {get; private set;}
    public float turnDirection { get; private set; } = 0f;
    [SerializeField] float rotationSpeed = 0.1f;

    [SerializeField] float respawnDelay = 3f;
    [SerializeField] float respawnInvulnerability = 3f;

    Rigidbody2D shipRB;

    public Bullet bulletPrefab;

    private void Awake()
    {
        shipRB = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine("TurnOnCollisions");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1f;
        }
        else
        {
            turnDirection = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    private void FixedUpdate()
    {
        if (thrusting)
        {
            shipRB.AddForce(transform.up * thrustAmount);
        }

        if (turnDirection != 0f)
        {
            shipRB.AddTorque(rotationSpeed * turnDirection);
        }
    }
    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.LaunchProjectile(transform.up);
    }

    IEnumerator TurnOnCollisions()
    {
        yield return new WaitForSeconds(respawnInvulnerability);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
