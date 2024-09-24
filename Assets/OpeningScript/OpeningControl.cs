using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OpeningControl : MonoBehaviour
{
    public GameObject player;
    public float speed = 2f; // General speed for player and other characters
    public float nurseSpeed = 1f; // Separate speed for nurse
    private Animator playerAnimator;

    private Vector3 targetPosition = new Vector3(-6.43f, 2.15f, 0f);
    private bool hasReachedTarget = false;

    public GameObject dialogueOne;

    public GameObject nurse;
    private Animator nurseAnimator;
    private bool nurseReachedFirstTarget = false;
    private bool nurseReachedSecondTarget = false;

    private Vector3 nurseFirstTarget = new Vector3(-5.5f, 1.2f, 0f);
    private Vector3 nurseSecondTarget = new Vector3(-5.5f, -7f, 0f);

    public GameObject doctor;
    private Animator doctorAnimator;
    private bool doctorReachedFirstTarget = false;

    private Vector3 doctorFirstTarget = new Vector3(-5.5f, 1.2f, 0f);
    private Vector3 doctorSecondTarget = new Vector3(-5.5f, 11f, 0f);

    public GameObject doctorDialogue;
    public GameObject blackScene;
    public GameObject text1;
    public GameObject text2;
    public GameObject presskey;

    private bool dialogueNurseshow;
    private bool dialogueDoctorshow;

    private bool dialogueOneShow;
    private bool doctorDialogueshow;

    private bool nurseMove = false;
    private bool doctorMove1 = false;
    private bool doctorMove2 = false;

    void Start()
    {
        dialogueNurseshow = false;
        dialogueDoctorshow = false;

        dialogueOneShow = false;
        doctorDialogueshow = false;

        if (player != null)
        {
            playerAnimator = player.GetComponent<Animator>();
        }

        if (nurse != null)
        {
            nurseAnimator = nurse.GetComponent<Animator>();
        }

        if (doctor != null)
        {
            doctorAnimator = doctor.GetComponent<Animator>();
        }

        nurseAnimator.SetFloat("Horizontal", -1f);
    }

    void Update()
    {
        // Check if player has reached the target position
        if (!hasReachedTarget)
        {
            MovePlayerToTarget();
        }

        // Check if the dialogueOne is closed and trigger nurse movement
        if (!dialogueOne.activeInHierarchy && !dialogueNurseshow && dialogueOneShow)
        {
            OnDialogueNurseClosed(); // Trigger nurse movement when dialogueOne is closed
        }

        if (!doctorDialogue.activeInHierarchy && !dialogueDoctorshow && doctorDialogueshow)
        {
            OnDialogueDoctorClosed();
        }

        // Start nurse movement once dialogueNurseshow becomes true
        if (dialogueNurseshow && !nurseReachedFirstTarget && !nurseMove)
        {
            StartCoroutine(StartNurseMovement());
            nurseMove = true;
        }

        // Start doctor movement after nurse reaches its second target
        if (nurseReachedSecondTarget && !doctorReachedFirstTarget && !doctorMove1)
        {
            StartCoroutine(StartDoctorMovement());
            doctorMove1 = true;
        }

        // Resume doctor movement after dialogue is closed
        if (doctorReachedFirstTarget && dialogueDoctorshow && !doctorMove2)
        {
            StartCoroutine(ContinueDoctorMovement());
            doctorMove2 = true;
        }
    }

    void MovePlayerToTarget()
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(player.transform.position, targetPosition) < 0.1f)
        {
            hasReachedTarget = true;
            playerAnimator.SetFloat("Horizontal", -1f);
            playerAnimator.SetFloat("Speed", 0f);
            dialogueOne.SetActive(true); // Show the dialogue when player reaches the target
            dialogueOneShow = true;
        }
        else
        {
            playerAnimator.SetFloat("Horizontal", 0f);
            playerAnimator.SetFloat("Speed", 1f);
        }
    }

    IEnumerator StartNurseMovement()
    {
        // Start nurse movement to the first target
        while (!nurseReachedFirstTarget)
        {
            nurse.transform.position = Vector3.MoveTowards(nurse.transform.position, nurseFirstTarget, nurseSpeed * Time.deltaTime);

            nurseAnimator.SetFloat("Horizontal", 1f);
            nurseAnimator.SetFloat("Speed", 1f);

            if (Vector3.Distance(nurse.transform.position, nurseFirstTarget) < 0.1f)
            {
                nurseReachedFirstTarget = true;
                nurseAnimator.SetFloat("Speed", 0f);
            }

            yield return null;
        }

        // Start nurse movement to the second target after reaching the first target
        while (!nurseReachedSecondTarget)
        {
            nurse.transform.position = Vector3.MoveTowards(nurse.transform.position, nurseSecondTarget, nurseSpeed * Time.deltaTime);

            nurseAnimator.SetFloat("Horizontal", 0f);
            nurseAnimator.SetFloat("Speed", 1f);

            if (Vector3.Distance(nurse.transform.position, nurseSecondTarget) < 0.1f)
            {
                nurseReachedSecondTarget = true;
                nurseAnimator.SetFloat("Speed", 0f);

                // Now that the nurse has reached the second target, the doctor can start moving
            }

            yield return null;
        }
    }

    IEnumerator StartDoctorMovement()
    {
        // Start doctor movement to the first target after the nurse reaches its second position
        while (!doctorReachedFirstTarget)
        {
            doctor.transform.position = Vector3.MoveTowards(doctor.transform.position, doctorFirstTarget, speed * Time.deltaTime);

            doctorAnimator.SetFloat("Vertical", 1f);
            doctorAnimator.SetFloat("Speed", 1f);

            if (Vector3.Distance(doctor.transform.position, doctorFirstTarget) < 0.1f)
            {
                doctorReachedFirstTarget = true;
                doctorAnimator.SetFloat("Speed", 0f);

                // Show doctor dialogue when it reaches the first target
                doctorDialogue.SetActive(true);
                doctorDialogueshow = true;
            }

            yield return null;
        }
    }

    IEnumerator ContinueDoctorMovement()
    {
        // Start doctor movement to the second target after the dialogue is closed
        while (doctorReachedFirstTarget)
        {
            doctor.transform.position = Vector3.MoveTowards(doctor.transform.position, doctorSecondTarget, speed * Time.deltaTime);

            doctorAnimator.SetFloat("Vertical", 1f);
            doctorAnimator.SetFloat("Speed", 1f);

            if (Vector3.Distance(doctor.transform.position, doctorSecondTarget) < 0.1f)
            {
                doctorAnimator.SetFloat("Speed", 0f);
                doctorReachedFirstTarget = false;
                StartCoroutine(FadeInElements());
            }

            yield return null;
        }
    }

    IEnumerator FadeInElements()
    {
        blackScene.SetActive(true);
        text1.SetActive(true);
        text2.SetActive(true);
        presskey.SetActive(true); // Activate presskey for the fade-in effect

        Image blackSceneImage = blackScene.GetComponent<Image>();
        TextMeshProUGUI text1Component = text1.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI text2Component = text2.GetComponent<TextMeshProUGUI>();
        Image presskeyImage = presskey.GetComponent<Image>(); // Get the Image component for presskey

        Color blackSceneColor = blackSceneImage.color;
        Color text1Color = text1Component.color;
        Color text2Color = text2Component.color;
        Color presskeyColor = presskeyImage.color; // Use Color for the Image component

        float elapsedTime = 0f;
        float duration = 3f;

        // Fade in black screen, texts, and presskey
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            blackSceneColor.a = Mathf.Clamp01(elapsedTime / duration);
            text1Color.a = Mathf.Clamp01(elapsedTime / duration);
            text2Color.a = Mathf.Clamp01(elapsedTime / duration);
            presskeyColor.a = Mathf.Clamp01(elapsedTime / duration); // Apply fade-in effect for presskey Image

            blackSceneImage.color = blackSceneColor;
            text1Component.color = text1Color;
            text2Component.color = text2Color;
            presskeyImage.color = presskeyColor; // Update presskey Image color

            yield return null;
        }

        // Wait for player input (left mouse click or 'J' key)
        yield return StartCoroutine(WaitForPlayerInput());

        // After input, load the next scene
        SceneManager.LoadScene("MainScene");
    }

    IEnumerator WaitForPlayerInput()
    {
        // Keep checking for player input
        while (!Input.GetKeyDown(KeyCode.J) && !Input.GetMouseButtonDown(0))
        {
            yield return null; // Wait until the next frame
        }
    }

    // Trigger nurse movement once dialogueOne is closed
    public void OnDialogueNurseClosed()
    {
        dialogueNurseshow = true; // Nurse movement will start in Update
    }

    // Trigger doctor movement once the dialogue is closed
    public void OnDialogueDoctorClosed()
    {
        dialogueDoctorshow = true; // Resume doctor movement in Update
    }
}
