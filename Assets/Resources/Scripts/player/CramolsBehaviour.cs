using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CramolsBehaviour : MonoBehaviour, IPlayer
{
    [SerializeField] float attackRate;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] SliderBehaviour hpBar;
    [SerializeField] SliderBehaviour enBar;
    [SerializeField] float duracaoUlt;

    float varX;
    float varSpeed = 5f;
    float jumpForce = 10;
    Rigidbody2D rig;
    int maxHp = 10;
    static int hp;
    int maxEn = 10;
    static int en;
    float nextAttack;
    float direction;
    bool facingRight = true;
    bool isReady;
    Animator animator;
    bool isGrounded;
    static bool isAlive = true;
    string tipo = "Cramols";
    [SerializeField]
    GameController gameController;

    public bool IsReady { get => isReady; set => isReady = value; }
    public string Tipo { get => tipo; set => tipo = value; }
    public static bool IsAlive { get => isAlive; set => isAlive = value; }

    void Start()
    {
        hp = maxHp;
        en = 0;
        hpBar.SetSlider(hp,maxHp);
        enBar.SetSlider(en,maxEn);
        rig = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }
    void Update()
    {
        animator.SetBool("isParado", rig.velocity.x == 0);
        direction = Input.GetAxis("Horizontal_P2");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayers);
        animator.SetBool("isGrounded", isGrounded);

        if(Input.GetKeyDown(KeyCode.JoystickButton0))
        Attack(false, 1);
        if(Input.GetKeyDown(KeyCode.JoystickButton2) && en == 10)
        StartCoroutine(Ultimate());
        if(Input.GetKeyDown(KeyCode.JoystickButton1))
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
    void Attack(bool ultimate, int dano)
    {
        if (Time.time > nextAttack || ultimate)
        {
            animator.SetTrigger("Ataque");
            Collider2D[] inimigos = Physics2D.OverlapCircleAll(attackPoint.position,attackRange, enemyLayers);
            foreach (Collider2D i in inimigos)
            {
                i.GetComponent<IEnemy>().TakeHit(dano);
            }
            nextAttack = Time.time + attackRate;
        }
    }
    IEnumerator Ultimate()
    {
        for(int i = 0;i<duracaoUlt;i++)
        {
            Attack(true, 3);
            yield return new WaitForSeconds (0.4f);
        }
    }
    public void PlayerTakeDamage(int dmg)
    {
        hp -= dmg;
        hpBar.SetSlider(hp,maxHp);
        if(hp <= 0)
        {
            IsAlive = false;
            gameController.KillPlayer(this.gameObject);
        }
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
    void OnDrawGizmosSelected()
    {
        if(attackRange == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
}
