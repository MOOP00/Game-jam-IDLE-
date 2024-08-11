using UnityEngine;

public class Enemys : EnemyBase
{
    protected override void Start()
    {
        base.Start();
        speed = 5f;
    }

    protected override void Update()
    {
        base.Update();

        // Check if the enemy should flip based on the player's position
        if (player != null)
        {
            Vector3 scale = transform.localScale;

            // If the player is to the left of the enemy and the enemy is not already flipped
            if (transform.position.x > player.position.x && scale.x > 0)
            {
                scale.x = -Mathf.Abs(scale.x); // Flip to face left
            }
            // If the player is to the right of the enemy and the enemy is flipped
            else if (transform.position.x < player.position.x && scale.x < 0)
            {
                scale.x = Mathf.Abs(scale.x); // Flip to face right
            }

            transform.localScale = scale;
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
    }
}