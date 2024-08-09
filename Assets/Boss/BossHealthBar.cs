using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    private Boss boss;

    void Start()
    {
        // Assuming the boss is already in the scene or spawned
        boss = FindObjectOfType<Boss>();

        if (boss != null)
        {
            // Initialize the health bar
            healthSlider.maxValue = boss.health;
            healthSlider.value = boss.health;
        }
    }

    void Update()
    {
        if (boss != null)
        {
            // Update the slider value based on the boss's current health
            healthSlider.value = boss.health;
        }
    }
}
