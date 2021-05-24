using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneBehaviour : MonoBehaviour
{
    float nextFire;
    [SerializeField]
    float fireRate;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform bulletSpawn;
    float varSpeed = 5;
    float jumpForce = 10;
    Rigidbody2D rig;
    [SerializeField]    
    Transform groundCheck;
    [SerializeField]
    LayerMask groundLayers;
    [SerializeField]
    HealthBarBehaviour healthBar;
    float maxHealth = 1000;
    float playerHealth;
    public static bool ultimateIsReady = false;
    void Start()
    {
        playerHealth = maxHealth;
        healthBar.SetHealth(playerHealth, maxHealth);
        rig = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            ultimateIsReady = true;
            Debug.Log("A ultimate ta carregada");
        }
        healthBar.SetHealth(playerHealth, maxHealth);
        if(playerHealth/100 > 7)
        AlysaBulletBehaviour.typeOfBullet = 0;
        else if((playerHealth/100) > 4)
        AlysaBulletBehaviour.typeOfBullet = 1;
        else
        AlysaBulletBehaviour.typeOfBullet = 2;

        healthBar.SetHealth(playerHealth, maxHealth);
        if (playerHealth<=0)
        {
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >nextFire)
        {
            Attack();
        }
        transform.position += new Vector3(Input.GetAxis("Horizontal")*varSpeed*Time.deltaTime,0f,0f);
        if (Input.GetAxis("Horizontal")>0)
        {
           transform.eulerAngles = new Vector2(0f, 0f);
        }
        else
        {
           transform.eulerAngles = new Vector2(0f, 180f);
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            playerHealth = 600;
        }
    }
    void Jump()
    {
        rig.AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
    }
    void Attack()
    {
        nextFire = Time.time + fireRate;
        GameObject cloneCookie = Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
    }
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayers);
    }
    public void PlayerTakeDamage(float dmg)
    {
        playerHealth -= dmg;
    }
}
