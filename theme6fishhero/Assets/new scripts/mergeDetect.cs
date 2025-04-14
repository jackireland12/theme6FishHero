using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Build.Content;
using UnityEngine;

public class mergeDetect : MonoBehaviour
{
    public int level = 1;
    public GameObject attackFish; // Attacker prefab, set in Inspector for pre-placed fish
    public fishManager fishManager;

    private bool isMerging = false;

    public GameObject SpawnAttacker(Vector3 position)
    {
        if (attackFish == null)
        {
            Debug.LogWarning("No attackFish assigned for level " + level);
            return null;
        }
        GameObject clone = Instantiate(attackFish, position, Quaternion.identity);
        return clone;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isMerging || fishManager.IsAttackMode()) return;

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

        yield return new WaitForSeconds(0.1f);

        fishManager.RemoveBaseFish(gameObject);
        fishManager.RemoveBaseFish(other.gameObject);

        Destroy(gameObject);
        Destroy(other.gameObject);

        GameObject newFish = Instantiate(fishManager.GetFishPrefab(level + 1), spawnPos, Quaternion.identity);
        mergeDetect newFishScript = newFish.GetComponent<mergeDetect>();
        newFishScript.fishManager = fishManager;
        newFishScript.level = level + 1;
        newFishScript.attackFish = fishManager.GetAttackFishPrefab(level + 1); // Set attacker for merged fish

        fishManager.AddBaseFish(newFish);
    }
}
