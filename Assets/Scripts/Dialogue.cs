using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject myDialoguebox;
    private bool isDialogueboxOpen;
    public string[] lines;
    public float textSpeed;
    public Button[] choiceButtons;
    public int[] choiceIndices; // Array to specify choice points
    public int targetIndex; // Public integer to set the target index in the Inspector

    private int index;
    private bool isChoicePoint;
    private bool choiceMade; // Flag to indicate if a choice was made
    private bool isTyping; // Flag to indicate if text is being typed

    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip dialogueAdvanceSound; // Sound effect for advancing dialogue

    void Start()
    {
        ResetDialogue();
        isDialogueboxOpen = false;
    }

    void Update()
    {
        if (myDialoguebox.activeInHierarchy && !isDialogueboxOpen)
        {
            StartDialogue();
            isDialogueboxOpen = true;
        }

        if ((Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0)))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                isTyping = false;
                CheckForChoicePoint();
            }
            else if (!isChoicePoint)
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        isChoicePoint = false;
        choiceMade = false;
        isTyping = false;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = string.Empty;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
        CheckForChoicePoint();
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            if (choiceMade)
            {
                index = targetIndex;
                choiceMade = false;
            }
            else
            {
                index++;
            }

            // Play the dialogue advance sound
            if (audioSource != null && dialogueAdvanceSound != null)
            {
                audioSource.PlayOneShot(dialogueAdvanceSound);
            }

            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            ResetDialogue();
        }
    }

    void CheckForChoicePoint()
    {
        isChoicePoint = System.Array.Exists(choiceIndices, element => element == index);
        if (isChoicePoint)
        {
            DisplayChoices();
        }
    }

    void DisplayChoices()
    {
        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(true);
        }
    }

    public void ChooseOption(int choiceIndex)
    {
        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }

        index = choiceIndex;
        isChoicePoint = false;
        isTyping = false;
        StartCoroutine(TypeLine());
        choiceMade = true;
    }

    public void ResetDialogue()
    {
        index = 0;
        isChoicePoint = false;
        choiceMade = false;
        isTyping = false;
        textComponent.text = string.Empty;
        isDialogueboxOpen = false;
        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
