using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public Dictionary<string, int > enemyCollection = new Dictionary<string, int>();
    //string enemyType;
    //public CollectionBookUI CBU;


    private void Start()
    {
        //CBU = GetComponent<CollectionBookUI>();
    }
    public void enemyDefeated(string enimeType)
    {
        if (enemyCollection.ContainsKey(enimeType))
        {
            enemyCollection[enimeType] += 1;
        }
        else
        {
            enemyCollection[enimeType] = 1;
        }
        //CBU.updateValue();

        foreach (var enemy in enemyCollection)
        {
            //Debug.Log(enemy.Key + ": " + enemy.Value);
        }

    }
    




}
