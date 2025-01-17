using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] float currentHealth, maxHealth;
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

    // Particles for each enemy, in case of the pig, fire, in most cases, blood
    [SerializeField] ParticleSystem enemyParticles;
    [SerializeField] Horda horda;

    private void Awake()
    {
        player = GameObject.Find("Tartalo").transform;
        agent = GetComponent<NavMeshAgent>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    private void Start()
    {
        //health = maxHealth;
        //healthBar.UpdateHealthBar(health, maxHealth);
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

        if (agent.enabled)
        {
            agent.SetDestination(player.position);
        }
    }

    void AttackPlayer()
    {
        // Make sure enemy dosen't move
        if (agent.enabled)
        {
            agent.SetDestination(transform.position);
        }

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            if (this.tag  == "Lobo")
            {
                StartCoroutine(PerformDashAttack());
                enemyParticles.Play();
                //Debug.Log("Soy un lobo, pum te ataco");
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
            else if (this.tag == "Cerdo")
            {
                enemyParticles.Play();

                //Debug.Log("Soy un cerdo, te escupo fuego!");

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Ay me hiciste daño");
        currentHealth -= damage;
        //healthBar.UpdateHealthbar(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
           if (horda != null)
              horda.EnemigoMuerto();
           // Animacion de enemigo muriendo
           Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    // Corrutina para la embestida del lobo
    IEnumerator PerformDashAttack()
    {
        // Desactivar temporalmente el NavMeshAgent
        agent.enabled = false;

        // Variables de la embestida
        float dashSpeed = 7f; // Velocidad de la embestida
        float dashDuration = 0.3f; // Duración de la embestida
        float backwardSpeed = 5f; // Velocidad del retroceso
        float backwardDuration = 0.2f; // Duración del retroceso

        Vector3 dashDirection = (player.position - transform.position).normalized;

        // Embestida hacia el jugador
        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Retroceso
        elapsedTime = 0f;
        Vector3 backwardDirection = -dashDirection;
        while (elapsedTime < backwardDuration)
        {
            transform.position += backwardDirection * backwardSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reactivar el NavMeshAgent
        agent.enabled = true;
    }
}
