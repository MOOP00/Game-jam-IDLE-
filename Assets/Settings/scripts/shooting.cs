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
    public float bulletSpeed = 20f;
    private Weapon weaponMain;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        weaponMain = FindObjectOfType<Weapon>();  // ใช้ FindObjectOfType
    }

    void Update()
    {
        if (bulletTransform == null)
        {
            bulletTransform = FindDeepChild(transform, "Fire_Point");
            if (bulletTransform == null)
            {
                return;
            }
        }

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - (Vector3)transform.position;
        float targetRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float rotationSpeed = 100f;
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, angle);

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

    Transform FindDeepChild(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child;
            }

            Transform foundChild = FindDeepChild(child, childName);
            if (foundChild != null)
            {
                return foundChild;
            }
        }
        return null;
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, bulletTransform.position, bulletTransform.rotation);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();

        Vector2 direction = (mousePos - transform.position).normalized;
        rb.linearVelocity = direction * bulletSpeed;

        bulletscript bulletScript = newBullet.GetComponent<bulletscript>();
        if (bulletScript != null)
        {
            bulletScript.damage = weaponMain.GetDamage();  // Set damage based on the weapon
        }
        else
        {
            Debug.LogWarning("Bullet script not found on the bullet prefab.");
            Destroy(newBullet);
        }
    }
}

public class Bullet : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่ากระสุนชนกับศัตรู
        if (collision.CompareTag("Enermy"))
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