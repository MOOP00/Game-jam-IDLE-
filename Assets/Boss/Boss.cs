using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 1f;
    public float attackRange = 1f;
    public float attackCooldown = 3f;
    public float shootingRange = 60f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 5f;
    public float fireRate = 3f; 
    public int health = 1000;
    public float waveIncrementHealth = 500 * 1.2f;
    public float damageMultiplier = 1.5f;
    public int damage = 10;

    private float lastAttackTime;
    private float lastFireTime;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
        }
        else if (Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }

        if (distanceToPlayer <= shootingRange && Time.time >= lastFireTime + fireRate)
        {
            ShootPlayer();
            lastFireTime = Time.time;
        }
    }

    protected void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    protected abstract void AttackPlayer();
    protected abstract void ShootPlayer();

    public void IncreaseDifficulty(int waveNumber)
    {
        float additionalHealth = 500 * 2.5f * waveNumber;
        health += (int)additionalHealth; // Cast after calculation
        damageMultiplier += waveNumber * 5.5f;
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle boss death
        Destroy(gameObject);
        Debug.Log("Boss has been defeated!");
    }


}
