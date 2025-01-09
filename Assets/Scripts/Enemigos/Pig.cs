using UnityEngine;

public class Pig : MonoBehaviour
{
    [SerializeField] Transform targetPlayer;
    float visionRadius = 10f;
    float chaseRadius = 15f;
    float movementSpeed = 3f;
    bool isChasing = false;

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

        if (isChasing)
        {
            FollowPlayer();
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
}
