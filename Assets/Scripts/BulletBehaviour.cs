using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float projectileDmg; 
    public float varSpeed;
    public float timeDestroy;
    public static int typeOfBullet = 1;
    public Sprite[] emojis;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = emojis[typeOfBullet];
        gameObject.GetComponentInChildren<ParticleSystem>().textureSheetAnimation.SetSprite(0,emojis[typeOfBullet]);
    }
    
    void Update()
    {   
        Invoke("DestroyGameObject", timeDestroy);
        transform.Translate(Vector2.right * varSpeed * Time.deltaTime);
    } 
    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBehaviour enemy = collision.GetComponent<EnemyBehaviour>();
        if (collision.gameObject.CompareTag("Ground"))
        {
            DestroyGameObject();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
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
