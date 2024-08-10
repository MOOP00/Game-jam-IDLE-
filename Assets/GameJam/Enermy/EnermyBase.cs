using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float speed = 2f;
    public float health = 30f;
    public float despawnDistance = 13f;
    public float waveIncrementHealth;
    public float damageMultiplier = 1.2f;
    public int damage = 1;

    protected Transform player;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        waveIncrementHealth = health * 1.125f;

    }

    protected virtual void Update()
    {
        MoveTowardsPlayer();
        CheckDespawn();
    }

    protected void MoveTowardsPlayer()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    protected void CheckDespawn()
    {
        if (player == null) return;

        if (Vector3.Distance(transform.position, player.position) > despawnDistance)
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseDifficulty(int waveNumber)
    {
        float additionalHealth = waveIncrementHealth * waveNumber;
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
    }
}
