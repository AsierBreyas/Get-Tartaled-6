using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] float health, maxHealth;
    [SerializeField] EnemyHealthBar healthBar;

    // Patroling
    [SerializeField] Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField]  float walkPointRange;

    // Attacking
    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    [SerializeField] float sightRange, attackRange;
    [SerializeField] bool playerInSightRange, playerInAttackRange;

    // Cerdo
    [SerializeField] ParticleSystem fireParticles;

    private void Awake()
    {
        player = GameObject.Find("Tartalo").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    void ChasePlayer()
    {
        // Animation of walk

        agent.SetDestination(player.position);
    }

    void AttackPlayer()
    {
        // Make sure enemy dosen't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            if (this.tag  == "Wolve")
            {
                // Attack code here
                Debug.Log("Soy un lobo, pum te ataco");
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
            else if (this.tag == "Pig")
            {
                // Play fire particles
                fireParticles.Play();

                Debug.Log("Soy un cerdo, te escupo fuego!");

                // Cooldown for the attack
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // Animation of enemy dying
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
