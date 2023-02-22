using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Ship ship;
    [SerializeField] BigAsteroid bigAsteroid;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            float x = Random.Range(asteroidMinPossibleX, asteroidMaxPossibleX);
            Vector2 startPos = new Vector2(x, asteroidStartPositionY);
            Debug.Log("minX: " + asteroidMinPossibleX + " maxX: " + asteroidMaxPossibleX);
            Instantiate(bigAsteroid, startPos, this.transform.rotation);
        }
    }

    void Respawn()
    {

    }
}
