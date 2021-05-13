using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    
    Rigidbody2D rig;
    public float varSpeed;

    public Transform bulletSpawn;
    public GameObject cookieObject;
    public float fireRate;
    private float nextFire;
    public static float playerHP;
    public float maxHP;
    public int typeOfWeapon;
    public HealthBarBehaviour Health;
    
    void Start()
    {
        playerHP = maxHP;
        rig = GetComponent<Rigidbody2D>();
        Health.SetHealth(playerHP,maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        Health.SetHealth(playerHP,maxHP);
        
        if (playerHP >70)
        {
            BulletBehaviour.typeOfBullet = 1;
        } else if(playerHP >40)
        {
            BulletBehaviour.typeOfBullet = 2;
        } else if (playerHP<=40)
        {
            BulletBehaviour.typeOfBullet = 3;
        }
        Fire();
        transform.position += new Vector3(Input.GetAxis("Horizontal")*varSpeed*Time.deltaTime,Input.GetAxis("Vertical")*varSpeed*Time.deltaTime, 0f);
        if (playerHP <=0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    void Fire()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject cloneCookie = Instantiate (cookieObject, bulletSpawn.position, bulletSpawn.rotation);
        }
    }
    public void PlayerDamage(float num)
    {
        playerHP -= num;
    }
}
