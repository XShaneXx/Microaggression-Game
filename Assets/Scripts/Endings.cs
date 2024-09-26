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

    public TextMeshProUGUI objectives; // Reference to the objectives text

    public GameObject menuUI;
    private bool isMenuActive = false;

    private string[] solutionsArray = new string[4]; // Array to track the solution progress

    public TextMeshProUGUI solutionsMIVText;
    public GameObject moreHintMIV1;
    public GameObject moreHintMIV2;
    public GameObject moreHintMIV3;
    private bool doneMIV1 = false;
    private bool doneMIV2 = false;
    public TextMeshProUGUI solutionsEOText;
    public GameObject moreHintEO1;
    public GameObject moreHintEO2;
    private bool doneEO1 = false;
    public TextMeshProUGUI solutionsDMText;
    public GameObject moreHintDM1;
    public GameObject moreHintDM2;
    private bool doneDM1 = false;
    public TextMeshProUGUI solutionsSEIText;
    public GameObject moreHintSEI1;
    public GameObject moreHintSEI2;
    private bool doneSEI1 = false;


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
        UpdateObjectivesText();
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

        // Solution MIV
        if(StorylineState.hasCheckedMedicalBottle && !doneMIV1)
        {
            solutionsMIVText.text = "- Solution 1: Find a way to translate the bottle";
            moreHintMIV1.SetActive(false);
            moreHintMIV2.SetActive(true);
            doneMIV1 = true;
        }

        if(StorylineState.hasTranslatedMedicalBottle && !doneMIV2)
        {
            solutionsMIVText.text = "- Solution 1: Find Nurse A to talk about it";
            moreHintMIV2.SetActive(false);
            moreHintMIV3.SetActive(true);
            doneMIV2 = true;
        }

        // Solution EO
        if(StorylineState.hasCheckedDEIPoster && !doneEO1)
        {
            solutionsEOText.text = "- Solution 2: Find Nurse A to talk about it";
            moreHintEO1.SetActive(false);
            moreHintEO2.SetActive(true);
            doneEO1 = true;
        }

        // Solution DM
        if(StorylineState.hasCheckedManual && !doneDM1)
        {
            solutionsDMText.text = "- Solution 3: Find Doctor A to talk about it";
            moreHintDM1.SetActive(false);
            moreHintDM2.SetActive(true);
            doneDM1 = true;
        }

        // Solution SEI
        if(StorylineState.hasCheckedNote && !doneSEI1)
        {
            solutionsSEIText.text = "- Solution 4: Find a way to call the number";
            moreHintSEI1.SetActive(false);
            moreHintSEI2.SetActive(true);
            doneSEI1 = true;
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
            StartCoroutine(DisplayEnding(solutionsFound, "Make the \"Invisible\" Visible"));
            StorylineState.endingMIV = true;

            // Update the goals
            solutionsMIVText.text = "- Solution Done: Make the \"Invisible\" Visible";
            moreHintMIV1.SetActive(false);
            moreHintMIV2.SetActive(false);
            moreHintMIV3.SetActive(false);
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
            StartCoroutine(DisplayEnding(solutionsFound, "Educate the Offender"));
            StorylineState.endingEO = true;

            // Update the goals
            solutionsEOText.text = "- Solution Done: Educate the Offender";
            moreHintEO1.SetActive(false);
            moreHintEO2.SetActive(false);
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
            StartCoroutine(DisplayEnding(solutionsFound, "Disarm the Microaggression"));
            StorylineState.endingDM = true;

            // Update the goals
            solutionsDMText.text = "- Solution Done: Disarm the Microaggression";
            moreHintDM1.SetActive(false);
            moreHintDM2.SetActive(false);
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
            StartCoroutine(DisplayEnding(solutionsFound, "Seek External Intervention"));
            StorylineState.endingSEI = true;

            // Update the goals
            solutionsSEIText.text = "- Solution Done: Seek External Intervention";
            moreHintSEI1.SetActive(false);
            moreHintSEI2.SetActive(false);
        }
    }

    private void UpdateSolution(int index, string solution)
    {
        solutionsArray[index] = solution;

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
                objectives.text = "Objective: Find ways to help the mother";
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

    // Solution MIV Hint Button
    public void MoreHintMIV1()
    {
        solutionsMIVText.text = "- Solution 1: Check the medicine bottle in ward";
        moreHintMIV1.SetActive(false);
    }

    public void MoreHintMIV2()
    {
        solutionsMIVText.text = "- Solution 1: Use the computer in nurse station";
        moreHintMIV2.SetActive(false);
    }

    public void MoreHintMIV3()
    {
        solutionsMIVText.text = "- Solution 1: Find Nurse A in examination room";
        moreHintMIV3.SetActive(false);
    }

    // Solution EO Hint Button
    public void MoreHintEO1()
    {
        solutionsEOText.text = "- Solution 2: Check the poster in waiting area";
        moreHintEO1.SetActive(false);
    }

    public void MoreHintEO2()
    {
        solutionsEOText.text = "- Solution 2: Find Nurse A in examination room";
        moreHintEO2.SetActive(false);
    }

    // Solution DM Hint Button
    public void MoreHintDM1()
    {
        solutionsDMText.text = "- Solution 3: Check the policy manual in nurse station";
        moreHintDM1.SetActive(false);
    }

    public void MoreHintDM2()
    {
        solutionsDMText.text = "- Solution 3: Find Doctor A in his office";
        moreHintDM2.SetActive(false);
    }


    // Solution SEI Hint Button
    public void MoreHintSEI1()
    {
        solutionsSEIText.text = "- Solution 4: Check the note pile in nurse station";
        moreHintSEI1.SetActive(false);
    }

    public void MoreHintSEI2()
    {
        solutionsSEIText.text = "- Solution 4: Find phone in the waiting area";
        moreHintSEI2.SetActive(false);
    }
}
