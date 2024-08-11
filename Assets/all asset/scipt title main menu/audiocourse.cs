using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiocourse : MonoBehaviour
{
    public AudioSource soundEffect;

    void Start() {
        // หา AudioSource จาก GameObject นี้
        soundEffect = GetComponent<AudioSource>();
    }

    void Update() {
        // ตรวจสอบการกดปุ่ม
        if (Input.GetKeyDown(KeyCode.Mouse0)) // ตั้งค่าเป็นปุ่มที่คุณต้องการ
        {
            // เล่นเสียง
            soundEffect.Play();
        }
    }
}
