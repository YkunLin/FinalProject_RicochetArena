using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class Launcher : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject projectilePrefab;
    public float velocityMult = 10f;

    [Header("Dynamic")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode = false;

    void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        if(launchPointTrans != null)
        {
            launchPoint = launchPointTrans.gameObject;
            launchPos = launchPointTrans.position;
        }
        else
        {
            Debug.LogError("LaunchPoint child not found!");
        }
    }

    void OnMouseDown()
    {
        if(RicochetGameManager.S != null && RicochetGameManager.S.levelEnded) return;

        aimingMode = true;
        projectile = Instantiate(projectilePrefab);
        projectile.transform.position = launchPos;

        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        if (!aimingMode) return;

        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        mousePos3D.z = 0;

        Vector3 mouseDelta = mousePos3D - launchPos;

        float maxMagnitude = GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 projPos = launchPos + mouseDelta;
        projPos.z = 0;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;

            Rigidbody projRB = projectile.GetComponent<Rigidbody>();
            projRB.isKinematic = false;
            projRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
            projRB.velocity = -mouseDelta * velocityMult;

            Projectile projScript = projectile.GetComponent<Projectile>();
            projScript.hasBeenShot = true;

            if(RicochetGameManager.S != null)
            {
                RicochetGameManager.S.ShotFired();
            }

            projectile = null;
        }
    }
}
