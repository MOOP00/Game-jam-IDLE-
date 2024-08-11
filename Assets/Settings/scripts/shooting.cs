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
    public float bulletSpeed = 20f;  // ตัวแปรสำหรับปรับความเร็วกระสุน
    public int damage = 25;  // เพิ่มความเสียหายให้กระสุน
    public AudioClip gunSound;  // เสียงปืนที่ต้องการเล่น

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
        rb.linearVelocity = direction * bulletSpeed;  // ใช้ตัวแปรความเร็วกระสุนที่สามารถปรับได้ใน Unity Inspector

        bulletscript bulletScript = newBullet.AddComponent<bulletscript>();  // เพิ่ม bulletscript ให้กระสุนที่ยิง
        bulletScript.damage = damage;  // กำหนดดาเมจให้กระสุน

        // เล่นเสียงปืน
        SoundManager.instance.PlaySFX(gunSound);
    }


}