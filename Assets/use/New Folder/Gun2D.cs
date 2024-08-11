using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Gun2D : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().angularVelocity = bulletSpawnPoint.up * bulletSpeed;
        }
    }
}
