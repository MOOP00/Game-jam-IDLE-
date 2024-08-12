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
        damage = 10;
        bulletSpeed = 7f;
        bulletLifetime = 10f;
    }

    protected override void AttackPlayer()
    {
       base.AttackPlayer();
    }

    protected override void Update()
    {
        base.Update();

        // Check if the boss should flip based on the player's position
        if (player != null)
        {
            Vector3 scale = transform.localScale;

            // If the player is to the right of the boss and the boss is not already facing right
            if (transform.position.x < player.position.x && scale.x < 0)
            {
                scale.x = Mathf.Abs(scale.x); // Flip to face right
            }
            // If the player is to the left of the boss and the boss is not already facing left
            else if (transform.position.x > player.position.x && scale.x > 0)
            {
                scale.x = -Mathf.Abs(scale.x); // Flip to face left
            }

            transform.localScale = scale;
        }
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
            // Calculate the center position for bullet spawning (adjust as needed)
            Vector3 bulletSpawnPosition = transform.position + new Vector3(0, 2.5f, 0); // Adjust the Y value based on your boss sprite

            for (int i = 0; i < bulletsPerBurst; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);
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
