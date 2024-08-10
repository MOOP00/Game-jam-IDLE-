using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthSlider; // Assign this in the inspector
    private Boss boss;

    void Start()
    {
        // Find the Boss instance in the scene
        boss = FindObjectOfType<Boss>();

        if (boss != null)
        {
            // Initialize the health bar
            healthSlider.maxValue = boss.health;
            healthSlider.value = boss.health;
        }
        else
        {
            Debug.LogWarning("No Boss found in the scene.");
        }
    }

    void Update()
    {
        if (boss != null)
        {
            // Update the slider value based on the boss's current health
            healthSlider.value = Mathf.Clamp(boss.health, 0, healthSlider.maxValue);
        }
    }
}
