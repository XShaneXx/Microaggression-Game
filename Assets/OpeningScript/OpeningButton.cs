using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningButton : MonoBehaviour
{
    public GameObject gameMenu;  // Reference to the game menu
    public GameObject HTPMenu;   // Reference to the How To Play menu
    public GameObject storyPanel; // Reference to the story panel

    private bool isMenuOpen = false; // Track if the menu is open or not

    void Update()
    {
        // Toggle menu when ESC is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuOpen)
            {
                Menuclose();
            }
            else
            {
                Menuopen();
            }
        }
    }

    public void Menuopen()
    {
        gameMenu.SetActive(true);  // Show the game menu
        Time.timeScale = 0f;       // Pause the game
        isMenuOpen = true;         // Set the menu open flag to true
    }

    public void Menuclose()
    {
        gameMenu.SetActive(false); // Hide the game menu
        Time.timeScale = 1f;       // Resume the game
        isMenuOpen = false;        // Set the menu open flag to false
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;       // Make sure to resume the game before loading the new scene
        SceneManager.LoadScene("TitleScene"); // Load the Title Scene
    }

    public void HTPOpen()
    {
        HTPMenu.SetActive(true);  // Show the How To Play menu
    }

    public void HTPClose()
    {
        HTPMenu.SetActive(false); // Hide the How To Play menu
    }

    public void storyOpen()
    {
        storyPanel.SetActive(true);  // Show the story panel
    }

    public void storyClose()
    {
        storyPanel.SetActive(false); // Hide the story panel
    }
}
