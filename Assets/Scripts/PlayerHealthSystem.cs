using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField]
    HealthBarBehaviour healthBar;
    float maxHealth = 100;
    float playerHealth;

    void Start()
    {
        playerHealth = maxHealth;
        healthBar.SetHealth(playerHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth > 70)
        AlysaBulletBehaviour.typeOfBullet = 0;
        else if(playerHealth>40)
        AlysaBulletBehaviour.typeOfBullet = 1;
        else if(playerHealth<=40)
        AlysaBulletBehaviour.typeOfBullet = 2;

        healthBar.SetHealth(playerHealth, maxHealth);
        if (playerHealth<=0)
        {
            Destroy(gameObject);
        }
    }
    public void PlayerTakeDamage(float dmg)
    {
        playerHealth -= dmg;
    }
}
