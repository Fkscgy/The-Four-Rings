using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    public float HitPoints;
    public float MaxHitPoints;
    public HealthBarBehaviour HealthBar;
    
    void Start()
    {
        HitPoints = MaxHitPoints;
        HealthBar.SetHealth(HitPoints,MaxHitPoints);
    }
    void Update()   
    {
        HealthBar.SetHealth(HitPoints,MaxHitPoints);
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
        PlayerOneBehaviour player = collision.collider.GetComponent<PlayerOneBehaviour>();
        if (collision.collider.CompareTag("Player"))
        {
            player.PlayerTakeDamage(10f);
        }
    }
}
