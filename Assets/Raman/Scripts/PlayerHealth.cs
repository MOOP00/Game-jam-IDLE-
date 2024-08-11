using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");
        }
    }

    void Die()
    {
        Debug.Log("Player has been defeated!");
        Destroy(gameObject); // Destroy the player object
        // Add any additional death logic here, like game over screen, respawning, etc.
    }

    public void IncreaseHealthPerWave(int waveNumber)
    {
        maxHealth *= Mathf.Pow(1.2f, waveNumber);
        currentHealth = maxHealth; // Reset health to max after increase
        Debug.Log($"Player health increased. New max health: {maxHealth}");
    }
}
