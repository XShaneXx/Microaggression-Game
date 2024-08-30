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
        ShowEnding("Ending 1/5 - Disarm the Microaggression\n\n" +
                   "Alex calmly addresses the nurse's statement, pointing out that cultural practices can play a vital role in patient care. The nurse listens, and after a brief discussion, agrees to respect Mr. Khan's diet while ensuring it aligns with medical guidelines.");
    }

    public void educate()
    {
        LogChoice("Educate the Offender");
        ShowEnding("Ending 2/5 - Educate the Offender\n\n" +
                   "Alex takes a moment to explain why dismissing cultural diets can be harmful, not just to Mr. Khan but to the broader goal of providing culturally competent care. The nurse acknowledges her mistake, and the conversation leads to a better understanding of cultural sensitivity.");
    }

    public void seek()
    {
        LogChoice("Seek External Intervention");
        ShowEnding("Ending 3/5 - Seek External Intervention\n\n" +
                   "Recognizing the complexity of the situation, Alex decides to involve the hospital's cultural liaison. The liaison mediates the discussion, helping both Alex and the nurse to reach a solution that respects Mr. Khan's cultural practices while ensuring his medical needs are met.");
    }

    public void make()
    {
        LogChoice("Make the Invisible Visible");
        ShowEnding("Ending 4/5 - Make the Invisible Visible\n\n" +
                   "Alex points out the underlying assumptions in the nurse's comments, bringing to light the importance of respecting diverse cultural practices. This discussion helps the team to see the value in integrating these practices into patient care, fostering a more inclusive environment.");
    }

    public void wrongchoice()
    {
        LogChoice("Wrong Choice");
        ShowEnding("Ending 5/5 - You didn't help out Mr. Khan.\n\n" +
                   "Unfortunately, by not addressing the microaggression, the situation worsens. Mr. Khan feels disrespected, leading to further complications in his care. The team later reflects on the missed opportunity to intervene.");
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
