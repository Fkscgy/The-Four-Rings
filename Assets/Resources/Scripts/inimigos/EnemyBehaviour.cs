using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour , IEnemy
{
    [SerializeField] int dano;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask playerLayers;
    [SerializeField] float speed;
    [SerializeField] float attackRate;
    [SerializeField] GameObject[] bonus;
    int HitPoints,MaxHitPoints = 1;
    [SerializeField]
    Transform target;
    [SerializeField]
    float minDist;
    float range = 40f;
    float fireCoolDown,Reload = .2f;
    Animator animator;
    Rigidbody2D rb;
    float nextAttack;
    bool facingRight = true;

    public Transform Target { get => target; set => target = value; }

    void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        HitPoints = MaxHitPoints;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    
    public void FlipGota()
    {
        if(facingRight && transform.position.x < UpdateTarget().position.x)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
            facingRight = false;
        }
        else if(!facingRight && transform.position.x > UpdateTarget().position.x)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
            facingRight = true;
        }
    }
    public void TakeHit(int damage)
    {
        HitPoints -= damage;
        if (HitPoints <=0 )
        {
            if(Random.Range(0,5)<3)
            {
                foreach (GameObject b in bonus)
                {
                    Instantiate(b,transform);
                }
            }
            Destroy(gameObject);
        }
    }
    public void Attack()
    {
        // animator.SetTrigger("Atacar");
        Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.position,attackRange, playerLayers);
        foreach (Collider2D player in players)
        {
            player.GetComponent<IPlayer>().PlayerTakeDamage(dano);
        }
    }
    public Transform UpdateTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float shortestDistance = Mathf.Infinity;
        GameObject nearPlayer = null;
        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if(distanceToPlayer < shortestDistance)
            {
                shortestDistance = distanceToPlayer;
                nearPlayer = player;
            }
        }
        if(nearPlayer != null && shortestDistance <= range)
        {
            return nearPlayer.transform;
        } else 
        {
            return null;
        }
    }
    void OnDrawGizmosSelected()
    {
        if(attackRange == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
}
