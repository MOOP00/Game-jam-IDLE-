using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet collided with the player
        if (collision.CompareTag("_player"))
        {
            Debug.Log("Bullet hit the player!");

            // Implement damage to the player here, if necessary

            // Destroy the bullet after it hits the player
            Destroy(gameObject);
        }
    }
}
