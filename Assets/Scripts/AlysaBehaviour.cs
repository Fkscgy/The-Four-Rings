using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlysaBehaviour : MonoBehaviour, IPlayer
{
    
    [SerializeField]
    float fireRate;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform bulletSpawn;
    [SerializeField]    
    Transform groundCheck;
    [SerializeField]
    LayerMask groundLayers;
    [SerializeField]
    LayerMask enemyLayers;
    [SerializeField]
    // HealthBarBehaviour healthBar;
    barradevida healthBar;

    float varX;
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
        healthBar.SetHealth2(playerHealth, maxHealth);
        rig = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            playerHealth -=1;
        }
        if (playerHealth<=0)
        {
            Destroy(this.gameObject);
        }
        if(playerHealth> 5)
        AlysaBulletBehaviour.typeOfBullet = 0;
        else if(playerHealth > 3)
        AlysaBulletBehaviour.typeOfBullet = 1;
        else
        AlysaBulletBehaviour.typeOfBullet = 2;

        healthBar.SetHealth2(playerHealth, maxHealth);
        
        if ( Input.GetAxis("AxisTeclado")>0)
        {
           transform.eulerAngles = new Vector2(0f, 0f);
        }
        if(Input.GetAxis("AxisTeclado")<0)
        {
           transform.eulerAngles = new Vector2(0f, 180f);
        }
    }
    public void Move(float axis)
    {
        transform.position += new Vector3(axis*varSpeed*Time.deltaTime, 0f,0f);
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
        if (key &&Time.time >nextFire)
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
    public void UsingUltimate()
    {
        StartCoroutine(Ultimate());
    }
    private IEnumerator Ultimate()
    {
        for(int i = 0;i<15;i++)
        {
            varX = Random.Range(-40f,5f);
            GameObject meteoro = Instantiate(bullet,new Vector3(varX,14.4f,0f),Quaternion.Euler(0f,0f,-45f));
            meteoro.GetComponent<AlysaBulletBehaviour>().typeOfSpawn = 2;
            yield return new WaitForSeconds(Random.Range(0.1f,0.2f));
        }
        yield return new WaitForSeconds(1.5f);
        Collider2D[] enemies = Physics2D.OverlapBoxAll(transform.position,new Vector2(100,30f),0f,enemyLayers);
        foreach(Collider2D enemy in enemies)
        {
            enemy.GetComponent<EnemyBehaviour>().TakeHit(10f);
        }
    }
    public void PlayerTakeDamage(float dmg)
    {
        playerHealth -= dmg;
        if (playerHealth<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
