using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemieClass : MonoBehaviour
{
    public float HP;
    public float speed;
    public float damage;
    public ParticleSystem damageEffect;
    public ParticleSystem deathEffect;
    public int normalCount;
    public CollectionManager CM;
    public int unitLevel;
    private fishManager fishManager;

    private Rigidbody2D rb2;

    public virtual void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        CM = FindObjectOfType<CollectionManager>();
        rb2.AddForce(-transform.right * speed);
        fishManager = FindObjectOfType<fishManager>();
    }

    public virtual void Damage(int damage)
    {
        //Debug.Log("attack");
        HP -= damage;
        if (HP > 0)
        {
            damageEffect.Play();
        }
        if (HP <=        0) {
            deathEffect.Play();
            if (fishManager != null)
            {
                fishManager.RemoveActiveFish(gameObject);
            }
            CM.enemyDefeated("normal");
            Destroy(gameObject);
        }
       //int normalCount= CM.enemyCollection["normal"];
       // Debug.Log("normal en"+normalCount);
    }

}
