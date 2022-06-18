using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SpawnerScript : MonoBehaviour
{
    public GameObject planetPrefab;
    public int randomX, randomY;
    public Vector3 randomPos;

    public float timeLeft;
    bool gameStarted = true;

    public int playerScore;
    public TextMeshProUGUI roundTimeText;

    public GameObject restartCanvas;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPlanet", 1, 1);
        restartCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Countdown
        timeLeft -= Time.deltaTime;
        roundTimeText.text = "TIMELEFT: " + timeLeft.ToString();

        if (timeLeft <= 0 && gameStarted)
        {
            print("Game Over");
            gameStarted = false;
            // Instanziieren stoppen
            CancelInvoke("SpawnPlanet");
            // Punktestand übermitteln
            GameObject.FindObjectOfType<Leaderboard>().SetLeaderboardEntry(playerScore);
            // Restart Canvas aktivieren
            restartCanvas.SetActive(true);
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

    public void AddPlayerScore()
    {
        // Läuft die Runde noch?
        if (gameStarted)
        {
            playerScore += 1;
        }
    }

    public void SceneRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}