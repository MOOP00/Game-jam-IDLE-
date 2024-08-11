using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;  // เลือดสูงสุดของศัตรู
    private int currentHealth;  // เลือดปัจจุบันของศัตรู

    void Start()
    {
        currentHealth = maxHealth;  // กำหนดค่าเริ่มต้นของเลือด
    }

    // ฟังก์ชันนี้จะถูกเรียกใช้เมื่อศัตรูโดนกระสุน
    public void TakeDamage(int damage)
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