using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ฟังก์ชันเพื่อให้สคริปต์อื่นสามารถเข้าถึง currentHealth ได้
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    void Die()
    {
        // แจ้ง GameManager ว่าศัตรูตัวนี้ถูกกำจัด
        GameManager.instance.OnEnemyDeath();

        Destroy(gameObject);
    }
}