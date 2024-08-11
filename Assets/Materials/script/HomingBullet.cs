using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float speed = 5f;  // ความเร็วของกระสุน
    public float rotateSpeed = 200f;  // ความเร็วในการหมุนเพื่อติดตามศัตรู
    public int damage = 25;  // ความเสียหายที่กระสุนสามารถทำได้
    private Transform target;  // เป้าหมายที่กระสุนจะติดตาม

    // ฟังก์ชันนี้ใช้ในการกำหนดเป้าหมายของกระสุน
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        // ถ้าไม่มีเป้าหมายให้ทำลายกระสุน
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // คำนวณทิศทางไปยังเป้าหมาย
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        // คำนวณมุมหมุนที่จะไปหาเป้าหมาย
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // หมุนกระสุนไปยังเป้าหมายโดยใช้ Quaternion.RotateTowards
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        // เคลื่อนที่ไปตรงยังเป้าหมาย
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    // ฟังก์ชันที่เรียกใช้เมื่อกระสุนชนกับสิ่งอื่น
    void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่ากระสุนชนกับศัตรู
        if (collision.CompareTag("Enemy"))
        {
            // ดึงข้อมูล EnemyHealth component ของศัตรู
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

            // ถ้าศัตรูมี EnemyHealth component ให้ทำดาเมจ
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);  // เรียกใช้ฟังก์ชัน TakeDamage ของศัตรู
            }

            // ทำลายกระสุนหลังจากทำดาเมจ
            Destroy(gameObject);
        }
    }
}