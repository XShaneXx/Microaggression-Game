using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PhoneDialogue : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        ResetDialogue();
        isDialogueboxOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(myDialoguebox.activeInHierarchy && !isDialogueboxOpen)
        {
            StartDialogue();
            isDialogueboxOpen = true;
        }

        if ((Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0)) && !isChoicePoint && !isTyping)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                isTyping = false;
            }
        }
    }

    public void SetStartLine(int startLine)
    {
        index = startLine;
    }

    public void StartDialogue()
    {
        isChoicePoint = false;
        choiceMade = false;
        isTyping = false;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = string.Empty; // Ensure text is cleared before typing
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
        if (!StorylineState.hasCheckedNote && index == 0)
        {
            // If the medical bottle hasn't been checked, end dialogue after the first line
            EndDialogue();
            return;
        }

        if (index < lines.Length - 1)
        {
            if (choiceMade)
            {
                index = targetIndex; // Use the public integer here
                choiceMade = false;
            }
            else
            {
                index++;
            }
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        gameObject.SetActive(false);
        ResetDialogue(); // Reset dialogue after it finishes
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

        // Adjust the dialogue index based on player choice
        index = choiceIndex;
        isChoicePoint = false;
        isTyping = false;
        StartCoroutine(TypeLine());

        // Set the flag to indicate a choice was made
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
