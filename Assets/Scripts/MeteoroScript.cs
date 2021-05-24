using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoroScript : MonoBehaviour
{
    float projectileDmg = 10f;
    float varSpeed = 15f;
    float timeDestroy = 1f;
    [SerializeField]
    Sprite[] emojis;
    int aleatorio;
    
    void Start()
    {
        aleatorio = Random.Range(0,3);
        gameObject.GetComponent<SpriteRenderer>().sprite = emojis[aleatorio];
        gameObject.GetComponentInChildren<ParticleSystem>().textureSheetAnimation.SetSprite(0,emojis[aleatorio]);
        Invoke("DestroyGameObject", timeDestroy);
    }

    void Update()
    {
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
