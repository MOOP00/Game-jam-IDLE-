using UnityEngine;

public class bulletscript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float bulletSpeed = 20f;  // ความเร็วของกระสุน
    public float damage = 200;  // ความเสียหายที่กระสุนสามารถทำได้

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;  // ใช้ bulletSpeed เพื่อปรับความเร็วกระสุน
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemys enemyHealth = collision.GetComponent<Enemys>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
        if (collision.CompareTag("Boss"))
        {
            Boss boss = collision.GetComponent<Boss>();

            if (boss != null)
            {
                boss.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
