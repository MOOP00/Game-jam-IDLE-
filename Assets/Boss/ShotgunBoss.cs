using UnityEngine;
using System.Collections;

public class ShotgunBoss : Boss
{
    public int pelletsPerShot = 15;
    public float spreadAngle = 90f;
    public int shotsBeforeCooldown = 75; // Number of shots before cooldown
    public float cooldownDuration = 0f;  // Duration of cooldown in seconds

    private int shotsFired = 0;
    private bool isCoolingDown = false;

    // Animator component
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        fireRate = 1.3f;
        damage = 5;
        bulletSpeed = 5f;

        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        // Update animation states based on conditions
        if (isCoolingDown)
        {
            animator.SetBool("IsShooting", false);
            animator.SetBool("IsIdle", true);
        }
        else if (shotsFired < shotsBeforeCooldown)
        {
            animator.SetBool("IsShooting", true);
            animator.SetBool("IsIdle", false);
        }
        else
        {
            animator.SetBool("IsShooting", false);
            animator.SetBool("IsIdle", true);
        }
    }

    protected override void AttackPlayer()
    {
        Debug.Log("ShotgunBoss attacks the player!");
    }

    protected override void ShootPlayer()
    {
        if (bulletPrefab != null && !isCoolingDown)
        {
            // Set animation to shooting
            animator.SetBool("IsShooting", true);
            animator.SetBool("IsIdle", false);

            Vector2 baseDirection = (player.position - transform.position).normalized;
            float baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg;
            float angleStep = spreadAngle / (pelletsPerShot - 1);
            float startAngle = baseAngle - spreadAngle / 2;

            for (int i = 0; i < pelletsPerShot; i++)
            {
                float currentAngle = startAngle + (angleStep * i);
                float radianAngle = currentAngle * Mathf.Deg2Rad;

                Vector2 direction = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));

                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.linearVelocity = direction * bulletSpeed * damageMultiplier;

                Destroy(bullet, bulletLifetime);
            }

            shotsFired++;

            if (shotsFired >= shotsBeforeCooldown)
            {
                StartCoroutine(Cooldown());
            }
        }
    }

    private IEnumerator Cooldown()
    {
        isCoolingDown = true;

        // Set animation to idle during cooldown
        animator.SetBool("IsShooting", false);
        animator.SetBool("IsIdle", true);

        yield return new WaitForSeconds(cooldownDuration);
        shotsFired = 0; // Reset the shot counter after cooldown
        isCoolingDown = false;
    }
}