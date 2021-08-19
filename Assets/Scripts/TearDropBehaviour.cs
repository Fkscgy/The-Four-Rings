using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearDropBehaviour : MonoBehaviour
{
    [SerializeField]
    Transform target;
    float range = 15f;
    void Start()
    {
        
    }

    void Update()
    {
        UpdateTarget();
    }
    void UpdateTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float shortestDistance = Mathf.Infinity;
        GameObject nearPlayer = null;
        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if(distanceToPlayer < shortestDistance)
            {
                shortestDistance = distanceToPlayer;
                nearPlayer = player;
            }
        }
        if(nearPlayer != null && shortestDistance <= range)
        {
            target = nearPlayer.transform;
        } else 
        {
            target = null;
        }
    }
}
