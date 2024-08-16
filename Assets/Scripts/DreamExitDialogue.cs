using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Required to manage scene transitions

public class DreamExitDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public Button[] choiceButtons;
    public int[] choiceIndices; // Array to specify choice points
    public int targetIndex; // Public integer to set the target index in the Inspector

    private int index;
    private bool isChoicePoint;
    private bool choiceMade; // Flag to indicate if a choice was made
    private bool isTyping; // Flag to indicate if text is being typed
    public GameObject dialoguePanel;

    // Start is called before the first frame update
    void Start()
    {
        ResetDialogue();
    }

    // Update is called once per frame
    void Update()
    {
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
            EndDialogue(); // End the dialogue when the last line is reached
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
        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    // Method for the "No" button
    public void OnNoButtonPressed()
    {
        dialoguePanel.SetActive(false); // Close the dialogue panel
        ResetDialogue(); // Reset the dialogue index and state
    }

    // Method for the "Yes" button
    public void OnYesButtonPressed()
    {
        if (StorylineState.endingMIV && StorylineState.endingEO && StorylineState.endingDM && StorylineState.endingSEI)
        {
            // If all endings are completed, transition to the next scene
            SceneManager.LoadScene("TestScene");
        }
        else
        {
            foreach (Button button in choiceButtons)
            {
                button.gameObject.SetActive(false);
            }
            NextLine();
        }
    }
}
