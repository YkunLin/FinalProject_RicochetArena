using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int enemyCount = 0;
    public AudioClip hitSound;
    public GameObject hitEffect;

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
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
            }
            if(hitEffect!= null)
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }

            enemyCount--;
            Destroy(gameObject);
        }
    }
}
