using UnityEngine;
using UnityEngine.UI;

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
    public float health = 1000f;
    public float waveIncrementHealth = 500 * 1.2f;
    public float damageMultiplier = 1.2f;
    public int damage = 1;
    public GameObject healthBarPrefab; // Assign this in the Inspector
    private GameObject healthBarInstance;
    private Slider healthSlider;

    private float lastAttackTime;
    private float lastFireTime;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Instantiate the health bar prefab and set it as a child of the boss
        healthBarInstance = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
        healthBarInstance.transform.SetParent(transform, false);
        healthSlider = healthBarInstance.GetComponentInChildren<Slider>();

        // Set the max value of the health slider
        if (healthSlider != null)
        {
            healthSlider.maxValue = health;
            healthSlider.value = health;
        }

        // Adjust the position of the health bar relative to the boss
        RectTransform healthBarRect = healthBarInstance.GetComponent<RectTransform>();
        healthBarRect.anchoredPosition = new Vector2(0, 1.5f); // Adjust this value to place it above the boss
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

        // Update the health bar position above the boss
        if (healthBarInstance != null)
        {
            healthBarInstance.transform.position = transform.position + new Vector3(0, 2f, 0); // Adjust Y offset if needed
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
        damageMultiplier += waveNumber * 2f;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (healthSlider != null)
        {
            healthSlider.value = Mathf.Clamp(health, 0, healthSlider.maxValue);
        }

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
