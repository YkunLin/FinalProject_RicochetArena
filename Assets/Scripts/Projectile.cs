using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public static int projectileCount = 0;
    private Rigidbody rb;
    private float stillTime = 0f;
    
    [Header("Dynamic")]
    public bool hasBeenShot = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        projectileCount++;
    }
    // void Start()
    // {
    //     //Prevent bullets from remaining in the scene for too long
    //     // Destroy(gameObject, 15f);
    // }

    void FixedUpdate()
    {
        //Keep the bullet on the XY plane at all times
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        if (!hasBeenShot) return;


        if(rb.velocity.magnitude < 0.3f)
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

    void OnDestroy()
    {
        projectileCount--;
    }
}
