using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField]
    HealthBarBehaviour healthBar;
    float maxHealth = 100;
    static float playerHealth;

    void Start()
    {
        playerHealth = maxHealth;
        healthBar.SetHealth(playerHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
        healthBar.SetHealth(playerHealth, maxHealth);
    }
    public void playerTakeDamage(float dmg)
    {
        playerHealth -= dmg;
    }
}
