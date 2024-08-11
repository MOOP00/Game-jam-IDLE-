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
        fireRate = 1.275f;
        damage = 5;
        bulletSpeed = 6f;

        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        // Check if the boss should flip based on the player's position
        if (player != null)
        {
            Vector3 scale = transform.localScale;

            // If the player is to the left of the boss and the boss is not already flipped
            if (transform.position.x > player.position.x && scale.x > 0)
            {
                scale.x = -Mathf.Abs(scale.x); // Flip to face left
            }
            // If the player is to the right of the boss and the boss is flipped
            else if (transform.position.x < player.position.x && scale.x < 0)
            {
                scale.x = Mathf.Abs(scale.x); // Flip to face right
            }

            transform.localScale = scale;
        }

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
