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
    [SerializeField] SliderBehaviour hpBar;
    [SerializeField] SliderBehaviour enBar;


    float varX;
    float varSpeed = 5f;
    float jumpForce = 10;
    Rigidbody2D rig;
    int maxHp = 10;
    int hp;
    int maxEn = 10;
    int en;
    float nextFire;
    float direction;
    bool facingRight = true;
    bool isReady;
    Animator animator;
    [SerializeField]
    CameraBehaviour mainCamera;

    public int MaxHp { get => maxHp; set => maxHp = value; }
    public int Hp { get => hp; set => hp = value; }
    public int MaxEn { get => maxEn; set => maxEn = value; }
    public int En { get => en; set => en = value; }
    public bool IsReady { get => isReady; set => isReady = value; }

    void Start()
    {
        Hp = MaxHp;
        En = 0;
        hpBar.SetSlider(Hp,MaxHp);
        enBar.SetSlider(En,MaxEn);
        rig = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        // mainCamera = GameObject.Find("Camera").GetComponent<CameraBehaviour>();
    }
    void Update()
    {
        animator.SetBool("isParado", rig.velocity.x == 0);
        direction = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.E))
        Attack();
        if(Input.GetKeyDown(KeyCode.Q))
        StartCoroutine(Ultimate());
        if(Input.GetKeyDown(KeyCode.Space))
        Jump();
        if(Input.GetKeyDown(KeyCode.Y))
        PlayerTakeDamage(1);
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
            animator.SetTrigger("Pulo");
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
        //Collider2D[] enemies = GameObject.FindGameObjectsWithTag("Enemy").GetComponent<EnemyBehaviour>().TakeDamage(2);
    }
    public void PlayerTakeDamage(int dmg)
    {
        Hp -= dmg;
        hpBar.SetSlider(Hp,MaxHp);
        if(Hp > 5)
        AlysaBulletBehaviour.typeOfBullet = 0;
        else if(Hp > 3)
        AlysaBulletBehaviour.typeOfBullet = 1;
        else
        AlysaBulletBehaviour.typeOfBullet = 2;
        if(Hp <= 0)
        {
            GameController.KillPlayer(this.gameObject);
        }
    }
    void PlayerRegenLife(int heal)
    {
        if(maxHp==Hp)
        return;
        if(heal + Hp > maxHp)
        {
            Hp = maxHp;
        } else 
        {
            Hp += heal;
        }
        hpBar.SetSlider(Hp,MaxHp);
    }
    void PlayerRegenEnergy(int charge)
    {
        if(MaxEn==En)
        return;
        if(charge + En > MaxEn)
        {
            En = MaxEn;
        } else 
        {
            En += charge;
        }
        enBar.SetSlider(En,MaxEn);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Vida"))
        {
            Destroy(col.gameObject);
            PlayerRegenLife(3);
        }
        if(col.CompareTag("Energia"))
        {
            Destroy(col.gameObject);
            PlayerRegenEnergy(3);
        }
    }
}
