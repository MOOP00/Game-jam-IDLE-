using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign your bullet prefab in the inspector
    public Transform shootPoint; // Where the bullet will spawn
    public float bulletSpeed = 10f; // Speed of the bullet
    public float bulletDamage = 10f; // Damage of the bullet

    void Update()
    {
        FollowMouseCursor();

        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Shoot();
        }
    }

    void FollowMouseCursor()
    {
        // Get the mouse position in the world
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        shootPoint.position = mousePosition; // Set the shoot point position to the mouse position

        // Optionally, you can rotate the shoot point to face the mouse cursor
        Vector2 direction = mousePosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        shootPoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = shootPoint.up * bulletSpeed; // Use the bullet speed

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = bulletDamage; // Set the damage for the bullet
        }
    }
}
