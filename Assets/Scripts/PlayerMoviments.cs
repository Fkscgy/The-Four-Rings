using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoviments : MonoBehaviour
{
    Vector2 movementInput;
    float varSpeed = 5;
    float jumpForce = 10;
    bool grounded;
    Rigidbody2D rig;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayers;
    bool isJumping= false;

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
        transform.position += new Vector3(movementInput.x*varSpeed*Time.deltaTime,0f,0f);
    }
    void Update()
    {
        if (isJumping && grounded)
        {
            grounded = false;
            rig.velocity = Vector2.up * jumpForce;
        }
    }
    public void OnMoveInput(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }
    public void OnJumpInput(InputAction.CallbackContext ctx)
    {
        isJumping = ctx.action.triggered;
    }
}