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

    public AudioClip audioo;

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

        // ตรวจสอบทิศทางเพื่อปรับมุมหมุน
        float rotationSpeed = 100f;
        float currentRotation = transform.eulerAngles.z;
        if (direction.x < 0) // ด้านซ้าย
        {
            targetRotation += 180; // เพิ่ม 180 องศาเพื่อให้หันตรงข้าม
        }
        float angle = Mathf.MoveTowardsAngle(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
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
        SoundManager.instance.PlaySFX(audioo);
        GameObject newBullet = Instantiate(bullet, bulletTransform.position, bulletTransform.rotation);
        bulletscript bulletScript = newBullet.GetComponent<bulletscript>();

        if (bulletScript != null)
        {
            bulletScript.damage = weaponMain.GetDamage();  // Set damage based on the weapon
            bulletScript.bulletSpeed = bulletSpeed;  // Set bullet speed if needed
        }
        else
        {
            Debug.LogWarning("bulletscript not found on the bullet prefab.");
            Destroy(newBullet);
        }
    }
}
