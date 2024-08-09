using UnityEngine;

public class MeteorBoss : Boss
{
    public GameObject meteorPrefab;

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
            GameObject meteor = Instantiate(meteorPrefab, new Vector2(player.position.x, transform.position.y), Quaternion.identity);
            Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.down * bulletSpeed * damageMultiplier;

            Destroy(meteor, bulletLifetime);
        }
    }
}
