using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class EndingButtons : MonoBehaviour
{
    public GameObject endingPanel; // Reference to the Ending panel
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshPro component for the ending text
    public GameObject testChoicePanel; // Reference to the choice panel that should be deactivated

    private string folderPath = "G:/Master Thesis/TestDataCollect";
    private string filePath;

    private List<string> playerChoices = new List<string>(); // Track player choices
    private int tryAgainCount = 0; // Track how many times the player tries again

    void Start()
    {
        // Generate a unique file name
        filePath = GenerateUniqueFilePath();

        // Ensure the folder exists
        Directory.CreateDirectory(folderPath);

        // Initialize the file with a header
        File.WriteAllText(filePath, "Player Choices and Attempts:\n\n");
    }

    public void disarm()
    {
        LogChoice("Disarm the Microaggression");
        ShowEnding("Ending 1/5 - Disarm the Microaggression");
    }

    public void educate()
    {
        LogChoice("Educate the Offender");
        ShowEnding("Ending 2/5 - Educate the Offender");
    }

    public void seek()
    {
        LogChoice("Seek External Intervention");
        ShowEnding("Ending 3/5 - Seek External Intervention");
    }

    public void make()
    {
        LogChoice("Make the Invisible Visible");
        ShowEnding("Ending 4/5 - Make the Invisible Visible");
    }

    public void wrongchoice()
    {
        LogChoice("Wrong Choice");
        ShowEnding("Ending 5/5 - You didn't help out Mr.Khan. Things happen again...");
    }

    private void ShowEnding(string endingText)
    {
        endingPanel.SetActive(true);
        dialogueText.text = endingText.Replace(" - ", "\n");
        testChoicePanel.SetActive(false);
    }

    private void LogChoice(string choice)
    {
        playerChoices.Add(choice);
        SaveData();
    }

    private void SaveData()
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine($"Choice made: {playerChoices[playerChoices.Count - 1]}");
        }
    }

    public void TryAgain()
    {
        tryAgainCount++;
        LogChoice($"Try Again Attempt {tryAgainCount}");
    }

    private string GenerateUniqueFilePath()
    {
        string baseFileName = "player_choices";
        string timeStamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string uniqueFileName = $"{baseFileName}_{timeStamp}.txt";
        return Path.Combine(folderPath, uniqueFileName);
    }
}