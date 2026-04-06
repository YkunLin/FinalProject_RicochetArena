using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPad : MonoBehaviour
{
    [Header("Bounce Boost Settings")]
    public float speedMultiplier = 1.3f;
    public float minSpeed = 10f;
    public float cooldown = 0.1f;

    private HashSet<Rigidbody> boostedBodies = new HashSet<Rigidbody>();

    private void OnCollisionEnter(Collision coll)
    {
        Projectile p = coll.gameObject.GetComponent<Projectile>();
        if (p == null) return;

        Rigidbody rb = coll.gameObject.GetComponent<Rigidbody>();
        if (rb == null) return;

        if (boostedBodies.Contains(rb)) return;

        StartCoroutine(BoostAfterBounce(rb));
    }

    private IEnumerator BoostAfterBounce(Rigidbody rb)
    {
        boostedBodies.Add(rb);

        yield return new WaitForFixedUpdate();

        if (rb != null)
        {
            Vector3 currentVel = rb.velocity;

            if (currentVel.magnitude > 0.01f)
            {
                Vector3 dir = currentVel.normalized;
                float newSpeed = Mathf.Max(currentVel.magnitude * speedMultiplier, minSpeed);
                rb.velocity = dir * newSpeed;
            }
        }

        yield return new WaitForSeconds(cooldown);

        if (rb != null)
        {
            boostedBodies.Remove(rb);
        }
    }
}
