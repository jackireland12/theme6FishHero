using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UI;

public class shop : MonoBehaviour
{
    public fishManager fishManager; // Reference to fishManager
    public Transform spawnArea; // Where fish spawn
    public int fishPrice = 10; // Cost of the fish
    public TMPro.TMP_Text buyButton; // Buy fish button
    public TMPro.TextMeshProUGUI coinsText; // Display coins

    private int coins = 50; // Starting coins

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        // Enable button only in Merging Mode and with enough coins
        buyButton.gameObject.SetActive(!fishManager.IsAttackMode() && coins >= fishPrice);
    }

    public void BuyFish()
    {
        if (fishManager.IsAttackMode())
        {
            Debug.Log("Cannot buy fish in Attack Mode");
            return;
        }

        if (coins >= fishPrice)
        {
            coins -= fishPrice;

            // Spawn Level 1 fish
            GameObject fishPrefab = fishManager.GetFishPrefab(1);
            if (fishPrefab != null)
            {
                Vector3 spawnPos = spawnArea.position + (Vector3)(Random.insideUnitCircle * 1f);
                GameObject newFish = Instantiate(fishPrefab, spawnPos, Quaternion.identity);

                // Initialize fish
                mergeDetect fishScript = newFish.GetComponent<mergeDetect>();
                if (fishScript != null)
                {
                    fishScript.fishManager = fishManager;
                    fishScript.level = 1;
                    fishScript.attackFish = fishManager.GetAttackFishPrefab(1);
                }

                // Ensure fish is active
                newFish.SetActive(true);

                // Add to fishManager
                fishManager.AddBaseFish(newFish);

                Debug.Log("Bought Level 1 fish");
            }

            UpdateUI();
        }
    }
    private void UpdateUI()
    {
        if (coinsText != null)
        {
            coinsText.text = $"Coins: {coins}";
        }
    }

    // Call this to add coins (e.g., from enemies)
    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateUI();
    }
}
