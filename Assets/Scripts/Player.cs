using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    public Animator animator;
    private float lrMoveIndex;
    private float udMoveIndex;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from WASD keys
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        // Animation
        lrMoveIndex = Input.GetAxisRaw("Horizontal");
        udMoveIndex = Input.GetAxisRaw("Vertical");

        animator.SetFloat("leftSpeed", moveVelocity.magnitude * lrMoveIndex);
        animator.SetFloat("rightSpeed", moveVelocity.magnitude * lrMoveIndex);
        animator.SetFloat("upSpeed", moveVelocity.magnitude * udMoveIndex);
        animator.SetFloat("downSpeed", moveVelocity.magnitude * udMoveIndex);

    }

    // FixedUpdate is called at a fixed interval
    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }


}
