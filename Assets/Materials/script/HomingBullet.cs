using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 200f;
    public int damage = 25;
    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);

                if (enemyHealth.GetCurrentHealth() <= 0) // ใช้ GetCurrentHealth() แทนการเข้าถึง currentHealth โดยตรง
                {
                    // อาจมีการทำอะไรเพิ่มเติมเมื่อศัตรูถูกทำลาย
                }
            }

            Destroy(gameObject);
        }
    }
}