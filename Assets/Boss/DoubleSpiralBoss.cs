using UnityEngine;

public class DoubleSpiralBoss : Boss
{
    public int spiralBulletCount = 30;
    public float spiralRotationSpeed = 200f;
    public float spiralOffset = 180f;

    private float currentAngle1 = 15f;
    private float currentAngle2 = 15f;

    protected override void Start()
    {
        base.Start();
        fireRate = 1.5f; // Adjust fire rate for this boss
        damage = 1; // Set custom damage value for this boss
    }

    protected override void ShootPlayer()
    {
        // First spiral
        currentAngle1 += spiralRotationSpeed * Time.deltaTime;
        ShootSpiral(currentAngle1);

        // Second spiral (opposite direction)
        currentAngle2 -= spiralRotationSpeed * Time.deltaTime;
        ShootSpiral(currentAngle2 + spiralOffset);
    }

    protected override void AttackPlayer()
    {
        // Implement the attack logic for when the boss is in close range of the player
        Debug.Log("DoubleSpiralBoss is attacking the player!");
    }

    private void ShootSpiral(float angle)
    {
        for (int i = 0; i < spiralBulletCount; i++)
        {
            float bulletAngle = angle + (360f / spiralBulletCount) * i;
            Vector2 bulletDirection = new Vector2(
                Mathf.Cos(bulletAngle * Mathf.Deg2Rad),
                Mathf.Sin(bulletAngle * Mathf.Deg2Rad)
            );

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = bulletDirection * bulletSpeed; // Use velocity instead of linearVelocity

            Destroy(bullet, bulletLifetime);
        }
    }
}
