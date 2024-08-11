using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float speed = 5f;  // ความเร็วของกระสุน
    public float rotateSpeed = 200f;  // ความเร็วในการหมุนเพื่อติดตามศัตรู
    public float damage;  // ความเสียหายที่กระสุนทำได้
    public float lifetime = 7f;  // อายุการใช้งานของกระสุน
    private Transform target;  // เป้าหมายที่กระสุนจะติดตาม

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // ฟังก์ชันในการกำหนดเป้าหมายให้กระสุน
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // ฟังก์ชันในการกำหนดความเสียหายของกระสุน
    public void SetDamage(float bulletDamage)
    {
        damage = bulletDamage;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);  // ลบกระสุนถ้าไม่มีเป้าหมาย
            return;
        }

        // คำนวณทิศทางไปยังเป้าหมาย
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        // คำนวณมุมที่จะหมุนเพื่อไปยังเป้าหมาย
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // หมุนกระสุนไปยังเป้าหมาย
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        // เคลื่อนที่ไปยังเป้าหมาย
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    // ฟังก์ชันที่ถูกเรียกเมื่อกระสุนชนกับวัตถุ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);  // เรียกใช้ TakeDamage บนศัตรู
            }

            Destroy(gameObject);  // ทำลายกระสุนหลังจากที่ทำความเสียหาย
        }
        if (collision.CompareTag("Boss"))
        {
            // จัดการกับการทำความเสียหายต่อบอสที่นี่ (ถ้าจำเป็น)
        }
    }
}
