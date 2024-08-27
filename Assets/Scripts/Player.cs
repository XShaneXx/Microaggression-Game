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
    public GameObject pressJ;
    public TextMeshProUGUI pressJText; // Reference to the TextMeshPro component

    private string[] messages = new string[]
    {
        "Press 'J' for interaction and dialogue",
        "NPCs and Items are available for interaction",
        "Find all 4 solutions to help Mrs. Lee!"
    };

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Start the coroutine to display messages
        StartCoroutine(DisplayPressJMessages());
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

    // Coroutine to display each message sequentially
    private IEnumerator DisplayPressJMessages()
    {
        foreach (string message in messages)
        {
            pressJText.text = message;
            pressJ.SetActive(true);
            yield return new WaitForSeconds(3f); // Display each message for 3 seconds
        }

        pressJ.SetActive(false); // Disable the pressJ GameObject after displaying all messages
    }
}
