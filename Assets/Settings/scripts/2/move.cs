using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndShoot : MonoBehaviour
{
    public Transform player; // ตัวผู้เล่นที่วัตถุจะหมุนรอบ
    public GameObject bulletPrefab; // พรีแฟบของกระสุน
    public float rotationSpeed = 50f; // ความเร็วในการหมุน
    public float radius = 2f; // รัศมีของวงกลมที่วัตถุจะหมุนรอบ
    public float shootInterval = 0.5f; // ช่วงเวลาระหว่างการยิง
    public float bulletForce = 10f; // ความเร็วกระสุน

    private float angle = 0f;
    private float nextShootTime = 0f;

    void Update()
    {
        // คำนวณมุมหมุน
        angle += rotationSpeed * Time.deltaTime;

        // คำนวณตำแหน่งใหม่ของวัตถุในวงกลม
        float posX = Mathf.Cos(angle) * radius;
        float posY = Mathf.Sin(angle) * radius;

        // ตั้งตำแหน่งใหม่ให้วัตถุที่หมุนรอบตัวผู้เล่น
        transform.position = new Vector3(player.position.x + posX, player.position.y + posY, transform.position.z);

        // ตรวจสอบเวลาการยิงกระสุน
        if (Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootInterval;
        }
    }

    void Shoot()
    {
        // สร้างกระสุนจากพรีแฟบที่ตำแหน่งปัจจุบันของ object
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        // เพิ่มความเร็วให้กระสุนพุ่งไปตามทิศทางที่ object หันหน้าอยู่
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = transform.up; // ทิศทางการยิงคือทิศทางที่ object หันหน้าอยู่
            rb.linearVelocity = direction * bulletForce;
        }
    }
}