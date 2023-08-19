using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public Wave[] waves;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public Text waveCountdownText;

    public GameManager gameManager;
    private int waveIndex = 0;

    void Update ()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }
        
        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }
        
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown,0f,Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}",countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

		Wave wave = waves[waveIndex];

		EnemiesAlive = wave.count;

        if (wave.enemy2 != null && wave.spawnPoint2 != null)
        {
            EnemiesAlive = wave.count * 2;
            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemy, wave.spawnPoint);
                SpawnEnemy2(wave.enemy2, wave.spawnPoint2);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }
        else
        {
            EnemiesAlive = wave.count;
            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemy, wave.spawnPoint);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }

        waveIndex++;

    }
    public void SpawnEnemy(GameObject enemy, Transform spawnPoint)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation); 
    }

    public void SpawnEnemy2(GameObject enemy2, Transform spawnPoint2)
    {
        Instantiate(enemy2, spawnPoint2.position, spawnPoint2.rotation); 
    }
    
}
