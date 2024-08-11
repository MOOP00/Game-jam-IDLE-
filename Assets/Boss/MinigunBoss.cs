using UnityEngine;
using System.Collections;


public class MinigunBoss : Boss
{
    public int bulletsPerBurst = 1;
    public int maxBulletsBeforeCooldown = 200; // Maximum bullets before entering cooldown
    public float cooldownDuration = 10f; // Cooldown duration in seconds

    private int bulletsFired = 0; // Counter for bullets fired
    private float lastShootTime; // Track when the last shot was fired
    private bool isCoolingDown = false; // Track if the boss is in cooldown

    protected override void Start()
    {
        base.Start();
        fireRate = 0.15f;
        damage = 2;
        bulletSpeed = 5f;
    }

    protected override void AttackPlayer()
    {
        Debug.Log("MinigunBoss attacks the player!");
    }

    protected override void ShootPlayer()
    {
        if (isCoolingDown)
        {
            // If in cooldown, return without shooting
            return;
        }

        if (bulletPrefab != null)
        {
            for (int i = 0; i < bulletsPerBurst; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.linearVelocity = (player.position - transform.position).normalized * bulletSpeed * damageMultiplier;

                Destroy(bullet, bulletLifetime);
            }

            bulletsFired += bulletsPerBurst; // Update bullets fired counter

            if (bulletsFired >= maxBulletsBeforeCooldown)
            {
                StartCoroutine(Cooldown()); // Start cooldown
            }
        }
    }

    private IEnumerator Cooldown()
    {
        isCoolingDown = true;
        Debug.Log("MinigunBoss is cooling down!");

        yield return new WaitForSeconds(cooldownDuration); // Wait for cooldown duration

        bulletsFired = 0; // Reset bullets fired counter
        isCoolingDown = false; // Exit cooldown state
        Debug.Log("MinigunBoss is ready to shoot again!");
    }
}
