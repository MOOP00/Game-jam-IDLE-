using UnityEngine;

public class SizeAdjuster : MonoBehaviour
{
    public enum WeaponType
    {
        Main, // ปืนหลัก
        Small // ปืนเล็ก
    }

    public WeaponType weaponType; // กำหนดประเภทปืน

    private Vector3 targetSize;

    void Start()
    {
        // เลือกขนาดเป้าหมายตามประเภทปืน
        SetTargetSize();
        // ปรับขนาดของ GameObject ให้เป็นขนาดที่กำหนด
        AdjustSize();
    }

    void SetTargetSize()
    {
        switch (weaponType)
        {
            case WeaponType.Main:
                targetSize = new Vector3(0.42f, 0.42f, 0.42f);
                break;
            case WeaponType.Small:
                targetSize = new Vector3(0.29f, 0.29f, 0.29f);
                break;
        }
    }

    void AdjustSize()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogWarning("Renderer component not found.");
            return;
        }

        Vector3 originalSize = renderer.bounds.size;
        Vector3 scale = new Vector3(
            targetSize.x / originalSize.x,
            targetSize.y / originalSize.y,
            targetSize.z / originalSize.z
        );

        transform.localScale = scale;
    }
}
