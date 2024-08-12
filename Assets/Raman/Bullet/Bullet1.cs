using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet collided with the player
        if (collision.CompareTag("_player"))
        {
            Game._instance.TakeDamage(50);
            Destroy(gameObject);
        }
    }
}
