using UnityEngine;

public class MeteorBoss : Boss
{
    public GameObject meteorPrefab;
    public float meteorSpread = 2f; // Distance between the meteors

    protected override void Start()
    {
        base.Start();
        fireRate = 8f;
        damage = 50;
    }

    protected override void AttackPlayer()
    {
        Debug.Log("MeteorBoss attacks the player!");
    }

    protected override void ShootPlayer()
    {
        if (meteorPrefab != null)
        {
            for (int i = -1; i <= 1; i++) // Loop to create 3 meteors
            {
                Vector2 spawnPosition = new Vector2(player.position.x + i * meteorSpread, transform.position.y);
                GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
                Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();
                rb.linearVelocity = Vector2.down * bulletSpeed * damageMultiplier;

                Destroy(meteor, bulletLifetime);
            }
        }
    }
}
