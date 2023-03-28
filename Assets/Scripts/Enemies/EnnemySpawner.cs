using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class EnnemySpawner : MonoBehaviour
{

    public List<GameObject> enemyPrefabs;
    public float spawnDelay = 1f;
    public int waves = 10;
    public int enemiesPerWave = 3;
    public float waveDelay = 5f;
    public float decreaseTime = 10;
    private int currentWave = 0;
    private int currentEnemies = 0;
    float counter = 0;

    void Start()
    {
        currentEnemies = enemiesPerWave; //3
        StartCoroutine(SpawnWaves());
    }
    IEnumerator SpawnWaves()
    {
        while (currentWave < waves) // 1 2 3 4 5 6 
        {
            if (currentEnemies <= 0) // 0
            {
                currentWave++; // 1 2 3
                currentEnemies = enemiesPerWave; // 3
                yield return new WaitForSeconds(waveDelay);
            }
            else
            {
                Vector2 position = GetRandomCorner();
                GameObject enemyPrefab = GetRandomEnemyPrefab();
                SpawnEnemy(enemyPrefab, position);
                currentEnemies--;
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= 10 && (int)counter % 10 == 0)
        {
            counter = 0;
            enemiesPerWave += 1;
        }
    }

    Vector2 GetRandomCorner()
    {
        // choose a random corner of the screen
        int corner = Random.Range(0, 4);
        switch (corner)
        {
            case 0:
                return new Vector2(-23.33f, 18.84f);
            case 1:
                return new Vector2(-20.28f, -9.3f);
            case 2:
                return new Vector2(36.98f, -9.5f);
            case 3:
                return new Vector2(28.1f, 18f);
            default:
                return Vector2.zero;
        }
    }

    GameObject GetRandomEnemyPrefab()
    {
        // choose a random enemy from the list of enemy prefabs
        int index = Random.Range(0, enemyPrefabs.Count);
        return enemyPrefabs[index];
    }

    void SpawnEnemy(GameObject enemyPrefab, Vector2 position)
    {
        // instantiate the enemy prefab at the given position
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        enemy.transform.SetParent(transform);
    }
    
}
