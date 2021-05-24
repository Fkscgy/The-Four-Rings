using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    float nextFire;
    [SerializeField]
    float fireRate;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform bulletSpawn;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject cloneCookie = Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
        }
    }
}
