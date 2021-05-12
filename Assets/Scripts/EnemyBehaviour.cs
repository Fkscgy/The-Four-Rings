using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    public float HitPoints;
    public float MaxHitPoints;
    private Transform target;
    public float speed;
    public GameObject bullet;
    public Transform bulletPoint;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        HitPoints = MaxHitPoints;
    }
    void Update()   
    {
        if (Vector2.Distance(transform.position,target.position) > 6f)
        {
            transform.position = Vector2.MoveTowards(transform.position,target.position,speed*Time.deltaTime);
        } else if(Vector2.Distance(transform.position,target.position) < 6f && Vector2.Distance(transform.position,target.position) > 4f)
        {
            transform.position = this.transform.position;
        } else if (Vector2.Distance(transform.position,target.position) < 4f)
        {
            transform.position = Vector2.MoveTowards(transform.position,target.position,-speed*Time.deltaTime);
        }
        if (Vector2.Distance(transform.position,target.position) < 6f)
        {/*
            Vector3 dir = target.position - bulletPoint.position;
            float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            bulletPoint.rotation = Quaternion.Euler(0f,0f,rotZ);
            if (Time.time> nextFire)
            {
                Instantiate(bullet,bulletPoint.position,bulletPoint.rotation);
                nextFire = fireRate + Time.time;
            }*/
        }
    }
    public void TakeHit(float damage)
    {
        HitPoints -= damage;
        if (HitPoints <=0 )
        {
            //ScoreBehaviour.scoreValue += 10;
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerBehaviour player = collision.collider.GetComponent<PlayerBehaviour>();
        if (collision.collider.CompareTag("Player"))
        {
            player.PlayerDamage(30f);
        }
    }
}
