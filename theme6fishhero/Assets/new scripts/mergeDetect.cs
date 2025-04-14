using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class mergeDetect : MonoBehaviour
{
    public int level = 1;
    public GameObject attackFish;
        public fishManager fishManager;

    private bool isMerging = false;

    public GameObject SpawnAttacker(Vector3 position)
    {
        GameObject clone = Instantiate(attackFish, position, Quaternion.identity);
        return clone;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isMerging) return;

        mergeDetect otherFish = other.GetComponent<mergeDetect>();

        if (otherFish != null && !otherFish.isMerging)
        {
            if (otherFish.level == level && level < fishManager.maxLevel)
            {
                StartCoroutine(MergeWith(otherFish));
            }
        }
    }

    IEnumerator MergeWith(mergeDetect other)
    {
        isMerging = true;
        other.isMerging = true;

        Vector3 spawnPos = (transform.position + other.transform.position) / 2;

        yield return new WaitForSeconds(0.1f); // Optional for effect

        Destroy(gameObject);
        Destroy(other.gameObject);

        GameObject newFish = Instantiate(fishManager.GetFishPrefab(level + 1),spawnPos,Quaternion.identity);
        mergeDetect newFishScript = newFish.GetComponent<mergeDetect>();
        newFishScript.fishManager = fishManager;
    }


}
