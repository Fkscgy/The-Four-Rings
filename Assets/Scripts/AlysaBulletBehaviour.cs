using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlysaBulletBehaviour : MonoBehaviour
{
    float projectileDmg = 10f;
    float varSpeed = 15f;
    float timeDestroy = 1f;
    public static int typeOfBullet;
    [SerializeField]
    Sprite[] emojis;

    void Start()
    {
        //define o sprite da particula e do objeto da bala da alysa, esse sprites estão dentro de um array, e o indicie é a variavel global typeOfBullet que é modificidada no codigo do player
        gameObject.GetComponent<SpriteRenderer>().sprite = emojis[typeOfBullet];
        gameObject.GetComponentInChildren<ParticleSystem>().textureSheetAnimation.SetSprite(0,emojis[typeOfBullet]);
        Invoke("DestroyGameObject", timeDestroy);
    }
    
    void Update()
    {
        //função que move o projetil pra frente:
        transform.Translate(Vector2.right * varSpeed * Time.deltaTime);
    } 
    void OnTriggerEnter2D(Collider2D collision)
    {
        //serve pra definir oq é um inimigo
        EnemyBehaviour enemy = collision.GetComponent<EnemyBehaviour>();
        if (collision.gameObject.CompareTag("Ground"))
        {
            DestroyGameObject();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //metodo do inimigo tomar dano
            enemy.TakeHit(projectileDmg);
            DestroyGameObject();
        }
    }
    //metodo que destroi o objeto, mas mantem as particulas causando um melhor efeito visual
    void DestroyGameObject()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<ParticleSystem>().Stop();
            Destroy(child.gameObject, timeDestroy);
        }
        transform.DetachChildren();
        Destroy(gameObject);
    }
}
