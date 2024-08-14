using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Endings : MonoBehaviour
{
    public GameObject endingMIVDialogue; // Reference to the UI panel
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshPro component

    public void EndingMIV()
    {
        // Start the coroutine to display the ending dialogue
        StartCoroutine(DisplayEndingMIV());

        StorylineState.endingMIV = true;
    }

    private IEnumerator DisplayEndingMIV()
    {
        // Activate the dialogue panel
        endingMIVDialogue.SetActive(true);

        // Set the text to the desired message
        dialogueText.text = "Ending 1/4 - Make the \"Invisible\" Visible";

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Deactivate the dialogue panel
        endingMIVDialogue.SetActive(false);
    }

    public void EndingEO()
    {
        // Start the coroutine to display the ending dialogue
        StartCoroutine(DisplayEndingEO());

        StorylineState.endingEO = true;
    }

    private IEnumerator DisplayEndingEO()
    {
        // Activate the dialogue panel
        endingMIVDialogue.SetActive(true);

        // Set the text to the desired message
        dialogueText.text = "Ending 3/4 - Educate the Offender";

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Deactivate the dialogue panel
        endingMIVDialogue.SetActive(false);
    }
}
