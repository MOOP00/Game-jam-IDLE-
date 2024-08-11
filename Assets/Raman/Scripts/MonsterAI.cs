using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform player; // Reference to the player's position
    public float moveSpeed = 5f; // Speed of the monster
    public float attackRange = 1.5f; // Range within which the monster can attack
    public float attackCooldown = 2f; // Cooldown time between attacks

    private float lastAttackTime;

    void Start()
    {
        // Find the player object in the scene by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Move towards the player if outside attack range
        if (distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            // Attack the player if within range and cooldown has elapsed
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time; // Update the last attack time
            }
        }
    }

    void MoveTowardsPlayer()
    {
        // Calculate the direction towards the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Move the monster in the direction of the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        // Implement your attack logic here
        Debug.Log("Monster attacks the player!");
    }
}

