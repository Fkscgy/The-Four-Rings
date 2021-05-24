using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviments : MonoBehaviour
{
    float varSpeed = 5;
    float jumpForce = 10;
    bool grounded;
    Rigidbody2D rig;
    [SerializeField]    
    Transform groundCheck;
    [SerializeField]
    LayerMask groundLayers;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayers))
        {
            grounded = true;
        }
        transform.position += new Vector3(Input.GetAxis("Horizontal")*varSpeed*Time.deltaTime,0f,0f);
    }
    void Update()
    {
        Jump();
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Debug.Log("a");
            grounded = false;
            rig.velocity = Vector2.up * jumpForce;
        }
    }
}