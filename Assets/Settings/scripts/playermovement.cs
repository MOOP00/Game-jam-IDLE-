using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // ความเร็วในการเคลื่อนที่

    void Update()
    {
        // กำหนดค่าเริ่มต้นของทิศทางการเคลื่อนที่
        float moveX = 0f;
        float moveY = 0f;

        // ตรวจจับการกดปุ่ม A (ซ้าย) และ D (ขวา)
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -moveSpeed; // ซ้าย
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = moveSpeed; // ขวา
        }

        // ตรวจจับการกดปุ่ม W (ขึ้น) และ S (ลง)
        if (Input.GetKey(KeyCode.W))
        {
            moveY = moveSpeed; // ขึ้น
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveY = -moveSpeed; // ลง
        }

        // สร้าง Vector3 สำหรับทิศทางการเคลื่อนที่
        Vector3 moveDirection = new Vector3(moveX, moveY, 0f) * Time.deltaTime;

        // เคลื่อนที่ตัวละครตามทิศทางที่ได้
        transform.Translate(moveDirection, Space.World);
    }
}