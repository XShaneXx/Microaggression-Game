using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Endings : MonoBehaviour
{
    public GameObject endingMIVDialogue; // Reference to the UI panel
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshPro component
    public bool leavedream;

    void Start()
    {
        leavedream = false;
    }

    void Update()
    {
        if (!leavedream && StorylineState.endingMIV && StorylineState.endingEO && StorylineState.endingDM && StorylineState.endingSEI)
        {
            leavedream = true;
            StartCoroutine(HandleAllEndingsComplete());
        }
    }

    private IEnumerator HandleAllEndingsComplete()
    {
        yield return new WaitForSeconds(10f);

        endingMIVDialogue.SetActive(true);

        dialogueText.text = "You found all the solutions, let's wake up!";

        yield return new WaitForSeconds(5f);

        endingMIVDialogue.SetActive(false);
    }

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
        dialogueText.text = "Solution 1/4 - Make the \"Invisible\" Visible";

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
        dialogueText.text = "Solution 3/4 - Educate the Offender";

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Deactivate the dialogue panel
        endingMIVDialogue.SetActive(false);
    }

    public void EndingDM()
    {
        // Start the coroutine to display the ending dialogue
        StartCoroutine(DisplayEndingDM());

        StorylineState.endingDM = true;
    }

    private IEnumerator DisplayEndingDM()
    {
        // Activate the dialogue panel
        endingMIVDialogue.SetActive(true);

        // Set the text to the desired message
        dialogueText.text = "Solution 2/4 - Disarm the Microaggression";

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Deactivate the dialogue panel
        endingMIVDialogue.SetActive(false);
    }

    public void EndingSEI()
    {
        // Start the coroutine to display the ending dialogue
        StartCoroutine(DisplayEndingSEI());

        StorylineState.endingSEI = true;
    }

    private IEnumerator DisplayEndingSEI()
    {
        // Activate the dialogue panel
        endingMIVDialogue.SetActive(true);

        // Set the text to the desired message
        dialogueText.text = "Solution 4/4 - Seek External Intervention";

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Deactivate the dialogue panel
        endingMIVDialogue.SetActive(false);
    }
}
