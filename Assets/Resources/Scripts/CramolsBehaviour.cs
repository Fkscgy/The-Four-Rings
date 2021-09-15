using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CramolsBehaviour : MonoBehaviour, IPlayer
{
    [SerializeField] float fireRate;
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
    bool isGrounded;
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
    }
    void Update()
    {
        animator.SetBool("isParado", rig.velocity.x == 0);
        direction = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayers);
        animator.SetBool("isGrounded", isGrounded);

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
    void Move(float axis)
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
    void Jump()
    {
        if (isGrounded)
        {
            rig.AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
        }
    }
    void Attack()
    {
        print("Ataque Cramols");
    }
    IEnumerator Ultimate()
    {
        print("ult cramols");
        yield return new WaitForSeconds (2f);
    }
    public void PlayerTakeDamage(int dmg)
    {
        Hp -= dmg;
        hpBar.SetSlider(Hp,MaxHp);
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
