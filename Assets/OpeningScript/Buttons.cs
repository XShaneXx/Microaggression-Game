using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public GameObject storyPanel;
    public GameObject blackScreen;

    public float fadeDuration = 2f; // Duration for the fade effect

    public void gameStartButton()
    {
        // Start the fade-in effect coroutine
        StartCoroutine(FadeInAndStartGame());
    }

    public void howtoplay()
    {
        howToPlayPanel.SetActive(true);
    }

    public void close()
    {
        howToPlayPanel.SetActive(false);
    }

    public void storyOpen()
    {
        storyPanel.SetActive(true);
    }

    public void storyClose()
    {
        storyPanel.SetActive(false);
    }

    // Coroutine to fade in the black screen and then start the game
    IEnumerator FadeInAndStartGame()
    {
        blackScreen.SetActive(true);
        Image blackScreenImage = blackScreen.GetComponent<Image>();
        Color blackColor = blackScreenImage.color;
        blackColor.a = 0f;
        blackScreenImage.color = blackColor;

        float elapsedTime = 0f;

        // Fade in effect
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            blackColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            blackScreenImage.color = blackColor;
            yield return null;
        }

        // Ensure black screen is fully opaque
        blackColor.a = 1f;
        blackScreenImage.color = blackColor;

        // Load the next scene after fade-in is complete
        SceneManager.LoadScene("Opening");
    }
}
