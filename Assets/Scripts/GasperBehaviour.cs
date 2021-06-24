using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasperBehaviour : MonoBehaviour, IPlayer
{
    
    [SerializeField]    
    Transform groundCheck;
    [SerializeField]
    LayerMask groundLayers;
    [SerializeField]
    LayerMask enemyLayers;
    [SerializeField]
    barradevida healthBar;

    float varX;
    float varSpeed = 5f;
    float jumpForce = 10;
    Rigidbody2D rig;
    float maxHealth = 7f;
    float playerHealth;
    float direction;

    public int tipo{get;set;}
    void Start()
    {
        playerHealth = maxHealth;
        healthBar.SetHealth2(playerHealth, maxHealth);
        rig = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            playerHealth -=1;
        }
        if (playerHealth<=0)
        {
            Destroy(this.gameObject);
        }
        healthBar.SetHealth2(playerHealth, maxHealth);
    }
    public void Jump(bool key)
    {
        if (key && IsGrounded())
        {
            rig.AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
        }
    }
    public void Move(float axis)
    {
        transform.position += new Vector3(axis*varSpeed*Time.deltaTime, 0f,0f);
    }
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayers);
    }
    public void Attack(bool key)
    {
        if(key)
        Debug.Log("Ataque do Casper");
    }
    public void UsingUltimate(bool key)
    {
        if(key)
        print("ult gasper");
    }
    public void Ultimate()
    {
        Debug.Log("Ult do Gasper");
    }
    public void PlayerTakeDamage(float dmg)
    {
        playerHealth -= dmg;
    }
}
