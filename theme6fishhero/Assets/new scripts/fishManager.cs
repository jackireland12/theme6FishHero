using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class fishManager : MonoBehaviour
{
    [Header("Fish Prefabs for Merging")]
    public GameObject[] fishPrefabs; // Level 1 = index 0, Level 2 = index 1, etc.

    [Header("Attack Fish Prefabs")]
    public GameObject[] attackFishPrefabs; // Attacker prefabs: Level 1 = index 0, etc.

    [Header("Fish Lists")]
    public List<GameObject> baseFish = new List<GameObject>();   // Mergeable fish
    public List<GameObject> activeFish = new List<GameObject>(); // Attacker fish during wave

    [Header("Battle Area")]
    public Transform battleArea; // Where attackers spawn
    public int maxLevel;
    public int wavecoins;
    public static fishManager Instance;

    private bool isAttackMode = false; // Tracks current mode

    private void Update()
    {
        if (isAttackMode)
        {
            // Remove null or inactive fish
            activeFish.RemoveAll(fish => fish == null || !fish.activeSelf);
            if (activeFish.Count == 0)
            {
                StartMergingMode();
                shop shop = FindObjectOfType<shop>();
                if (shop != null)
                {
                    shop.AddCoins(wavecoins);
                    Debug.Log("Awarded 20 coins for clearing wave");
                }
            }
        }
    }

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    // Get mergeable fish prefab
    public GameObject GetFishPrefab(int level)
    {
        if (level - 1 >= 0 && level - 1 < fishPrefabs.Length)
        {
            return fishPrefabs[level - 1];
        }

        Debug.LogWarning("No fish prefab for level " + level);
        return null;
    }
    // Get attack fish prefab
    public GameObject GetAttackFishPrefab(int level)
    {
        if (level - 1 >= 0 && level - 1 < attackFishPrefabs.Length)
        {
            return attackFishPrefabs[level - 1];
        }
        Debug.LogWarning("No attack fish prefab for level " + level);
        return null;
    }

    // Add a fish to the list of base (mergeable) fish
    public void AddBaseFish(GameObject fish)
    {
        if (fish != null && !baseFish.Contains(fish))
        {
            baseFish.Add(fish);
            Debug.Log("Added fish to baseFish: " + fish.name);
        }
    }

    // Remove a fish (e.g., before merging)
    public void RemoveBaseFish(GameObject fish)
    {
        if (fish != null && baseFish.Contains(fish))
        {
            baseFish.Remove(fish);
            Debug.Log("Removed fish from baseFish: " + fish.name);
        }
    }

    // Switch to Attack Mode and start the wave
    public void StartAttackMode()
    {
        if (isAttackMode) return; // Prevent re-entering Attack Mode
        isAttackMode = true;
        Debug.Log("Entering Attack Mode");
        activeFish.Clear(); // Clear any old entries
        // Disable mergeDetect scripts and spawn attackers
        foreach (GameObject fish in baseFish.ToList())
        {
            if (fish == null) continue;

            // Disable the fish GameObject
            fish.SetActive(false);

            mergeDetect fishMerge = fish.GetComponent<mergeDetect>();
            if (fishMerge != null)
            {
                // Disable merging functionality
                fishMerge.enabled = false;

                // Spawn attacker based on fish level
                Vector3 spawnPos = battleArea.position + (Vector3)(Random.insideUnitCircle * 2f);
                GameObject attacker = fishMerge.SpawnAttacker(spawnPos);

                if (attacker != null)
                {
                    activeFish.Add(attacker);
                    Debug.Log("Spawned attacker: " + attacker.name);
                }
            }
        }
    }

    public void RemoveActiveFish(GameObject fish)
    {
        if (fish != null && activeFish.Contains(fish))
        {
            activeFish.Remove(fish);
            Debug.Log("Removed fish from activeFish: " + fish.name);
        }
    }

    // Switch back to Merging Mode and end the wave
    public void StartMergingMode()
    {
        if (!isAttackMode) return; // Prevent re-entering Merging Mode
        isAttackMode = false;
        Debug.Log("Entering Merging Mode");

        // Destroy all attackers
        foreach (GameObject attacker in activeFish)
        {
            if (attacker != null)
                Destroy(attacker);
        }
        activeFish.Clear();

        // Re-enable mergeable fish and their mergeDetect scripts
        foreach (GameObject fish in baseFish)
        {
            if (fish != null)
            {
                fish.SetActive(true);
                mergeDetect fishMerge = fish.GetComponent<mergeDetect>();
                if (fishMerge != null)
                {
                    fishMerge.enabled = true;
                }
            }
        }
    }

    // Check current mode (optional, for other scripts)
    public bool IsAttackMode()
    {
        return isAttackMode;
    }
}
