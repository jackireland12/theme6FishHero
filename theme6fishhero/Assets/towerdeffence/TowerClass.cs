using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerClass : MonoBehaviour
{
    public GameObject target;
    private SpawnerClass ec;
    public float attackSpeed = 1.5f;
    private Coroutine shootingCoroutine;
    public ParticleSystem shootEffcet;
    private void Start()
    {
        ec = FindObjectOfType<SpawnerClass>();
        
    }
    private void Update()
    {
        if (ec.enemies != null)
        {
            findClosestEnemy();
            
        }
        else { 
            target = null;
            //Debug.Log("target null");
        }
    }

     
    void findClosestEnemy()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy= null;
        foreach( GameObject enemy in ec.enemies )
        {
            float distance = Vector3.Distance(transform.position,enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }
        if (closestEnemy != target)
        {
            target = closestEnemy;
            if (shootingCoroutine  != null)
            {
                StopCoroutine(shootingCoroutine);
                shootingCoroutine = null;
            }
            if(target != null)
            { 
                shootingCoroutine = StartCoroutine(shootDelay());
                Debug.Log( "co start");
            }

        }
        target = closestEnemy;
        //Debug.Log(target);
    }

    void shoot()
    {
        if (target != null)
        {
            enemieClass enemyHealth = target.GetComponent<enemieClass>();
            if (enemyHealth != null)
            {
                enemyHealth.Damage(10); //  Damage enemy instead of instant kill
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
            shoot();
            yield return new WaitForSeconds(attackSpeed);

            if (target == null) // Enemy died? Find new one!
            {
                findClosestEnemy();
            }
        }

        shootingCoroutine = null; // Reset coroutine

    }
}
