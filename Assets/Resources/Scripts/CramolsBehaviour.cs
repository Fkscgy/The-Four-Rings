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
    [SerializeField] SliderBehaviour hpBar;
    [SerializeField] SliderBehaviour enBar;

    float varSpeed = 5f;
    float jumpForce = 10;
    Rigidbody2D rig;
    int maxHp = 10;
    int hp;
    int maxEn = 10;
    int en;
    float nextFire;
    float direction;
    bool isReady;

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
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        PlayerTakeDamage(1);
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
    void Jump(bool key)
    {
        if (key && IsGrounded())
        {
            rig.AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
        }
    }
    void Attack(bool key)
    {
        if(key)
        print("Ataque Cramols");
    }
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayers);
    }
    void UsingUltimate(bool key)
    {
        if(key)
        print("ult cramols");
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
