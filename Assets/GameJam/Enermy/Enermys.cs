using UnityEngine;

public class Enemys : EnemyBase
{
    protected override void Start()
    {
        base.Start();
        speed = 4f;
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