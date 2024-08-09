using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Bullet hit the player!");

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
