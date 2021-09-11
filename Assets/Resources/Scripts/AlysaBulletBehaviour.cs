using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlysaBulletBehaviour : MonoBehaviour
{
    [SerializeField]
    float projectileDmg;
    [SerializeField]
    float varSpeed = 15f;
    [SerializeField]
    float timeDestroy = 1f;
    public static int typeOfBullet = 0;
    public int typeOfSpawn;
    [SerializeField]
    ParticleSystem ps;
    [SerializeField]
    Sprite[] emojis;

    public float TimeDestroy { get => timeDestroy; private set => timeDestroy = value; }

    void Start()
    {
        ps = gameObject.GetComponentInChildren<ParticleSystem>();
        if(typeOfSpawn == 1)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = emojis[typeOfBullet];
            ps.textureSheetAnimation.SetSprite(0,emojis[typeOfBullet]);
        } else if(typeOfSpawn == 2)
        {
            int i = Random.Range(0,3);
            GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = emojis[i];
            ps.textureSheetAnimation.SetSprite(0,emojis[i]);
            TimeDestroy = 1.7f;
        }
        Invoke("DestroyGameObject", TimeDestroy);
    }
    void Update()
    {
        //função que move o projetil pra frente:
        transform.Translate(Vector2.right * varSpeed * Time.deltaTime);
    } 
    void OnTriggerEnter2D(Collider2D collision)
    {
        //serve pra definir oq é um inimigo
        // EnemyBehaviour enemy = collision.GetComponent<EnemyBehaviour>();
        if (collision.gameObject.CompareTag("Ground"))
        {
            DestroyGameObject();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //metodo do inimigo tomar dano
            // enemy.TakeHit(projectileDmg);
            DestroyGameObject();
        }
    }
    //metodo que destroi o objeto, mas mantem as particulas causando um melhor efeito visual
    void DestroyGameObject()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);
        foreach (Transform child in transform)
        {
            ps.Stop();
            Destroy(child.gameObject, TimeDestroy);
        }
        transform.DetachChildren();
        Destroy(gameObject);    
    }
}
