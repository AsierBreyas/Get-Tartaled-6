using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Tartxalo : MonoBehaviour
{
    private Transform player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] float maxHealth;
    float currentHealth;
    [SerializeField] EnemyHealthBar healthBar;
    // Attacking
    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    [SerializeField] float sightRange, attackRange;
    [SerializeField] bool playerInSightRange, playerInAttackRange;

    // Particles for each enemy, in case of the pig, fire, in most cases, blood
    [SerializeField] ArmaEnemigo arma;

    //Animator
    [SerializeField] Animator animator;
    bool dead;
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

            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
    }

    void ChasePlayer()
    {
        // Animation of walk

        if (agent.enabled)
        {
            agent.SetDestination(player.position);
            animator.SetBool("IsWalking", true);
        }
    }

    void AttackPlayer()
    {
        animator.SetBool("IsWalking", false);
        // Make sure enemy dosen't move
        if (agent.enabled)
        {
            agent.SetDestination(transform.position);
        }

        // Ignoramos el eje y para que el cerdo mire recto a Tartalo y no gire ligeramente hacia arriba
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPosition);

        if (!alreadyAttacked)
        {
            animator.SetTrigger("Attack");
            arma.EmpeceElAtaque();
            Debug.Log("Soy malo");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    void AttackFinished()
    {
        arma.TermineElAtaque();
    }
    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        if (!dead)
        {
            Debug.Log("Ay me hiciste daño");
            currentHealth -= damage;
            //healthBar.UpdateHealthbar(currentHealth, maxHealth);
            if (currentHealth <= 0)
            {
                dead = true;
                animator.SetTrigger("dead");
            }
        }
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
