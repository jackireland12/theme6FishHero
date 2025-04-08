using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectionBookUI : MonoBehaviour
{
    public TextMeshProUGUI fastText;
    public TextMeshProUGUI heavyText;
    public TextMeshProUGUI normalText;
    public CollectionManager collectionManager;
    
    public GameObject Canvas;

    void Start()
    {
        updateValue();  // Update UI on start
        Canvas.SetActive(false);
        
    }
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            UIupdate();
        }
    }
    public void updateValue()
    {
        // Clear the text first
        normalText.text = "Normal Enemies: 0";
        fastText.text = "Fast Enemies: 0";
        heavyText.text = "Heavy Enemies: 0";

        // Update each type based on the dictionary's values
        if (collectionManager.enemyCollection.ContainsKey("normal"))
        {
            normalText.text = $"Normal Enemies: {collectionManager.enemyCollection["normal"]}";
        }

        if (collectionManager.enemyCollection.ContainsKey("fast"))
        {
            fastText.text = $"Fast Enemies: {collectionManager.enemyCollection["fast"]}";
        }

        if (collectionManager.enemyCollection.ContainsKey("heavy"))
        {
            heavyText.text = $"Heavy Enemies: {collectionManager.enemyCollection["heavy"]}";
        }

    }
    void UIupdate()
    {
        // Toggle the active state of the collection book UI
        bool isActive = Canvas.activeSelf;  // Check if it's currently active
        Canvas.SetActive(!isActive);  // Toggle it

    }
}
