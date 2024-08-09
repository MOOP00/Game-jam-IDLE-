using UnityEngine;

public class Enemys : EnemyBase
{
    protected override void Start()
    {
        base.Start();
        speed = 4f;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
    }
}