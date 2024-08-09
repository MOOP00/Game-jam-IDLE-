using UnityEngine;

public class BombingBoss : Boss
{
    public GameObject bombPrefab;

    protected override void Start()
    {
        base.Start();
        fireRate = 4f;
        damage = 25;
    }

    protected override void AttackPlayer()
    {
        Debug.Log("BombingBoss attacks the player!");
    }

    protected override void ShootPlayer()
    {
        if (bombPrefab != null)
        {
            GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.down * bulletSpeed * damageMultiplier;

            Destroy(bomb, bulletLifetime);
        }
    }
}
