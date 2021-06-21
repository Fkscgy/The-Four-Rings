using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float varSpeed = 5f;

    void Start()
    {
        Destroy(gameObject,1f);
    }

    void Update()
    {
        transform.Translate(Vector2.right * varSpeed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer player = collision.GetComponent<IPlayer>();
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Enemy"))
        {
            //metodo do inimigo tomar dano
            player.PlayerTakeDamage(1f);
            Destroy(gameObject);
        }
    }
}
