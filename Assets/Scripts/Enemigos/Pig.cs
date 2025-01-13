using UnityEngine;
using UnityEngine.UI;

public class Pig : MonoBehaviour
{
    Rigidbody rb;
    // Variables de ataque
    [SerializeField] Transform targetPlayer;
    [SerializeField] float visionRadius = 10f;
    [SerializeField]  float chaseRadius = 15f;
    [SerializeField]  float movementSpeed = 3f;
    [SerializeField]  float stopDistance = 1f;
    [SerializeField] float fireSpitDistance = 3f; // Distancia para intentar escupir fuego
    [SerializeField] ParticleSystem fireParticles; // Sistema de partículas para el fuego
    [SerializeField] float fireSpitProbability = 0.7f; // Probabilidad de escupir fuego (70%)
    [SerializeField] float timeStopWhenFlame = 3f; // Tiempo parado escupiendo fuego
    [SerializeField] float fireCooldown = 5f;
    float nextFireTime = 0f; // Momento en el que puede volver a escupir fuego

    bool isChasing = false;
    private bool isSpittingFire = false;

    // Variables vida
    [SerializeField] float health, maxHealth = 3f;
    [SerializeField] EnemyHealthBar healthBar;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    private void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);

        if (distanceToPlayer <= visionRadius)
        {
            // El jugador está en el radio de visión: empieza a seguirlo
            isChasing = true;
        }
        else if (distanceToPlayer > chaseRadius)
        {
            // El jugador está fuera del rango de persecución: deja de seguirlo
            isChasing = false;
        }

        if (isChasing && distanceToPlayer > stopDistance && !isSpittingFire)
        {
            FollowPlayer();
        }

        // Intentar escupir fuego si está lo suficientemente cerca
        if (distanceToPlayer <= fireSpitDistance && !isSpittingFire && Time.time >= nextFireTime)
        {
            TrySpitFire();
        }

        void TrySpitFire()
        {
            float randomChance = Random.Range(0f, 1f);
            if (randomChance <= fireSpitProbability)
            {
                // Detener el movimiento y escupir fuego
                isSpittingFire = true;
                StartCoroutine(SpitFire());
            }
        }

        System.Collections.IEnumerator SpitFire()
        {
            // Detener el movimiento mientras escupe fuego
            isChasing = false;
            
            // Activa las partículas de fuego
            fireParticles.Play();

            // Establecer el cooldown
            nextFireTime = Time.time + fireCooldown;

            // Espera mientras escupe fuego (por ejemplo, 2 segundos)
            yield return new WaitForSeconds(timeStopWhenFlame);

            // Detiene las partículas y permite volver a moverse
            fireParticles.Stop();
            isSpittingFire = false;
            isChasing = true;
        }
    }

    void FollowPlayer()
    {
        // Mueve al cerdo hacia el jugador
        Vector3 direction = (targetPlayer.position - transform.position).normalized;
        transform.position += direction * movementSpeed * Time.deltaTime;

        // Opcional: Orienta al cerdo hacia el jugador
        transform.LookAt(new Vector3(targetPlayer.position.x, transform.position.y, targetPlayer.position.z));
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("PUM quemao");
        }
    }

    void Die()
    {
        // Animacion de morir aqui
        Destroy(gameObject);
    }
}
