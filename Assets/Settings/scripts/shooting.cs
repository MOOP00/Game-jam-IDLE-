using UnityEngine;

public class shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;  // Prefab ของกระสุน
    public Transform bulletTransform;  // ตำแหน่งที่กระสุนจะถูกยิงออกมา
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    public int damage = 25;  // เพิ่มความเสียหายให้กระสุน

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 rotation = mousePos - transform.position;
        float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotz);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, bulletTransform.position, bulletTransform.rotation);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();

        Vector2 direction = (mousePos - transform.position).normalized;
        rb.linearVelocity = direction * 10f;  // กำหนดความเร็วกระสุน

        // ใส่ Collider2D ให้ Prefab ของกระสุนที่คุณยิงเอง
    }
}

public class Bullet : MonoBehaviour
{
    public int damage = 25;  // กำหนดความเสียหาย

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่ากระสุนชนกับศัตรู
        if (collision.CompareTag("Enemy"))
        {
            // ดึงข้อมูล EnemyHealth component ของศัตรู
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

            // ถ้าศัตรูมี EnemyHealth component ให้ทำดาเมจ
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);  // เรียกใช้ฟังก์ชัน TakeDamage ของศัตรู
            }

            // ทำลายกระสุนหลังจากทำดาเมจ
            Destroy(gameObject);
        }
    }
}