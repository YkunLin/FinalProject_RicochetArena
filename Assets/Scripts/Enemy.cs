using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int enemyCount = 0;
    public AudioClip hitSound;

    void Start()
    {
        enemyCount++;
    }

    void OnCollisionEnter(Collision coll)
    {
        Projectile p = coll.gameObject.GetComponent<Projectile>();
        if(p != null)
        {
            if(hitSound != null)
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position,10f);
            }

            enemyCount--;
            Destroy(gameObject);
        }
    }
}
