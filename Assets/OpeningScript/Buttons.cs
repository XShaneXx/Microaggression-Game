using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    
    public GameObject howToPlayPanel;
    public GameObject storyPanel;

    public void gameStartButton()
    {
        SceneManager.LoadScene("Opening");
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
}
