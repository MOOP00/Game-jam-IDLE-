using UnityEngine;

public class LaserBoss : Boss
{
    protected override void Start()
    {
        base.Start();
        fireRate = 1f;
        damage = 15;
    }

    protected override void AttackPlayer()
    {
        Debug.Log("LaserBoss attacks the player!");
    }

    protected override void ShootPlayer()
    {
        if (bulletPrefab != null)
        {
            GameObject laser = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
            rb.linearVelocity = (player.position - transform.position).normalized * bulletSpeed * damageMultiplier;

            Destroy(laser, bulletLifetime);
        }
    }
}
