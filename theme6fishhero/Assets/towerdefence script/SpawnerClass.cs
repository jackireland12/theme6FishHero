using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerClass : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    //public GameObject enemie1;
    public List<GameObject> enemineType;
    public int waveNumber = 1;
    private bool isSpawing = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            for (int i = 0; i < 3; i++) 
            { 
                spawnEnemie(); 
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartWaves();
        }
        
        CleanEnemyList();
        if (enemies.Count == 0&& isSpawing == false)
        {
            StartWaves();
        }
        
    }
    IEnumerator spawnWaves(int count)
    {
        for(int i = 0;i < count; i++)
        {
            
            int RandomEnemie = Random.Range(0, enemineType.Count);
            GameObject choisenEnime = enemineType[RandomEnemie];
            Vector3 randomspawn = new Vector3(5.0f, Random.Range(-5.0f, 5.0f), 0f);
            yield return new WaitForSeconds(0.3f);
            GameObject enemy = Instantiate(choisenEnime, randomspawn, Quaternion.identity);
            enemies.Add(enemy);
        }
        isSpawing = false ;
       

    }

    void StartWaves()
    {
        isSpawing = true;
        waveNumber++;
        int enemiesCount = Mathf.RoundToInt(5 + waveNumber *3);
        StartCoroutine( spawnWaves(enemiesCount));

    }

    void spawnEnemie()
    {
        //Debug.Log(enemineType.Count);
        int RandomEnemie = Random.Range(0, enemineType.Count);
        GameObject choisenEnime = enemineType[RandomEnemie];
        Vector3 randomspawn = new Vector3(5.0f, Random.Range(-5.0f, 5.0f), 0f);
        GameObject enemy = Instantiate(choisenEnime, randomspawn,Quaternion.identity);
        enemies.Add(enemy);

    }
    void deleteEnemie()
    {
        GameObject enemyToDelete = enemies[0];
        enemies.RemoveAt(0);
        Destroy(enemyToDelete);
        Debug.Log("del");

    }
    void CleanEnemyList()
    {
        for (int i = enemies.Count - 1; i >= 0; i--) // Loop backwards to avoid index issues
        {
            if (enemies[i] == null) // Only remove if destroyed
            {
                enemies.RemoveAt(i);
               // Debug.Log("cleen list");
            }
        }
    }
}