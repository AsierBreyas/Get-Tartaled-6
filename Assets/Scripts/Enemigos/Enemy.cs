using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private Transform player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] float currentHealth, maxHealth;
    [SerializeField] EnemyHealthBar healthBar;

    // Patroling
    [SerializeField] Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] float walkPointRange;

    // Attacking
    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    [SerializeField] float sightRange, attackRange;
    [SerializeField] bool playerInSightRange, playerInAttackRange;

    // Particles for each enemy, in case of the pig, fire, in most cases, blood
    [SerializeField] ParticleSystem enemyParticles;
    [SerializeField] Horda horda;
    bool dead;
    [SerializeField] bool isEdible;

    //Animator
    [SerializeField] Animator animator;

    private void Awake()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con el tag 'Player' en la escena.");
        }
        agent = GetComponent<NavMeshAgent>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    private void Start()
    {
        float scaleFactor = transform.localScale.x;
        sightRange *= scaleFactor;
        attackRange *= scaleFactor;
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (!dead)
        {
            // Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
            
    }

    void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            animator.SetBool("isWalking", true);
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

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
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
            animator.SetBool("isWalking", true);
        }
    }

    void AttackPlayer()
    {
        animator.SetBool("isWalking", false);
        // Make sure enemy dosen't move
        if (agent.enabled)
        {
            agent.SetDestination(transform.position);
        }

        // Ignoramos el eje y para que el cerdo mire recto a Tartalo y no gire ligeramente hacia arriba
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

        if (Vector3.Distance(transform.forward, (targetPosition - transform.position).normalized) > 0.01f)
        {
            transform.LookAt(targetPosition);
        }

        if (!alreadyAttacked)
        {
            if (this.tag == "Lobo")
            {
                StartCoroutine(PerformDashAttack());
                if (enemyParticles != null)
                {
                    enemyParticles.Play();
                }
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
            else if (this.tag == "Cerdo")
            {
                if (enemyParticles != null)
                {
                    enemyParticles.Play();
                }
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
        if (!dead)
        {
            currentHealth -= damage;
            //healthBar.UpdateHealthbar(currentHealth, maxHealth);
            if (currentHealth <= 0)
            {
                this.gameObject.transform.Rotate(new Vector3(0, 0, 90));

                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.useGravity = false;
                }
                Debug.Log("enemigo muerto y supuestamente tumbado");
                dead = true;
                agent.enabled = false;
                animator.SetBool("isWalking", false);

                //Desactivar todos los colliders menos el trigger de comer, para que el cadaver no nos siga haciendo daño
                Collider[] colliders = GetComponents<Collider>();
                foreach (Collider col in colliders)
                {
                    if (!col.isTrigger)
                    {
                        col.enabled = false;
                    }
                }
                horda.EnemigoMuerto();
                // Animacion de enemigo muriendo
                if (!isEdible)
                {
                    Invoke(nameof(DestroyEnemy), 0.5f);
                }
                else
                {
                    FindAnyObjectByType<ControlesTartalo>().AparecioComestible();
                }
            }
        }
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    // Corrutina para la embestida del lobo
    IEnumerator PerformDashAttack()
    {
        if (!dead)
        {
            agent.enabled = false; // Desactiva el NavMeshAgent para moverse libremente

            Vector3 dashDirection = (player.position - transform.position).normalized;
            float dashSpeed = 70f;
            float dashDuration = 0.3f;
            float elapsedTime = 0f;

            while (elapsedTime < dashDuration)
            {
                transform.position += dashDirection * dashSpeed * Time.deltaTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Retroceso tras el ataque
            elapsedTime = 0f;
            Vector3 backwardDirection = -dashDirection;
            float backwardSpeed = 60f;
            float backwardDuration = 0.2f;

            while (elapsedTime < backwardDuration)
            {
                transform.position += backwardDirection * backwardSpeed * Time.deltaTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Reactiva el NavMeshAgent con un pequeño delay
            agent.enabled = true;
            yield return new WaitForSeconds(0.1f); // Espera un instante para evitar bugs

            if (agent.enabled && Vector3.Distance(transform.position, player.position) <= sightRange)
            {
                ChasePlayer();
            }
        }
    }

    public void BeEat()
    {
        if (dead && isEdible)
            Invoke(nameof(DestroyEnemy), 0.5f);
    }
    public bool GetIsEsdible()
    {
        return isEdible;
    }
    public bool IsDead()
    {
        return dead;
    }
}
