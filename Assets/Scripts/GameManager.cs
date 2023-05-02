using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Ship ship;
    [SerializeField] BigAsteroid bigAsteroid;
    [SerializeField] float totalWaves;
    [SerializeField] float currentWave;
    [SerializeField] float setBetweenWaveTimer = 10f;
    [SerializeField] float asteroidsSentThisWave;
    [SerializeField] float asteroidsTotalThisWave;
    [SerializeField] float setAsteroidSpawnRate = 10f;
    float asteroidSpawnRate;
    [SerializeField] float asteroidsRemainingInWave;
    [SerializeField] float betweenWaveCountdown;
    [SerializeField] float asteroidSpawnCountdown;
    [SerializeField] bool startWave;
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject inGameUICanvas;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text livesText;


    SpaceBuilding[] spaceBuildings;
    Camera mainCamera;

    float asteroidMinPossibleX;
    float asteroidMaxPossibleX;
    float asteroidStartPositionY;

    public bool canPlay;
    public int currentScore = 0;
    public int currentLives = 3;

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

        betweenWaveCountdown = setBetweenWaveTimer;
        asteroidsSentThisWave = currentWave;

        highScoreText.SetText("" + Constants.highScore);
        scoreText.SetText("00000");
        asteroidSpawnRate = setAsteroidSpawnRate;

        foreach (SpaceBuilding spaceBuilding in spaceBuildings)
        {
            Constants.spaceBuildingHealth += spaceBuilding.hitPoints;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (canPlay)
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
                    betweenWaveCountdown = setBetweenWaveTimer;
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
            scoreText.SetText("" + currentScore);
            livesText.SetText("" + currentLives);
            int currentBuildingHealth = 0;
            foreach (SpaceBuilding spaceBuilding in spaceBuildings)
            {
                currentBuildingHealth += spaceBuilding.hitPoints;
            }
            Constants.spaceBuildingHealth = currentBuildingHealth;
        }

        if (currentLives <= 0 || Constants.spaceBuildingHealth <= 0)
        {
            GameOver();
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

    public void StartGame()
    {
        canPlay = true;
        mainMenuCanvas.SetActive(false);
        inGameUICanvas.SetActive(true);
    }
    void GameOver()
    {
        canPlay = false;
        mainMenuCanvas.SetActive(true);
        inGameUICanvas.SetActive(false);
        if (Constants.highScore < currentScore)
        {
            Constants.highScore = currentScore;
        }        
        currentLives = 3;
        currentScore = 0;
        asteroidSpawnRate = setAsteroidSpawnRate;
        asteroidsSentThisWave = 0;
        currentWave = 0;
        BigAsteroid[] bigAsteroidsToClear = FindObjectsOfType<BigAsteroid>();
        foreach (BigAsteroid bigAsteroid in bigAsteroidsToClear)
        {
            Destroy(bigAsteroid.gameObject);
        }
        SmallAsteroid[] smallAsteroidsToClear = FindObjectsOfType<SmallAsteroid>();
        foreach (SmallAsteroid smallAsteroid in smallAsteroidsToClear)
        {
            Destroy(smallAsteroid.gameObject);
        }
        foreach (SpaceBuilding spaceBuilding in spaceBuildings)
        {
            spaceBuilding.hitPoints = 3;
            foreach (Collider2D collider in spaceBuilding.colliderComponents)
            {
                collider.enabled = true;
            }
        }
    }
}
