using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : enemieClass
{
    public int fastCount;
    public override void Damage(int damage)
    {
        Debug.Log("attack");
        HP -= damage;
        if (HP > 0)
        {
            damageEffect.Play();
        }
        if (HP <= 0)
        {
            deathEffect.Play();
            CM.enemyDefeated("fast");
            Destroy(gameObject);
        }
        //int fastCount = CM.enemyCollection["fast"];
        //Debug.Log("fast enimey"+fastCount);
    }
}
