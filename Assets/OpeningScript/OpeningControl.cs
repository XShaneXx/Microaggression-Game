using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningControl : MonoBehaviour
{
    public GameObject player; // Reference to the player character
    public float speed = 2f; // Speed of the player movement
    private Animator playerAnimator; // Reference to the player's animator

    private Vector3 targetPosition = new Vector3(-6.43f, 2.15f, 0f); // Target position for the player
    private bool hasReachedTarget = false; // Flag to check if the player has reached the target

    public GameObject dialogueOne; // Reference to the dialogueOne GameObject

    public GameObject nurse; // Reference to the nurse character
    public float nurseAnimationDelay = 10f;
    private Animator nurseAnimator; // Reference to the nurse's animator
    private bool nurseMovingToFirstTarget = true; // Flag to control nurse's movement
    private bool nurseReachedFirstTarget = false; // Flag to check if the nurse has reached the first target

    private Vector3 nurseFirstTarget = new Vector3(-5.5f, 1.2f, 0f); // First target position for the nurse
    private Vector3 nurseSecondTarget = new Vector3(-5.5f, -7f, 0f); // Second target position for the nurse

    // Start is called before the first frame update
    void Start()
    {
        if (player != null)
        {
            playerAnimator = player.GetComponent<Animator>(); // Get the Animator component attached to the player
        }

        if (nurse != null)
        {
            nurseAnimator = nurse.GetComponent<Animator>(); // Get the Animator component attached to the nurse
        }

        // Start the nurse's movement after a 10-second delay
        StartCoroutine(StartNurseMovement());
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasReachedTarget)
        {
            MovePlayerToTarget();
        }
    }

    void MovePlayerToTarget()
    {
        // Move the player towards the target position
        player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the player has reached the target position
        if (Vector3.Distance(player.transform.position, targetPosition) < 0.1f)
        {
            hasReachedTarget = true;
            // Stop player movement and face left
            playerAnimator.SetFloat("Horizontal", -1f);
            playerAnimator.SetFloat("Speed", 0f); // Stop walking animation

            // Activate dialogueOne and update the flag
            dialogueOne.SetActive(true);
        }
        else
        {
            // If the player is still moving, set the animation to walking vertically
            playerAnimator.SetFloat("Horizontal", 0f); // Ensure the player walks vertically
            playerAnimator.SetFloat("Speed", 1f); // Play walking animation
        }
    }

    IEnumerator StartNurseMovement()
    {
        yield return new WaitForSeconds(nurseAnimationDelay); // Wait for 10 seconds

        while (!nurseReachedFirstTarget)
        {
            // Move the nurse towards the first target position
            nurse.transform.position = Vector3.MoveTowards(nurse.transform.position, nurseFirstTarget, speed * Time.deltaTime);

            // Set nurse's animation to walk right
            nurseAnimator.SetFloat("Horizontal", 1f);
            nurseAnimator.SetFloat("Speed", 1f);

            if (Vector3.Distance(nurse.transform.position, nurseFirstTarget) < 0.1f)
            {
                nurseReachedFirstTarget = true;
                nurseMovingToFirstTarget = false; // Switch to moving to the second target
            }

            yield return null;
        }

        while (!nurseMovingToFirstTarget)
        {
            // Move the nurse towards the second target position
            nurse.transform.position = Vector3.MoveTowards(nurse.transform.position, nurseSecondTarget, speed * Time.deltaTime);

            // Keep nurse's animation to walk down (assuming down is `Vertical` direction)
            nurseAnimator.SetFloat("Horizontal", 0f);
            nurseAnimator.SetFloat("Speed", 1f);

            if (Vector3.Distance(nurse.transform.position, nurseSecondTarget) < 0.1f)
            {
                nurseMovingToFirstTarget = true; // Mark the movement as completed
                nurseAnimator.SetFloat("Speed", 0f); // Stop the nurse's walking animation
            }

            yield return null;
        }
    }
}
