using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlysaBehaviour : MonoBehaviour, IPlayer
{
    [SerializeField] float fireRate;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] LayerMask enemyLayers;

    float varX;
    float varSpeed = 5f;
    float jumpForce = 10;
    Rigidbody2D rig;
    float maxHealth = 7f;
    float playerHealth;
    float nextFire;
    float direction;
    bool facingRight = true;

    void Start()
    {
        playerHealth = maxHealth;
        rig = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.E))
        Attack();
        if(Input.GetKeyDown(KeyCode.Q))
        StartCoroutine(Ultimate());
        if(Input.GetKeyDown(KeyCode.Space))
        Jump();
    }
    void FixedUpdate()
    {
        Move(direction);
    }
    void Move(float dir)
    {
        float xVal = dir * varSpeed * 100 * Time.fixedDeltaTime;
        Vector2 targetVelocity = new Vector2(xVal, rig.velocity.y);
        rig.velocity = targetVelocity;
 
        if(facingRight && dir < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
            facingRight = false;
        }
        else if(!facingRight && dir > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
            facingRight = true;
        }
    }
    void Jump()
    {
        if (IsGrounded())
        {
            rig.velocity = Vector2.up * jumpForce;
        }
    }
    void Attack()
    {
        if (Time.time >nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject bullets =Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
            bullets.GetComponent<AlysaBulletBehaviour>().typeOfSpawn = 1;
        }
    }
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayers);
    }
    IEnumerator Ultimate()
    {
        for(int i = 0;i<15;i++)
        {
            varX = Random.Range(-40f,5f);
            GameObject meteoro = Instantiate(bullet,new Vector3(varX,14.4f,0f),Quaternion.Euler(0f,0f,-45f));
            meteoro.GetComponent<AlysaBulletBehaviour>().typeOfSpawn = 2;
            yield return new WaitForSeconds(Random.Range(0.1f,0.2f));
        }
        yield return new WaitForSeconds(1.7f);
        Debug.Log("xablau");
        Collider2D[] enemies = Physics2D.OverlapBoxAll(transform.position,new Vector2(100,30f),0f,enemyLayers);
        foreach(Collider2D enemy in enemies)
        {
            // enemy.GetComponent<EnemyBehaviour>().TakeHit(10f);
        }
    }
    public void PlayerTakeDamage(int dmg)
    {
        playerHealth -= dmg;
        if(playerHealth> 5)
        AlysaBulletBehaviour.typeOfBullet = 0;
        else if(playerHealth > 3)
        AlysaBulletBehaviour.typeOfBullet = 1;
        else
        AlysaBulletBehaviour.typeOfBullet = 2;
        // if (playerHealth<=0)
        // {
        //     Destroy(this.gameObject);
        // }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("AA");
    }
}
