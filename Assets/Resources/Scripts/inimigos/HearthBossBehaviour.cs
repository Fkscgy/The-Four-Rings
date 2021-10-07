using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthBossBehaviour : MonoBehaviour,IEnemy
{
    [SerializeField] int meleeDmg;
    [SerializeField] 
    Transform meleeAttackPoint;
    [SerializeField]
    Transform magicAttackPoint;
    [SerializeField]
    GameObject prefabMagia;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask playerLayers;
    int HitPoints,MaxHitPoints = 10;
    [SerializeField]
    float minDist;
    float range = 40f;
    Animator animator;
    Rigidbody2D rb;
    bool facingRight = true;
    [SerializeField]
    SliderBehaviour barra;
    [SerializeField]
    GameObject ganhou;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        HitPoints = MaxHitPoints;
        barra.SetSlider(HitPoints,MaxHitPoints);
    }
    
    public void Flip()
    {
        if(facingRight && transform.position.x > UpdateTarget().position.x)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
            facingRight = false;
        }
        else if(!facingRight && transform.position.x < UpdateTarget().position.x)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
            facingRight = true;
        }
    }
    public void TakeHit(int damage)
    {
        HitPoints -= damage;
        if(HitPoints<= 5)
        {
            animator.SetBool("Quebrou", true);
        }
        if (HitPoints <=0 )
        {
            Instantiate(ganhou);
            Destroy(gameObject);
        }
        barra.SetSlider(HitPoints,MaxHitPoints);
    }
    public void MeleeAttack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] players = Physics2D.OverlapCircleAll(meleeAttackPoint.position,attackRange, playerLayers);
        foreach (Collider2D player in players)
        {
            player.GetComponent<IPlayer>().PlayerTakeDamage(meleeDmg);
        }
    }
    public void MeleeAttackQuebrado()
    {
        animator.SetTrigger("Attack2");
        Collider2D[] players = Physics2D.OverlapCircleAll(meleeAttackPoint.position,attackRange, playerLayers);
        foreach (Collider2D player in players)
        {
            player.GetComponent<IPlayer>().PlayerTakeDamage(meleeDmg);
        }
    }
    public void MagicAttack()
    {
        LookTarget(UpdateTarget());
        Instantiate(prefabMagia,magicAttackPoint.transform.position,magicAttackPoint.transform.rotation);
    }
    public IEnumerator MagicAttackQuebrado()
    {
        LookTarget(UpdateTarget());
        for(int i =0;i<2;i++)
        {
            Instantiate(prefabMagia,magicAttackPoint.transform.position,magicAttackPoint.transform.rotation);
            yield return new WaitForSeconds(0.5f);
        }
    }
    void LookTarget(Transform player)
    {
        if(player == null)
        return;
        Vector3 dir = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg;
        magicAttackPoint.rotation = Quaternion.Euler(0f,0f,angle);
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
        Gizmos.DrawWireSphere(meleeAttackPoint.position,attackRange);
    }
}
