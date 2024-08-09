using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPlayer2D : MonoBehaviour
{
    public Transform player; // ตัวผู้เล่นที่วัตถุจะหมุนรอบ
    public float rotationSpeed = 50f; // ความเร็วในการหมุน
    public float radius = 2f; // รัศมีของวงกลมที่วัตถุจะหมุนรอบ

    private float angle = 0f;

    void Update()
    {
        // คำนวณมุมหมุน
        angle += rotationSpeed * Time.deltaTime;

        // คำนวณตำแหน่งใหม่ของวัตถุในวงกลม
        float posX = Mathf.Cos(angle) * radius;
        float posY = Mathf.Sin(angle) * radius;

        // ตั้งตำแหน่งใหม่ให้วัตถุที่หมุนรอบตัวผู้เล่น
        transform.position = new Vector3(player.position.x + posX, player.position.y + posY, transform.position.z);
    }
}