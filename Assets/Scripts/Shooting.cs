using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float cooldown; // Amount of time between shots.
    private float nextShotTime;
    [SerializeField] private int damage;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private GameObject hitParticleEffect;
    [SerializeField] private GameObject shootParticleEffect;
    private Color hitColor;


    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
            {
                CheckObjectHit(hit.collider, hit.point);
            }

            Vector3 direction = hit.point - shootPoint.position;
            Quaternion rot = Quaternion.LookRotation(direction);

            GameObject effect = Instantiate(shootParticleEffect, shootPoint.position, rot);
            Destroy(effect, 2.5f);

            nextShotTime = Time.time + cooldown;
        }
    }

    private void CheckObjectHit(Collider other, Vector3 hitpoint)
    {
        IDamageable damageableObject = other.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeDamage(damage);
            SpawnParticleSystem(hitpoint);
        }
        else
        {
            SpawnParticleSystem(hitpoint);
        }
    }

    private void SpawnParticleSystem(Vector3 spawnPos)
    {
        GameObject effect = Instantiate(hitParticleEffect, spawnPos, Quaternion.identity);
        Destroy(effect, 2.5f);
    }
}
