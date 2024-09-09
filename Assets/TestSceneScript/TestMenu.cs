using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMenu : MonoBehaviour
{
    public GameObject menuUI;
    private bool isMenuActive = false;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

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
