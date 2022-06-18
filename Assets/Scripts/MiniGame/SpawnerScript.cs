using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject planetPrefab;
    public int randomX, randomY;
    public Vector3 randomPos;

    public float timeLeft;
    bool gameStarted = true;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPlanet", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // Countdown
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0 && gameStarted)
        {
            print("Game Over");
            gameStarted = false;
            // Instanziieren stoppen
            CancelInvoke("SpawnPlanet");
        }
    }

    public void SpawnPlanet()
    {
        // Zufällige Koordinaten generieren (X + Y Achse)
        randomX = Random.Range(-9, 5);
        randomY = Random.Range(-4, 6);

        randomPos = new Vector3(randomX, randomY, 0);

        // Instanziieren des Planeten
        Instantiate(planetPrefab, randomPos, Quaternion.identity);
    }
}