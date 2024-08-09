using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class playershooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2)
        {
            timer = 0;
            shoot();
        }
        shoot();
    }


    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
