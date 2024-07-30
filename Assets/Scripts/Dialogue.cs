using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public Button[] choiceButtons;
    public int[] choiceIndices; // Add this array to specify choice points
    public int targetIndex; // Add this public integer to set the target index in the Inspector

    private int index;
    private bool isChoicePoint;
    private bool choiceMade; // Flag to indicate if a choice was made
    private bool isTyping; // Flag to indicate if text is being typed

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isChoicePoint && !isTyping)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        isChoicePoint = false;
        choiceMade = false;
        isTyping = false;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
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
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
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

        // Adjust the dialogue index based on player choice
        index = choiceIndex;
        textComponent.text = string.Empty;
        isChoicePoint = false;
        isTyping = false;
        StartCoroutine(TypeLine());

        // Set the flag to indicate a choice was made
        choiceMade = true;
    }
}
