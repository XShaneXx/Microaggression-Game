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

    public GameObject doctor; // Reference to the doctor character
    public float doctorAnimationDelay = 35f;
    private Animator doctorAnimator; // Reference to the doctor's animator
    private bool doctorReachedFirstTarget = false; // Flag to check if the doctor has reached the first target

    private Vector3 doctorFirstTarget = new Vector3(-5.5f, 1.2f, 0f); // First target position for the doctor
    private Vector3 doctorSecondTarget = new Vector3(-5.5f, 11f, 0f); // Second target position for the doctor

    public GameObject doctorDialogue;

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

        nurseAnimator.SetFloat("Horizontal", -1f);

        if (doctor != null)
        {
            doctorAnimator = doctor.GetComponent<Animator>(); // Get the Animator component attached to the doctor
        }

        // Start the nurse's movement after a 10-second delay
        StartCoroutine(StartNurseMovement());

        // Start the doctor's movement after a 35-second delay
        StartCoroutine(StartDoctorMovement());
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

    IEnumerator StartDoctorMovement()
    {
        yield return new WaitForSeconds(doctorAnimationDelay); // Wait for 35 seconds

        doctorDialogue.SetActive(true);

        // Move the doctor towards the first target position
        while (!doctorReachedFirstTarget)
        {
            doctor.transform.position = Vector3.MoveTowards(doctor.transform.position, doctorFirstTarget, speed * Time.deltaTime);

            // Set doctor's animation to walk up (assuming up is `Vertical` direction)
            doctorAnimator.SetFloat("Vertical", 1f);
            doctorAnimator.SetFloat("Speed", 1f);

            if (Vector3.Distance(doctor.transform.position, doctorFirstTarget) < 0.1f)
            {
                doctorReachedFirstTarget = true;
                doctorAnimator.SetFloat("Speed", 0f); // Stop the doctor for 5 seconds
                yield return new WaitForSeconds(5f); // Wait for 5 seconds at the first target
            }

            yield return null;
        }

        // Move the doctor towards the second target position
        while (doctorReachedFirstTarget)
        {
            doctor.transform.position = Vector3.MoveTowards(doctor.transform.position, doctorSecondTarget, speed * Time.deltaTime);

            // Keep doctor's animation to walk up
            doctorAnimator.SetFloat("Vertical", 1f);
            doctorAnimator.SetFloat("Speed", 1f);

            if (Vector3.Distance(doctor.transform.position, doctorSecondTarget) < 0.1f)
            {
                doctorAnimator.SetFloat("Speed", 0f); // Stop the doctor's walking animation
                doctorReachedFirstTarget = false; // Mark the movement as completed
            }

            yield return null;
        }
    }
}
