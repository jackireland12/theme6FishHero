using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishManager : MonoBehaviour
{
    [Header("Fish Prefabs for Merging")]
    public GameObject[] fishPrefabs; // Level 1 = index 0, Level 2 = index 1, etc.

    [Header("Fish Lists")]
    public List<GameObject> baseFish = new List<GameObject>();   // Mergeable fish
    public List<GameObject> activeFish = new List<GameObject>(); // Attacker fish during wave
    
    [Header("Battle Area")]
    public Transform battleArea; // Where attackers spawn
    public int maxLevel;
    public static fishManager Instance;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Get the prefab for a given level
    public GameObject GetFishPrefab(int level)
    {
        if (level - 1 < fishPrefabs.Length)
        {
            return fishPrefabs[level - 1];
        }

        Debug.LogWarning("No fish prefab for level " + level);
        return null;
    }

    // Add a fish to the list of base (mergeable) fish
    public void AddBaseFish(GameObject fish)
    {
        if (!baseFish.Contains(fish))
            baseFish.Add(fish);
        Debug.Log("work");
    }

    // Remove a fish (e.g. before merging)
    public void RemoveBaseFish(GameObject fish)
    {
        if (baseFish.Contains(fish))
            baseFish.Remove(fish);
    }

    // Start the wave: each base fish spawns an attacker
    public void StartWave()
    {
        Debug.Log("work");
        foreach (GameObject fish in baseFish)
        {
            fish.SetActive(false); // Hide base fish for now

            mergeDetect fishMerge = fish.GetComponent<mergeDetect>();
            if (fishMerge != null)
            {
                Vector3 spawnPos = battleArea.position + (Vector3)(Random.insideUnitCircle * 2f);
                GameObject attacker = fishMerge.SpawnAttacker(spawnPos);

                if (attacker != null)
                {
                    activeFish.Add(attacker);
                }
            }
        }
    }

    // End the wave: destroy attackers and bring back base fish
    public void EndWave()
    {
        foreach (GameObject attacker in activeFish)
        {
            Destroy(attacker);
        }

        activeFish.Clear();

        foreach (GameObject fish in baseFish)
        {
            fish.SetActive(true);
        }
    }
}
