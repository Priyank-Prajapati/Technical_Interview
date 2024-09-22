using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent enemyAgent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private MeleeAttack meleeAttack;
    private ProjectileAttack projectileAttack;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        enemyAgent = GetComponent<NavMeshAgent>();
        meleeAttack = GetComponent<MeleeAttack>();
        projectileAttack = GetComponent<ProjectileAttack>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //insight or attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
            Patroling();
        if(playerInSightRange && !playerInAttackRange)
            FireProjectileAttack();
        if (playerInSightRange && playerInAttackRange)
        {
            MeleeAttack();
            //if (!playerInSightRange)
            //    Chasing();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) 
            SearchWalkPoint();

        if (walkPointSet)
        {
            animator.SetBool("isWalking", true);
            enemyAgent.SetDestination(walkPoint);
        }

        if ((transform.position - walkPoint).magnitude < 0.1f)
            walkPointSet = false;   
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    //private void Chasing()
    //{
    //    enemyAgent.SetDestination(player.position);
    //}
    private void FireProjectileAttack()
    {
        animator.SetBool("isWalking", false);
        animator.SetTrigger("projectileAttack");

        enemyAgent.SetDestination(transform.position);
        transform.LookAt(player);
        projectileAttack.canAttack = true;
    }

    private void MeleeAttack()
    {
        animator.SetBool("isWalking", false);
        animator.SetTrigger("meleeAttack");

        enemyAgent.SetDestination(transform.position);
        transform.LookAt(player);
        //meleeAttack.canAttack = true;
        meleeAttack.TriggerAttack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        
    }
}
