﻿using UnityEngine;

public class GameManagerH : MonoBehaviour
{
    public static GameManagerH instance;  // เพิ่มตัวแปร instance

    public GameObject bossPrefab;
    public Transform bossSpawnPoint;

    void Awake()
    {
        // ตรวจสอบว่า instance นี้เป็น instance เดียวหรือไม่
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);  // ทำลาย object นี้ถ้ามี instance ที่ไม่ซ้ำกัน
        }
    }
    public AudioClip themeMusic;  

    void Start()
    {
        SoundManager.instance.PlayTheme(themeMusic);
    }

    public void OnEnemyDeath()
    {
        // ตรวจสอบว่าศัตรูทั้งหมดถูกกำจัดหรือยัง
        GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (remainingEnemies.Length == 0)
        {
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
        Debug.Log("Boss has spawned!");
    }
}