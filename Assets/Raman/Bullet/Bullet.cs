using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Speed of the bullet
    public float damage = 10f; // Damage dealt by the bullet
    public float lifetime = 5f; // Lifetime of the bullet in seconds

    private Vector2 moveDirection;

    private void Start()
    {
        // Destroy the bullet after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if it hits a Boss
        Boss boss = collision.GetComponent<Boss>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
            Debug.Log("Bullet hit Boss for " + damage + " damage!");
            Destroy(gameObject);
            return;
        }

        // Check if it hits an EnemyBase
        EnemyBase enemy = collision.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log("Bullet hit Enemy for " + damage + " damage!");
            Destroy(gameObject);
            return;
        }

        // Destroy the bullet if it hits an object with the "Enemy" or "Boss" tag
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
