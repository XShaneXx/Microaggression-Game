using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Endings : MonoBehaviour
{
    public GameObject endingMIVDialogue; // Reference to the UI panel
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshPro component
    public bool leavedream;

    private int solutionsFound = 0; // Track the number of solutions found
    private HashSet<string> solutionsCompleted = new HashSet<string>(); // Track completed solutions
    private float startTime; // Track when the player first presses J
    private float endTime; // Track when all solutions are found

    private Dictionary<string, float> solutionTimes = new Dictionary<string, float>(); // Store times for each solution

    private string filePath;

    void Start()
    {
        leavedream = false;
        startTime = 0f;
        endTime = 0f;

        // Generate a unique file path
        filePath = GenerateUniqueFilePath();

        // Ensure the directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        // Initialize the file with a header
        File.WriteAllText(filePath, "Solution Times Data:\n\n");
    }

    void Update()
    {
        if (!leavedream && StorylineState.endingMIV && StorylineState.endingEO && StorylineState.endingDM && StorylineState.endingSEI)
        {
            leavedream = true;
            endTime = Time.time;
            StartCoroutine(HandleAllEndingsComplete());
            SaveData();
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

    public void StartTrackingTime()
    {
        if (startTime == 0f) // Start tracking time only on the first press of J
        {
            startTime = Time.time;
        }
    }

    public void EndingMIV()
    {
        if (!solutionsCompleted.Contains("MIV"))
        {
            solutionsFound++;
            solutionsCompleted.Add("MIV");

            float timeFound = Time.time - startTime;
            solutionTimes["MIV"] = timeFound;

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

            float timeFound = Time.time - startTime;
            solutionTimes["EO"] = timeFound;

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

            float timeFound = Time.time - startTime;
            solutionTimes["DM"] = timeFound;

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

            float timeFound = Time.time - startTime;
            solutionTimes["SEI"] = timeFound;

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

    private void SaveData()
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine($"Total time taken: {endTime - startTime} seconds");

            foreach (var solution in solutionTimes)
            {
                writer.WriteLine($"{solution.Key} found at: {solution.Value} seconds");
            }
        }
    }

    private string GenerateUniqueFilePath()
    {
        string folderPath = Path.Combine(Application.dataPath, "TestDataCollect");
        string baseFileName = "solution_times";
        string timeStamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string uniqueFileName = $"{baseFileName}_{timeStamp}.txt";
        return Path.Combine(folderPath, uniqueFileName);
    }
}