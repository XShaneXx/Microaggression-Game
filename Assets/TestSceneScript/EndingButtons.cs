using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndingButtons : MonoBehaviour
{
    public GameObject endingPanel; // Reference to the Ending panel
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshPro component for the ending text
    public GameObject testChoicePanel; // Reference to the choice panel that should be deactivated

    public void disarm()
    {
        ShowEnding("Ending 1/5 - Disarm the Microaggression");
    }

    public void educate()
    {
        ShowEnding("Ending 2/5 - Educate the Offender");
    }

    public void seek()
    {
        ShowEnding("Ending 3/5 - Seek External Intervention");
    }

    public void make()
    {
        ShowEnding("Ending 4/5 - Make the Invisible Visible");
    }

    public void wrongchoice()
    {
        ShowEnding("Ending 5/5 - You didn't help out Mr.Khan. Things happen again...");
    }

    private void ShowEnding(string endingText)
    {
        // Set active the ending panel
        endingPanel.SetActive(true);

        // Set the ending text
        dialogueText.text = endingText.Replace(" - ", "\n");

        // Deactivate the choice panel
        testChoicePanel.SetActive(false);
    }
}
