using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float projectileDmg; 
    public float varSpeed;
    public float timeDestroy;
    public float range = 15f;
    public GameObject particulas;
    public static int typeOfBullet;
    public Sprite emoji1;
    public Sprite emoji2;
    public Sprite emoji3;
    // Start is called before the first frame update
    void Start()
    {
        if (typeOfBullet == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = emoji1;
            gameObject.GetComponentInChildren<ParticleSystem>().textureSheetAnimation.SetSprite(0,emoji1);
        }
        if (typeOfBullet == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = emoji2;
            gameObject.GetComponentInChildren<ParticleSystem>().textureSheetAnimation.SetSprite(0,emoji2);
        }
        if (typeOfBullet == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = emoji3;
            gameObject.GetComponentInChildren<ParticleSystem>().textureSheetAnimation.SetSprite(0,emoji3);
        }
    }
    // Update is called once per frame
    void Update()
    {   
        Invoke("DestroyGameObject", timeDestroy);
        transform.Translate(Vector2.right * varSpeed * Time.deltaTime);
        Debug.Log("a");
    } 
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("b");
        EnemyBehaviour enemy = collision.GetComponent<EnemyBehaviour>();
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("c");
            DestroyGameObject();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //enemy.TakeHit(projectileDmg);
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
