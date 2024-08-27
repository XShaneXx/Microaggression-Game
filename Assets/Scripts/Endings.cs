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

    private int solutionsFound = 0; // Track the number of solutions found
    private HashSet<string> solutionsCompleted = new HashSet<string>(); // Track completed solutions

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
        if (!solutionsCompleted.Contains("MIV"))
        {
            solutionsFound++;
            solutionsCompleted.Add("MIV");

            StartCoroutine(DisplayEnding(solutionsFound, "Make the \"Invisible\" Visible"));
            StorylineState.endingMIV = true;
        }
    }

    public void EndingEO()
    {
        if (!solutionsCompleted.Contains("EO"))
        {
            solutionsFound++;
            solutionsCompleted.Add("EO");

            StartCoroutine(DisplayEnding(solutionsFound, "Educate the Offender"));
            StorylineState.endingEO = true;
        }
    }

    public void EndingDM()
    {
        if (!solutionsCompleted.Contains("DM"))
        {
            solutionsFound++;
            solutionsCompleted.Add("DM");

            StartCoroutine(DisplayEnding(solutionsFound, "Disarm the Microaggression"));
            StorylineState.endingDM = true;
        }
    }

    public void EndingSEI()
    {
        if (!solutionsCompleted.Contains("SEI"))
        {
            solutionsFound++;
            solutionsCompleted.Add("SEI");

            StartCoroutine(DisplayEnding(solutionsFound, "Seek External Intervention"));
            StorylineState.endingSEI = true;
        }
    }

    private IEnumerator DisplayEnding(int solutionNumber, string message)
    {
        endingMIVDialogue.SetActive(true);

        dialogueText.text = $"Solution {solutionNumber}/4 - {message}";

        yield return new WaitForSeconds(5f);

        endingMIVDialogue.SetActive(false);
    }
}
