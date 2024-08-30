using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OpeningControl : MonoBehaviour
{
    public GameObject player;
    public float speed = 2f;
    private Animator playerAnimator;

    private Vector3 targetPosition = new Vector3(-6.43f, 2.15f, 0f);
    private bool hasReachedTarget = false;

    public GameObject dialogueOne;

    public GameObject nurse;
    public float nurseAnimationDelay = 10f;
    private Animator nurseAnimator;
    private bool nurseMovingToFirstTarget = true;
    private bool nurseReachedFirstTarget = false;

    private Vector3 nurseFirstTarget = new Vector3(-5.5f, 1.2f, 0f);
    private Vector3 nurseSecondTarget = new Vector3(-5.5f, -7f, 0f);

    public GameObject doctor;
    public float doctorAnimationDelay = 35f;
    private Animator doctorAnimator;
    private bool doctorReachedFirstTarget = false;

    private Vector3 doctorFirstTarget = new Vector3(-5.5f, 1.2f, 0f);
    private Vector3 doctorSecondTarget = new Vector3(-5.5f, 11f, 0f);

    public GameObject doctorDialogue;
    public GameObject blackScene;
    public GameObject text1;
    public GameObject text2;

    void Start()
    {
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

        StartCoroutine(StartNurseMovement());
        StartCoroutine(StartDoctorMovement());
    }

    void Update()
    {
        if (!hasReachedTarget)
        {
            MovePlayerToTarget();
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
            dialogueOne.SetActive(true);
        }
        else
        {
            playerAnimator.SetFloat("Horizontal", 0f);
            playerAnimator.SetFloat("Speed", 1f);
        }
    }

    IEnumerator StartNurseMovement()
    {
        yield return new WaitForSeconds(nurseAnimationDelay);

        while (!nurseReachedFirstTarget)
        {
            nurse.transform.position = Vector3.MoveTowards(nurse.transform.position, nurseFirstTarget, speed * Time.deltaTime);

            nurseAnimator.SetFloat("Horizontal", 1f);
            nurseAnimator.SetFloat("Speed", 1f);

            if (Vector3.Distance(nurse.transform.position, nurseFirstTarget) < 0.1f)
            {
                nurseReachedFirstTarget = true;
                nurseMovingToFirstTarget = false;
            }

            yield return null;
        }

        while (!nurseMovingToFirstTarget)
        {
            nurse.transform.position = Vector3.MoveTowards(nurse.transform.position, nurseSecondTarget, speed * Time.deltaTime);

            nurseAnimator.SetFloat("Horizontal", 0f);
            nurseAnimator.SetFloat("Speed", 1f);

            if (Vector3.Distance(nurse.transform.position, nurseSecondTarget) < 0.1f)
            {
                nurseMovingToFirstTarget = true;
                nurseAnimator.SetFloat("Speed", 0f);
            }

            yield return null;
        }
    }

    IEnumerator StartDoctorMovement()
    {
        yield return new WaitForSeconds(doctorAnimationDelay);

        doctorDialogue.SetActive(true);

        while (!doctorReachedFirstTarget)
        {
            doctor.transform.position = Vector3.MoveTowards(doctor.transform.position, doctorFirstTarget, speed * Time.deltaTime);

            doctorAnimator.SetFloat("Vertical", 1f);
            doctorAnimator.SetFloat("Speed", 1f);

            if (Vector3.Distance(doctor.transform.position, doctorFirstTarget) < 0.1f)
            {
                doctorReachedFirstTarget = true;
                doctorAnimator.SetFloat("Speed", 0f);
                yield return new WaitForSeconds(5f);
            }

            yield return null;
        }

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

        Image blackSceneImage = blackScene.GetComponent<Image>();
        TextMeshProUGUI text1Component = text1.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI text2Component = text2.GetComponent<TextMeshProUGUI>();

        Color blackSceneColor = blackSceneImage.color;
        Color text1Color = text1Component.color;
        Color text2Color = text2Component.color;

        float elapsedTime = 0f;
        float duration = 3f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            blackSceneColor.a = Mathf.Clamp01(elapsedTime / duration);
            text1Color.a = Mathf.Clamp01(elapsedTime / duration);
            text2Color.a = Mathf.Clamp01(elapsedTime / duration);

            blackSceneImage.color = blackSceneColor;
            text1Component.color = text1Color;
            text2Component.color = text2Color;

            yield return null;
        }

        yield return new WaitForSeconds(15f);

        SceneManager.LoadScene("MainScene");
    }
}
