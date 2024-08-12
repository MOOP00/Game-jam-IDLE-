using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float speed = 2f;
    public float health = 30f;
    public float waveIncrementHealth;
    public float damageMultiplier = 1.2f;
    public int damage = 2;
    public float attackRange = 2f; // ระยะโจมตี
    public float attackCooldown = 1f; // เวลาระหว่างการโจมตีแต่ละครั้ง

    protected Transform player;
    private float lastAttackTime; // เวลาโจมตีล่าสุด

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("_player").transform;
        waveIncrementHealth = health * 1.125f;
    }

    protected virtual void Update()
    {
        MoveTowardsPlayer();

        // โจมตีผู้เล่นเมื่ออยู่ในระยะโจมตี
        if (Vector2.Distance(transform.position, player.position) <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("_player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage * damageMultiplier); // Use the existing damage variable
            }
        }
    }


    protected void MoveTowardsPlayer()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    protected virtual void AttackPlayer()
    {
        // เพิ่มโค้ดสำหรับการทำดาเมจใส่ผู้เล่นที่นี่
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>(); // Assuming you have a PlayerHealth script on the player
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage * damageMultiplier);
        }
    }

    public void IncreaseDifficulty(int waveNumber)
    {
        float additionalHealth = health * 1.2f * waveNumber;
        health += (int)additionalHealth;
        damageMultiplier += waveNumber * 2f;
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
        EnemySpawner.Instance.EnemyDefeated();
    }
}