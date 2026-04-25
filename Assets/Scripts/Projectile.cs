using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public static int projectileCount = 0;
    private AudioSource audioSource;
    private Rigidbody rb;
    private float stillTime = 0f;
    
    [Header("Dynamic")]
    public bool hasBeenShot = false;
    public float gravity = -9.8f;

    [Header("Bounce Limit")]
    public int maxBounces = 8;
    private int bounceCount = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        projectileCount++;
        audioSource = GetComponent<AudioSource>();
    }
    

    void FixedUpdate()
    {
        if (hasBeenShot)
        {
            //control projectile's gravity
            rb.velocity += new Vector3(0, gravity * Time.fixedDeltaTime, 0);
        }

        //Keep the bullet on the XY plane at all times
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        if (!hasBeenShot) return;


        if(rb.velocity.magnitude < 0.5f)
        {
            stillTime += Time.fixedDeltaTime;

            if (stillTime > 1f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            stillTime = 0f;
        }     
    }

    void OnCollisionEnter(Collision coll)
    {
        if(audioSource != null)
        {
            audioSource.Play();
        }
        
        if(coll.gameObject.GetComponent<Enemy>() != null)
        {
            return;
        }
        bounceCount++;

        if (bounceCount >= maxBounces)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        projectileCount--;
    }
}
