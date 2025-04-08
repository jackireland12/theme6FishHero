using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heaveyEnemie : enemieClass
{
    public int heaveyCount;



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
            CM.enemyDefeated("heavy");
            Destroy(gameObject);
        }
        //int heaveyCount = CM.enemyCollection["heavy"];
        //Debug.Log("heavy en"+heaveyCount);
    }
}
