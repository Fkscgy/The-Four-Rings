using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAttacks : MonoBehaviour
{
    float nextFire;
    [SerializeField]
    float fireRate;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform bulletSpawn;	
    bool isAttacking = false;
    
    void Update()
    {
        if (isAttacking && Time.time >nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject cloneCookie = Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
        }
    }
    public void OnAttackInput(InputAction.CallbackContext ctx)
    {
        isAttacking = ctx.action.triggered;
    }
}
