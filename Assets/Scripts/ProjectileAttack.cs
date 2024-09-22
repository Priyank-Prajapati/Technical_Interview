using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float attackCooldown = 2f;


    private float attackCooldownTimer;
    public bool canAttack;

    void Update()
    {
        // Handle attack cooldown
        attackCooldownTimer -= Time.deltaTime;
        if (canAttack && attackCooldownTimer <= 0)
        {
            FireProjectile();
            attackCooldownTimer = attackCooldown;
            canAttack = false;
        }
    }

    void FireProjectile()
    {
        // Instantiate the projectile and give it an initial velocity
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rbProjectile = projectile.GetComponent<Rigidbody>();
        rbProjectile.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
        rbProjectile.AddForce(transform.up * (projectileSpeed/4), ForceMode.Impulse);
    }
}
