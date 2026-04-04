using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int enemyCount = 0;

    void Start()
    {
        enemyCount++;
    }

    void OnCollisionEnter(Collision coll)
    {
        Projectile p = coll.gameObject.GetComponent<Projectile>();
        if(p != null)
        {
            enemyCount--;
            Destroy(gameObject);
        }
    }
}
