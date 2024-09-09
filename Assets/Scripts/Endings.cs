using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Endings : MonoBehaviour
{
    public GameObject endingMIVDialogue;
    public TextMeshProUGUI dialogueText;
    public bool leavedream;

    private int solutionsFound = 0; 
    private HashSet<string> solutionsCompleted = new HashSet<string>(); 
    private float startTime;
    private float endTime;

    private Dictionary<string, float> solutionTimes = new Dictionary<string, float>(); 

    private string filePath;

    public GameObject solutionsCount;
    public TextMeshProUGUI CountText;
    public TextMeshProUGUI solutionsText; // Reference to the solutions text
    public TextMeshProUGUI objectives; // Reference to the objectives text

    public GameObject menuUI;
    private bool isMenuActive = false;

    private string[] solutionsArray = new string[4]; // Array to track the solution progress

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

        // Initialize the solutions count display and objective text
        UpdateSolutionCountDisplay();
        UpdateObjectivesText();

        // Initialize solutionsText with dashes
        InitializeSolutionsText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }

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
        if (startTime == 0f)
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

            // Update the solution in the array and UI
            UpdateSolution(0, "Make the \"Invisible\" Visible");
            UpdateSolutionCountDisplay();
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

            // Update the solution in the array and UI
            UpdateSolution(1, "Educate the Offender");
            UpdateSolutionCountDisplay();
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

            // Update the solution in the array and UI
            UpdateSolution(2, "Disarm the Microaggression");
            UpdateSolutionCountDisplay();
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

            // Update the solution in the array and UI
            UpdateSolution(3, "Seek External Intervention");
            UpdateSolutionCountDisplay();
            StartCoroutine(DisplayEnding(solutionsFound, "Seek External Intervention"));
            StorylineState.endingSEI = true;
        }
    }

    private void UpdateSolution(int index, string solution)
    {
        solutionsArray[index] = solution;
        UpdateSolutionsText();

        if (solutionsFound == 4)
        {
            UpdateObjectivesText();
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

    private void UpdateSolutionCountDisplay()
    {
        if (CountText != null)
        {
            CountText.text = $"Solutions Found {solutionsFound}/4";
        }
    }

    private void InitializeSolutionsText()
    {
        for (int i = 0; i < solutionsArray.Length; i++)
        {
            solutionsArray[i] = "--------------------------------------------";
        }
        UpdateSolutionsText();
    }

    private void UpdateSolutionsText()
    {
        if (solutionsText != null)
        {
            solutionsText.text = $"- Solution 1: {solutionsArray[0]}\n\n" +
                                 $"- Solution 2: {solutionsArray[1]}\n\n" +
                                 $"- Solution 3: {solutionsArray[2]}\n\n" +
                                 $"- Solution 4: {solutionsArray[3]}";
        }
    }

    private void UpdateObjectivesText()
    {
        if (objectives != null)
        {
            if (solutionsFound == 4)
            {
                objectives.text = "Objective: Find a way to leave the dream";
            }
            else
            {
                objectives.text = "Objective: Find all 4 solutions to help Mrs.Lee";
            }
        }
    }

    // Method for ESC key toggle functionality
    public void ToggleMenu()
    {
        if (isMenuActive)
        {
            closeMenu();
        }
        else
        {
            openMenu();
        }
    }

    public void openMenu()
    {
        isMenuActive = true;
        menuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void closeMenu()
    {
        isMenuActive = false;
        menuUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
