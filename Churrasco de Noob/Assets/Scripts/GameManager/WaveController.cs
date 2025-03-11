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
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public Transform[] boxSpawnPoints;
    public GameObject boxPrefab;
    public float timeBetweenWaves = 5f;
    public int boxesPerWave = 3;

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
                waitingForNextWave = true;
                waitTimer = timeBetweenWaves;
                yield return new WaitForSeconds(timeBetweenWaves);
                waitingForNextWave = false;
            }
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        spawningWave = true;
        waveTimer = wave.waveDuration;
        float startTime = Time.time;

        while (Time.time - startTime < wave.waveDuration)
        {
            GameObject enemy = wave.enemies[Random.Range(0, wave.enemies.Length)];
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemy, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }

        spawningWave = false;
    }

    IEnumerator SpawnBoxes()
    {
        for (int i = 0; i < boxesPerWave; i++)
        {
            Transform boxSpawn = boxSpawnPoints[Random.Range(0, boxSpawnPoints.Length)];
            Instantiate(boxPrefab, boxSpawn.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }

    }
}
