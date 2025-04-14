using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerClass : MonoBehaviour
{
    public GameObject target;
    private fishManager ec;
    public float attackSpeed = 1.5f;
    private Coroutine shootingCoroutine;
    public ParticleSystem shootEffcet;
    public int damage = 10;
    private void Start()
    {
        ec = FindObjectOfType<fishManager>();
        
    }
    private void Update()
    {
        if (ec.activeFish != null && ec.activeFish.Count > 0)
        {
            if (target == null || !target.activeSelf) // Check if target is dead or null
            {
                findClosestEnemy();
            }
        }
        else
        {
            target = null;
            if (shootingCoroutine != null)
            {
                StopCoroutine(shootingCoroutine);
                shootingCoroutine = null;
            }
        }
    }

     
    void findClosestEnemy()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in ec.activeFish)
        {
            if (enemy == null || !enemy.activeSelf) continue; // Skip dead or null enemies
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        target = closestEnemy;

        // Manage coroutine
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }

        if (target != null)
        {
            shootingCoroutine = StartCoroutine(shootDelay());
            Debug.Log("New target acquired: " + target.name);
        }
    }

    void shoot()
    {
        if (target != null)
        {
            enemieClass enemyHealth = target.GetComponent<enemieClass>();
            if (enemyHealth != null)
            {
                enemyHealth.Damage(damage); //  Damage enemy instead of instant kill
                Debug.Log(target + "has damaged");
                if (shootEffcet != null)
                {
                    shootEffcet.Play();
                }
            }
            else
            {
                Debug.LogWarning("No enemieClass script found on " + target.name);
            }

        }
    }
    IEnumerator shootDelay()
    {
        while (target != null)
        {
            
            yield return new WaitForSeconds(attackSpeed);
            shoot();

            if (target == null) // Enemy died? Find new one!
            {
                findClosestEnemy();
                Debug.Log("new en");
            }

        }

        shootingCoroutine = null; // Reset coroutine

    }
}
