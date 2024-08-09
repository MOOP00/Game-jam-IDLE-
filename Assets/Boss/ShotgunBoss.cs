using UnityEngine;

public class ShotgunBoss : Boss
{
    public int pelletsPerShot = 10;
    public float spreadAngle = 30f;

    protected override void Start()
    {
        base.Start();
        fireRate = 2f;
        damage = 15;
    }

    protected override void AttackPlayer()
    {
        Debug.Log("ShotgunBoss attacks the player!");
    }

    protected override void ShootPlayer()
    {
        if (bulletPrefab != null)
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
        }
    }
}
