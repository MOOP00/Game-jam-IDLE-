﻿using UnityEngine;

public class bulletscript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float bulletSpeed = 20f;  // ความเร็วของกระสุน
    public float damage = 25;  // ความเสียหายที่กระสุนสามารถทำได้
    public float lifetime = 7f;

    void Start()
    {
        Destroy(gameObject, lifetime);
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;  // ใช้ bulletSpeed เพื่อปรับความเร็วกระสุน
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);  // เรียกใช้ฟังก์ชัน TakeDamage ของศัตรู
            }

            Destroy(gameObject);  // ทำลายกระสุนหลังจากทำดาเมจ
        }
        // เพิ่มโค้ดสำหรับ Boss หากต้องการ
    }
}