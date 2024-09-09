using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    
    public GameObject howToPlayPanel;

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
}
