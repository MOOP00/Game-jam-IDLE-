using UnityEngine;
using System.Collections;

public class LaserBoss : Boss
{
    public int pelletsPerShot = 10;
    public float spreadAngle = 180f;
    public int shotsBeforeCooldown = 5; // Number of shots before cooldown
    public float cooldownDuration = 7f;  // Duration of cooldown in seconds

    private int shotsFired = 0;
    private bool isCoolingDown = false;

    protected override void Start()
    {
        base.Start();
        fireRate = 1.5f;
        damage = 20;
        bulletSpeed = 1f;
        bulletLifetime = 10f;
    }

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
    }

    protected override void ShootPlayer()
    {
        if (bulletPrefab != null && !isCoolingDown)
        {
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
        yield return new WaitForSeconds(cooldownDuration);
        shotsFired = 0; // Reset the shot counter after cooldown
        isCoolingDown = false;
    }
}
