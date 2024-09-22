using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float attackCooldown = 2f;
    public int attackDamage = 10;
    public LayerMask targetLayer;

    private float attackCooldownTimer;
    private Animator animator;
    private bool isAttacking;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //Handle attack cooldown
        attackCooldownTimer -= Time.deltaTime;
    }

    public void TriggerAttack()
    {
        if (attackCooldownTimer <= 0)
        {
            animator.SetBool("isWalking", false);
            animator.SetTrigger("meleeAttack");
            attackCooldownTimer = attackCooldown; // Reset cooldown after attack
            isAttacking = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the attack is active and the collided object is on the target layer
        if (isAttacking && ((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            Health targetHealth = other.GetComponent<Health>();

            // Check the parent if not found on the child
            if (targetHealth == null && other.transform.parent != null)
            {
                targetHealth = other.GetComponentInParent<Health>();
            }

            Debug.Log(targetHealth);

            if (targetHealth != null)
            {
                targetHealth.TakeDamage(attackDamage);
            }
        }
        isAttacking = false;
    }
    public void EndAttack()
    {
        isAttacking = false;
        Debug.Log("Melee Attack Ended");
    }
}
