using UnityEngine;

public class MinigunBoss : Boss
{
    public int bulletsPerBurst = 30;

    protected override void Start()
    {
        base.Start();
        fireRate = 0.1f;
        damage = 1
           ;
    }

    protected override void AttackPlayer()
    {
        Debug.Log("MinigunBoss attacks the player!");
    }
    protected override void ShootPlayer()
    {
        if (bulletPrefab != null)
        {
            for (int i = 0; i < bulletsPerBurst; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.linearVelocity = (player.position - transform.position).normalized * bulletSpeed * damageMultiplier;

                Destroy(bullet, bulletLifetime);
            }
        }
    }
}
