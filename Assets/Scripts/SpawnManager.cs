using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {// calling a method 
        spawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    // Tracks the count of enemies 
    void Update()
    {// find's all objects with the specific script attached to it
        enemyCount = FindObjectsOfType <EnemyFollow>().Length;
    // if enemies ever become 0 spawn more enemies
        if (enemyCount == 0)
        {
            waveNumber++;
            spawnEnemyWave(waveNumber);
            Debug.Log(waveNumber + " Enemies" + "!");
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }
// created a private method to calculate spawnPos and clean up the code.
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
       
        return randomPos;
    }
    //void does a task.
    void spawnEnemyWave(int enemiesToSpawn)
    {// i initiate at 0, check condition (run loop body) i goes up by one (recheck condition)
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
}
