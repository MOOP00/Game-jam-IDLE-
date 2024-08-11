using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;  // เลือดสูงสุดของศัตรู
    private float currentHealth;  // เลือดปัจจุบันของศัตรู

    void Start()
    {
        currentHealth = maxHealth;  // กำหนดค่าเริ่มต้นของเลือด
    }

    // ฟังก์ชันนี้จะถูกเรียกใช้เมื่อศัตรูโดนกระสุน
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;  // ลดค่าเลือดตามความเสียหายที่ได้รับ

        if (currentHealth <= 0)
        {
            Die();  // ถ้าเลือดหมด ให้เรียกใช้ฟังก์ชัน Die()
        }
    }

    // ฟังก์ชันที่ทำให้ศัตรูตาย
    void Die()
    {
        Destroy(gameObject);  // ทำลายวัตถุศัตรู
    }
}