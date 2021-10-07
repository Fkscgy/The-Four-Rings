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
    static int hp = 10;
    int maxEn = 10;
    static int en = 0;
    float nextFire;
    float direction;
    bool facingRight = true;
    bool isReady;
    Animator animator;
    bool isGrounded;
    static bool isAlive = true;
    string tipo = "Alysa";
    [SerializeField]
    GameController gameController;

    public bool IsReady { get => isReady; set => isReady = value; }
    public string Tipo { get => tipo; set => tipo = value; }
    public static bool IsAlive { get => isAlive; set => isAlive = value; }

    void Start()
    {
        hpBar.SetSlider(hp,maxHp);
        enBar.SetSlider(en,maxEn);
        rig = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }
    void Update()
    {
        animator.SetBool("isParado", rig.velocity.x == 0);
        direction = Input.GetAxis("Horizontal_P1");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayers);
        animator.SetBool("isGrounded", isGrounded);

        if(Input.GetKeyDown(KeyCode.U))
        Attack();
        if(Input.GetKeyDown(KeyCode.O) && en == 10)
        StartCoroutine(Ultimate());
        if(Input.GetKeyDown(KeyCode.I))
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
        if (isGrounded)
        {
            animator.SetTrigger("Pulo");
            rig.velocity = Vector2.up * jumpForce;
        }
    }
    void Attack()
    {
        if (Time.time >nextFire)
        {
            animator.SetInteger("AtaqueIndex", Random.Range(0,2));
            animator.SetTrigger("Ataque");
            nextFire = Time.time + fireRate;
            GameObject bullets =Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
            bullets.GetComponent<AlysaBulletBehaviour>().typeOfSpawn = 1;
        }
    }
    IEnumerator Ultimate()
    {
        en = 0;
        for(int i = 0;i<15;i++)
        {
            varX = Random.Range(-40f,5f);
            GameObject meteoro = Instantiate(bullet,new Vector3(varX,14.4f,0f),Quaternion.Euler(0f,0f,-45f));
            meteoro.GetComponent<AlysaBulletBehaviour>().typeOfSpawn = 2;
            yield return new WaitForSeconds(Random.Range(0.1f,0.2f));
        }
        yield return new WaitForSeconds(1.7f);
        GameObject[] inimigos =GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject inimigo in inimigos)
        {
            inimigo.GetComponent<IEnemy>().TakeHit(2);
        }
    }
    public void PlayerTakeDamage(int dmg)
    {
        hp -= dmg;
        if(hp > 5)
        AlysaBulletBehaviour.typeOfBullet = 0;
        else if(hp > 3)
        AlysaBulletBehaviour.typeOfBullet = 1;
        else
        AlysaBulletBehaviour.typeOfBullet = 2;
        if(hp <= 0)
        {
            IsAlive = false;
            gameController.KillPlayer(this.gameObject);
        }
        hpBar.SetSlider(hp,maxHp);
    }
    void PlayerRegenLife(int heal)
    {
        if(maxHp==hp)
        return;
        if(heal + hp > maxHp)
        {
            hp = maxHp;
        } else 
        {
            hp += heal;
        }
        hpBar.SetSlider(hp,maxHp);
    }
    void PlayerRegenEnergy(int charge)
    {
        if(maxEn==en)
        return;
        if(charge + en > maxEn)
        {
            en = maxEn;
        } else 
        {
            en += charge;
        }
        enBar.SetSlider(en,maxEn);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.tag)
        {
            case "Vida":
                Destroy(col.gameObject);
                PlayerRegenLife(3);
            break;
            case "Energia":
                Destroy(col.gameObject);
                PlayerRegenEnergy(3);
            break;
        }
    }
}
