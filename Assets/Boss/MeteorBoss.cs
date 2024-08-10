using UnityEngine;

public class MeteorBoss : Boss
{
    public GameObject meteorPrefab;
    public float meteorSpread = 5f; // Distance between the meteors

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
            float spawnHeightAbovePlayer = 10f; // Adjust this value to control how high above the player the meteors spawn

            for (int i = -1; i <= 1; i++) // Loop to create 3 meteors
            {
                // Randomize the spread by adding a random value to the meteorSpread
                float randomSpread = meteorSpread + Random.Range(-5f, 5f); // Adjust the range to control the randomness

                Vector2 spawnPosition = new Vector2(player.position.x + i * randomSpread, player.position.y + spawnHeightAbovePlayer);
                GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
                Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();
                rb.linearVelocity = Vector2.down * bulletSpeed * damageMultiplier;

                Destroy(meteor, bulletLifetime);
            }
        }
    }


}
