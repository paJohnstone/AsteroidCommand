using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Ship ship;
    [SerializeField] BigAsteroid bigAsteroid;
    [SerializeField] float totalWaves;
    [SerializeField] float currentWave;
    [SerializeField] float betweenWaveTimer = 10f;
    [SerializeField] float asteroidsSentThisWave;
    [SerializeField] float asteroidsTotalThisWave;
    [SerializeField] float asteroidSpawnRate = 10f;
    [SerializeField] float asteroidsRemainingInWave;
    [SerializeField] float betweenWaveCountdown;
    [SerializeField] float asteroidSpawnCountdown;
    [SerializeField] bool startWave;
    SpaceBuilding[] spaceBuildings;
    Camera mainCamera;

    float asteroidMinPossibleX;
    float asteroidMaxPossibleX;
    float asteroidStartPositionY;

    private void Awake()
    {
        mainCamera = Camera.main;
        spaceBuildings = FindObjectsOfType<SpaceBuilding>();
    }

    void Start()
    {
        asteroidMinPossibleX = mainCamera.ViewportToWorldPoint(new Vector2(0, 0)).x;
        asteroidMaxPossibleX = mainCamera.ViewportToWorldPoint(new Vector2(1, 1)).x;
        asteroidStartPositionY = mainCamera.ViewportToWorldPoint(new Vector2(1, 1)).y + 1;

        betweenWaveCountdown = betweenWaveTimer;
        asteroidsSentThisWave = currentWave;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnBigAsteroidOnQueue();
        }
        if (!startWave)
        {
            betweenWaveCountdown -= Time.deltaTime;
            if (betweenWaveCountdown <= 0)
            {
                startWave = true;
                StartCoroutine("SpawnBigAsteroid");
                betweenWaveCountdown = betweenWaveTimer;
                currentWave++;
            }
        }
        if (asteroidsSentThisWave > currentWave)
        {
            StopCoroutine("SpawnBigAsteroid");
            asteroidsSentThisWave = 0;
            if (currentWave > 0)
            {
                asteroidSpawnRate = asteroidSpawnRate - 0.25f;
            }
            startWave = false;
        }
    }

    void Respawn()
    {

    }

    void SpawnBigAsteroidOnQueue()
    {
        float x = Random.Range(asteroidMinPossibleX, asteroidMaxPossibleX);
        Vector2 startPos = new Vector2(x, asteroidStartPositionY);
        Instantiate(bigAsteroid, startPos, this.transform.rotation);
    }

    IEnumerator SpawnBigAsteroid()
    {
        while (true)
        {
            float x = Random.Range(asteroidMinPossibleX, asteroidMaxPossibleX);
            Vector2 startPos = new Vector2(x, asteroidStartPositionY);
            Instantiate(bigAsteroid, startPos, this.transform.rotation);

            asteroidsSentThisWave++;

            yield return new WaitForSeconds(asteroidSpawnRate);
        }
    }
}
