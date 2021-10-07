using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiaBossBehaviour : MonoBehaviour
{
    [SerializeField]
    float varSpeed;
    [SerializeField]
    int projectileDmg;
    float TimeDestroy = 2f;
    void Start()
    {
        Destroy(this.gameObject,TimeDestroy);
    }
    void Update()
    {
        transform.Translate(Vector2.right * varSpeed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<IPlayer>().PlayerTakeDamage(projectileDmg);
            Destroy(this.gameObject);
        }
    }
}
