using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    public Transform player; // Reference to the player's position
    public float moveSpeed = 5f; // Speed of the monster
    public float attackRange = 1.5f; // Range within which the monster can attack
    public float attackCooldown = 2f; // Cooldown time between attacks
    public float shootingRange = 10f; // Range within which the mini boss can shoot the player
    public GameObject bulletPrefab; // Bullet prefab for shooting
    public float bulletSpeed = 10f; // Speed of the bullets
    public float bulletLifetime = 3f; // Lifetime of the bullets before they are destroyed
    public float fireRate = 1f; // Time between shots in seconds
    public int health = 100; // Mini boss health
    public int waveIncrementHealth = 50; // Health increment per wave
    public int pelletsPerShot = 5; // Number of pellets per shotgun shot
    public float spreadAngle = 30f; // Total spread angle of the shotgun

    private float lastAttackTime;
    private float lastFireTime;
    private bool isInShootingRange = false;

    void Start()
    {
        // Find the player object in the scene by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            // Move towards the player if outside attack range
            MoveTowardsPlayer();
        }
        else if (Time.time >= lastAttackTime + attackCooldown)
        {
            // Attack the player if within range and cooldown has elapsed
            AttackPlayer();
            lastAttackTime = Time.time; // Update the last attack time
        }

        // Check if the player is in shooting range
        if (distanceToPlayer <= shootingRange && Time.time >= lastFireTime + fireRate)
        {
            isInShootingRange = true;
            ShootPlayer();
            lastFireTime = Time.time; // Update the last fire time
        }
        else
        {
            isInShootingRange = false;
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
        Debug.Log("MiniBoss attacks the player!");
    }

    void ShootPlayer()
    {
        if (bulletPrefab != null && isInShootingRange)
        {
            Vector2 baseDirection = (player.position - transform.position).normalized;
            float baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg;
            float angleStep = spreadAngle / (pelletsPerShot - 1);
            float startAngle = baseAngle - spreadAngle / 2;

            for (int i = 0; i < pelletsPerShot; i++)
            {
                float currentAngle = startAngle + (angleStep * i);
                float radianAngle = currentAngle * Mathf.Deg2Rad;

                Vector2 direction = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));

                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.linearVelocity = direction * bulletSpeed;

                // Destroy the bullet after a certain lifetime
                Destroy(bullet, bulletLifetime);
            }
        }
    }

    public void IncreaseDifficulty(int waveNumber)
    {
        health += waveIncrementHealth * waveNumber;
    }
}
