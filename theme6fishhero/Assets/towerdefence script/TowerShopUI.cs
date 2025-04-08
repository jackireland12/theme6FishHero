using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TowerShopUI : MonoBehaviour
{
    public CollectionManager collectionManager; // Reference to the collection system
    public TMP_Text normalCurrencyText;
    public TMP_Text fastCurrencyText;
    public TMP_Text heavyCurrencyText;

    public GameObject towerPrefab; // Tower to spawn
    public Transform spawnPoint;   // Where the tower will be placed

    public int normalTowerCost = 5;
    public int fastTowerCost = 3;
    public int heavyTowerCost = 8;

    private void Start()
    {
        UpdateUI();
    }

    public void BuyTowerWithNormal()
    {
        if (collectionManager.enemyCollection["normal"] >= normalTowerCost)
        {
            collectionManager.enemyCollection["normal"] -= normalTowerCost;
            SpawnTower();
        }
        else
        {
            Debug.Log("Not enough Normal enemies collected!");
        }
        UpdateUI();
    }

    public void BuyTowerWithFast()
    {
        if (collectionManager.enemyCollection["fast"] >= fastTowerCost)
        {
            collectionManager.enemyCollection["fast"] -= fastTowerCost;
            SpawnTower();
        }
        else
        {
            Debug.Log("Not enough Fast enemies collected!");
        }
        UpdateUI();
    }

    public void BuyTowerWithHeavy()
    {
        if (collectionManager.enemyCollection["heavy"] >= heavyTowerCost)
        {
            collectionManager.enemyCollection["heavy"] -= heavyTowerCost;
            SpawnTower();
        }
        else
        {
            Debug.Log("Not enough Heavy enemies collected!");
        }
        UpdateUI();
    }

    private void SpawnTower()
    {
        Instantiate(towerPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("Tower Purchased!");
    }

    private void UpdateUI()
    {
        normalCurrencyText.text = "Normal: " + collectionManager.enemyCollection["normal"];
        fastCurrencyText.text = "Fast: " + collectionManager.enemyCollection["fast"];
        heavyCurrencyText.text = "Heavy: " + collectionManager.enemyCollection["heavy"];
    }
}
