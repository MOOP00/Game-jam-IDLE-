using UnityEngine;

public class PlayerAutoShooting : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab ของกระสุน
    public Transform firePoint;  // จุดที่กระสุนจะถูกยิงออกมา
    public float shootingRange = 10f;  // ระยะการยิงอัตโนมัติ
    public float fireRate = 1f;  // อัตราการยิง
    private float nextTimeToFire = 0f;  // เวลาที่จะยิงครั้งต่อไป

    public AudioClip gunSound;  // เพิ่ม AudioClip สำหรับเสียงปืน

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy <= shootingRange && distanceToEnemy < shortestDistance)
            {
                nearestEnemy = enemy;
                shortestDistance = distanceToEnemy;
            }
        }

        if (nearestEnemy != null && Time.time >= nextTimeToFire)
        {
            Shoot(nearestEnemy);
            nextTimeToFire = Time.time + 1f / fireRate;
        }
    }

    void Shoot(GameObject enemy)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        HomingBullet homingBullet = bullet.GetComponent<HomingBullet>();

        if (homingBullet != null)
        {
            homingBullet.SetTarget(enemy.transform);  // กำหนดเป้าหมายให้กระสุนติดตาม
        }

        // เล่นเสียงปืน
        //SoundManager.instance.PlaySFX(gunSound);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootingRange);  // วาดวงกลมแสดงระยะยิงใน Scene view
    }
}