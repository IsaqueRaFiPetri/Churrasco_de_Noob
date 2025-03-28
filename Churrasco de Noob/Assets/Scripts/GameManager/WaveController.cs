using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class WaveController : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public GameObject[] enemies;
        public float waveDuration;
        public float spawnRate;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public Transform[] boxSpawnPoints;
    public GameObject boxPrefab;
    public float timeBetweenWaves = 5f;
    public int boxesPerWave = 3;

    public GameObject portal;

    public TMP_Text waveText;
    public TMP_Text timerText;

    private int currentWaveIndex = 0;
    private bool spawningWave = false;
    private float waveTimer;
    private float waitTimer;
    private bool waitingForNextWave = false;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (spawningWave)
        {
            waveTimer -= Time.deltaTime;
            timerText.text = Mathf.Ceil(waveTimer).ToString();
        }
        else if (waitingForNextWave)
        {
            waitTimer -= Time.deltaTime;
            timerText.text = Mathf.Ceil(waitTimer).ToString();
        }
    }

    IEnumerator SpawnWaves()
    {
        while (currentWaveIndex < waves.Length)
        {
            waveText.text = "Wave: " + (currentWaveIndex + 1).ToString();
            yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            currentWaveIndex++;

            if (currentWaveIndex < waves.Length)
            {
                yield return StartCoroutine(SpawnBoxes());
                waveText.text = "Break";
                waitingForNextWave = true;
                waitTimer = timeBetweenWaves;
                yield return new WaitForSeconds(timeBetweenWaves);
                waitingForNextWave = false;
            }
            else
            {
                EndGame();
            }
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        spawningWave = true;
        waveTimer = wave.waveDuration;
        float spawnInterval = 1f / wave.spawnRate;
        float startTime = Time.time;

        while (Time.time - startTime < wave.waveDuration)
        {
            GameObject enemy = wave.enemies[Random.Range(0, wave.enemies.Length)];
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemy, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }

        spawningWave = false;
    }

    IEnumerator SpawnBoxes()
    {
        List<Transform> availableSpawns = new List<Transform>(boxSpawnPoints);

        for (int i = 0; i < boxesPerWave && availableSpawns.Count > 0; i++)
        {
            int index = Random.Range(0, availableSpawns.Count);
            Transform boxSpawn = availableSpawns[index];
            availableSpawns.RemoveAt(index);
            Instantiate(boxPrefab, boxSpawn.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void EndGame()
    {
        waveText.text = "Entre no portal";
        timerText.text = "";
        portal.SetActive(true);
    }
}
