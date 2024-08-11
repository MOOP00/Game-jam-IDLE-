using UnityEngine;

public class PlayerAutoShooting : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab ของกระสุน
    public float shootingRange = 10f;  // ระยะการยิงอัตโนมัติ
    public float fireRate = 1f;  // อัตราการยิง
    private float nextTimeToFire = 0f;  // เวลาที่จะยิงครั้งต่อไป
    public Transform firePoint; // จุดยิงของกระสุน
    public float damage; // ค่าความเสียหายที่กำหนดให้กระสุน
    public WeaponSupport weaponSupport;

    public int selectedSlotIndex = 0; // ตัวแปรเพื่อระบุดัชนีของ supportSlots ที่ต้องการดึงค่า
    public SO_Item currentSupportItem; // ไอเท็มที่ใช้ในการยิง
    public WeaponSupport.Data_Item currentDataItem; // เพื่อเข้าถึง stacklvl
    public AudioClip audios;

    private GameObject currentBullet; // ตัวแปรเพื่อเก็บการอ้างอิงไปยัง bullet ที่สร้างขึ้น

    private void Start()
    {
        weaponSupport = FindObjectOfType<WeaponSupport>();
    }

    private float CalculateDamage(float baseAttackPower, Rarity rarity, int stacklvl)
    {
        float value = CalculateValue(rarity, stacklvl);
        float valueRarity = CalculateValueRarity(rarity, stacklvl);
        return baseAttackPower * (value + valueRarity * stacklvl);
    }

    float CalculateValue(Rarity rarity, int stacklvl)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return 1.25f;
            case Rarity.Uncommon:
                return 2f;
            case Rarity.Rare:
                return 5f;
            case Rarity.Epic:
                return 10f;
            case Rarity.Legendary:
                return 50f;
            default:
                return 0f;
        }
    }

    float CalculateValueRarity(Rarity rarity, int stacklvl)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return 0.25f;
            case Rarity.Uncommon:
                return 0.5f;
            case Rarity.Rare:
                return 1.25f;
            case Rarity.Epic:
                return 2.5f;
            case Rarity.Legendary:
                return 2;
            default:
                return 0f;
        }
    }

    private void Update()
    {
        // ตรวจสอบว่า firePoint เป็น null และพยายามค้นหา
        if (firePoint == null)
        {
            firePoint = FindDeepChild(transform, "Fire_Point");
            if (firePoint == null)
            {
                return;
            }
        }

        // กำหนด currentSupportItem และ currentDataItem จาก slot ที่ระบุ
        SetCurrentSupportItem(selectedSlotIndex);

        // ตรวจสอบว่า currentSupportItem และ currentDataItem ถูกกำหนดค่าแล้ว
        if (currentSupportItem != null && currentDataItem.itemData != null)
        {
            // คำนวณความเสียหาย
            damage = CalculateDamage(Game.Instance.AttackPower, currentSupportItem.rarity, currentDataItem.lvl);
            Debug.Log(damage);
        }
        else
        {
            Debug.LogWarning("Current support item or data item is not set. Damage calculation will be skipped.");
            // ลบ bullet ที่สร้างขึ้นถ้าต้องการ
            if (currentBullet != null)
            {
                Destroy(currentBullet);
                currentBullet = null;
            }
            return;
        }

        // ค้นหาศัตรูที่อยู่ใกล้ที่สุดในระยะยิง
        GameObject nearestEnemy = FindNearestEnemy();

        // ยิงศัตรูที่ใกล้ที่สุดหากถึงเวลายิง
        if (nearestEnemy != null && Time.time >= nextTimeToFire)
        {
            Shoot(nearestEnemy);
            nextTimeToFire = Time.time + 1f / fireRate;
        }
    }

    private Transform FindDeepChild(Transform parent, string childName)
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

    // Ensure supportSlots are properly initialized and assigned
    private void SetCurrentSupportItem(int slotIndex)
    {
        // ตรวจสอบให้แน่ใจว่า slotIndex ไม่เกินขนาดของ supportSlots
        if (slotIndex >= 0 && slotIndex < weaponSupport.supportSlots.Length)
        {
            // กำหนด currentSupportItem และ currentDataItem จากช่องที่ระบุ
            currentSupportItem = weaponSupport.supportSlots[slotIndex].itemData;
            currentDataItem = weaponSupport.supportSlots[slotIndex];
        }
        else
        {
            currentSupportItem = null;
            currentDataItem = new WeaponSupport.Data_Item(0, null);
            Debug.LogWarning("Slot index is out of range. Ensure WeaponSupport is properly initialized.");
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance && distance <= shootingRange)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    private void Shoot(GameObject target)
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Bullet prefab or fire point is not assigned.");
            return;
        }

        // สร้างกระสุนและกำหนดค่าความเสียหาย
        if (currentBullet != null)
        {
            Destroy(currentBullet); // ลบ bullet เดิมถ้ามี
        }

        currentBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        HomingBullet bulletScript = currentBullet.GetComponent<HomingBullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDamage(damage); // Set the calculated damage

            // กำหนดเป้าหมายให้กับกระสุน
            bulletScript.SetTarget(target.transform);

            // Play the shooting sound
            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlaySFX(audios);
            }
        }
        else
        {
            Debug.LogWarning("HomingBullet script not found on the bullet prefab.");
            Destroy(currentBullet); // ลบ bullet ถ้าไม่พบ script
            currentBullet = null;
        }
    }


}