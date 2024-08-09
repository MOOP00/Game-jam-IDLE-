using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float speed = 5f;  // ความเร็วของกระสุน
    public float rotateSpeed = 200f;  // ความเร็วในการหมุนเพื่อติดตามศัตรู
    private Transform target;  // เป้าหมายที่กระสุนจะติดตาม

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
        {
            // หากเป้าหมายถูกทำลาย กระสุนจะถูกทำลาย
            Destroy(gameObject);
            return;
        }

        // คำนวณทิศทางไปยังเป้าหมาย
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        // คำนวณการหมุนของกระสุนเพื่อติดตามศัตรู
        float rotateAmount = Vector3.Cross(direction, transform.right).z;
        transform.Rotate(Vector3.forward * -rotateAmount * rotateSpeed * Time.deltaTime);

        // เคลื่อนที่ไปข้างหน้าในทิศทางที่ถูกต้อง
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    // ฟังก์ชันตรวจจับการชน
    void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่ากระสุนชนกับศัตรู
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);  // ทำลายศัตรู
            Destroy(gameObject);  // ทำลายกระสุน
        }
    }
}