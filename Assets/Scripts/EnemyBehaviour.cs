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

    // Start is called before the first frame update
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
        PlayerBehaviour player = collision.collider.GetComponent<PlayerBehaviour>();
        if (collision.collider.CompareTag("Player"))
        {
            player.PlayerDamage(30f);
        }
    }
}
