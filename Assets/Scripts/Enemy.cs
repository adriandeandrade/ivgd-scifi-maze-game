using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform targetPos;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private float health;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float shootDistance;
    [SerializeField] private bool hasTarget;

    [SerializeField] private float cooldown; // Amount of time between shots.
    [SerializeField] private int damage;
    private float nextShotTime;

    [SerializeField] private GameObject playerHitParticle;
    [SerializeField] private GameObject wallHitParticle;
    [SerializeField] private GameObject shootParticle;


    private void Start()
    {
        targetPos = FindObjectOfType<PlayerController>().transform;
    }

    public void TakeDamage(float damage)
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            health -= damage;
        }
    }

    private void Update()
    {
        if (targetPos != null)
        {
            float distance = Vector3.Distance(transform.position, targetPos.position);
            Vector3 direction = (targetPos.position - transform.position).normalized;
            if (distance <= shootDistance && Time.time > nextShotTime)
            {
                // Shoot
                Shoot();
            }
            else
            {
                transform.LookAt(targetPos);
                transform.localPosition += transform.forward * moveSpeed * Time.deltaTime;
            }
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, transform.forward, out hit))
        {
            CheckObjectHit(hit.collider, hit.point);
            print("shot");
        }

        Vector3 direction = hit.point - shootPoint.position;
        Quaternion rot = Quaternion.LookRotation(direction);

        GameObject effect = Instantiate(shootParticle, shootPoint.position, rot);
        Destroy(effect, 2.5f);

        nextShotTime = Time.time + cooldown;
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
        GameObject effect = Instantiate(playerHitParticle, spawnPos, Quaternion.identity);
        Destroy(effect, 2.5f);
    }
}
