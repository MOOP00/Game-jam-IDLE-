using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float speed = 2f;
    public float health = 30f;
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
    }

    protected void MoveTowardsPlayer()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    public void IncreaseDifficulty(int waveNumber)
    {
        //float additionalHealth = waveIncrementHealth * EnemySpawner.Instance.waveNumber;
        //health += additionalHealth; // Remove cast to int for float values
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
        //FindObjectOfType<EnemySpawner>().EnemyDefeated();
    }
}
