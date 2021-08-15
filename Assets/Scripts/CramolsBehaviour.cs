using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CramolsBehaviour : MonoBehaviour, IPlayer
{
    [SerializeField] float fireRate;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] LayerMask enemyLayers;

    float varSpeed = 5f;
    float jumpForce = 10;
    Rigidbody2D rig;
    float maxHealth = 7f;
    float playerHealth;
    float nextFire;
    float direction;

    void Start()
    {
        playerHealth = maxHealth;
        // healthBar.SetHealth2(playerHealth, maxHealth);
        rig = this.GetComponent<Rigidbody2D>();
    }
    // void Update()
    // {
    //     healthBar.SetHealth2(playerHealth, maxHealth);
    // }
    public void Move(float axis)
    {
        transform.position += new Vector3(axis*varSpeed*Time.deltaTime, 0f,0f);
        if (axis>0)
        {
           transform.eulerAngles = new Vector2(0f, 0f);
        }
        if(axis<0)
        {
           transform.eulerAngles = new Vector2(0f, 180f);
        }
    }
    public void Jump(bool key)
    {
        if (key && IsGrounded())
        {
            rig.AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
        }
    }
    public void Attack(bool key)
    {
        if(key)
        print("Ataque Cramols");
    }
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayers);
    }
    public void UsingUltimate(bool key)
    {
        if(key)
        print("ult cramols");
    }
    public void PlayerTakeDamage(int dmg)
    {
        playerHealth -= dmg;
        // if (playerHealth<=0)
        // {
        //     Destroy(this.gameObject);
        // }
    }
}
