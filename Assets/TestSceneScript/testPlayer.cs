using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayer : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    public Animator animator;
    private float lrMoveIndex;
    private float udMoveIndex;

    public GameObject[] uiPanels;
    public AudioSource walkingSound; // Reference to the walking sound AudioSource

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (walkingSound == null)
        {
            walkingSound = GetComponent<AudioSource>(); // Get the AudioSource component if not assigned in Inspector
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if any UI panel is active
        if (IsAnyPanelActive())
        {
            // If a panel is active, don't allow movement
            moveVelocity = Vector2.zero;
            animator.SetFloat("Speed", 0f);
            StopWalkingSound();
            return;
        }

        // Get input from WASD keys
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        // Animation
        lrMoveIndex = Input.GetAxisRaw("Horizontal");
        udMoveIndex = Input.GetAxisRaw("Vertical");

        if (moveInput != Vector2.zero)
        {
            animator.SetFloat("Horizontal", lrMoveIndex);
            animator.SetFloat("Vertical", udMoveIndex);
            PlayWalkingSound();
        }
        else
        {
            StopWalkingSound();
        }

        animator.SetFloat("Speed", moveVelocity.magnitude);
    }

    // FixedUpdate is called at a fixed interval
    void FixedUpdate()
    {
        // Move the player
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

    // Method to play the walking sound
    private void PlayWalkingSound()
    {
        if (!walkingSound.isPlaying)
        {
            walkingSound.Play();
        }
    }

    // Method to stop the walking sound
    private void StopWalkingSound()
    {
        if (walkingSound.isPlaying)
        {
            walkingSound.Stop();
        }
    }
}
