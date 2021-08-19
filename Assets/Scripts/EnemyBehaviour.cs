using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    float HitPoints,MaxHitPoints = 10f;
    [SerializeField]
    Transform target;
    float range = 5f;
    [SerializeField]
    Transform firePoint;
    float fireCoolDown,Reload = .2f;
    [SerializeField]
    GameObject bulletPrefab;
    
    void Start()
    {
        HitPoints = MaxHitPoints;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void Update()   
    {
        if(target != null)
        {
            Shooting();
        }
    }
    public void TakeHit(float damage)
    {
        HitPoints -= damage;
        if (HitPoints <=0 )
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        IPlayer player = collision.collider.GetComponent<IPlayer>();
        if (collision.collider.CompareTag("Player"))
        {
            player.PlayerTakeDamage(1);
        }
    }
    void UpdateTarget()
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
            target = nearPlayer.transform;
        } else 
        {
            target = null;
        }
    }
    void LookTarget()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0f,0f,angle);
    }
    void Shoot()
    {
        Instantiate(bulletPrefab,firePoint.position, firePoint.rotation);
    }
    void Shooting()
    {
        LookTarget();
        if(Time.time > fireCoolDown)
        {
            Shoot();
            fireCoolDown = Time.time + Reload;
        }
    }
}
