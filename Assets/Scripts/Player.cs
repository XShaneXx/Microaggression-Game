using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    public Animator animator;
    private float lrMoveIndex;
    private float udMoveIndex;

    public GameObject[] uiPanels;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsAnyPanelActive())
        {
            moveVelocity = Vector2.zero;
            animator.SetFloat("Speed", 0f);
            return;
        }

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        lrMoveIndex = Input.GetAxisRaw("Horizontal");
        udMoveIndex = Input.GetAxisRaw("Vertical");

        if (moveInput != Vector2.zero)
        {
            animator.SetFloat("Horizontal", lrMoveIndex);
            animator.SetFloat("Vertical", udMoveIndex);
        }

        animator.SetFloat("Speed", moveVelocity.magnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private bool IsAnyPanelActive()
    {
        foreach (GameObject panel in uiPanels)
        {
            if (panel.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }
}
