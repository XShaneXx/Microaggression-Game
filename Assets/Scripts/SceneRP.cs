using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManagement namespace

public class SceneRP : MonoBehaviour
{
    private bool isPlayerInRange; // Flag to check if player is within the collider

    // Start is called before the first frame update
    void Start()
    {
        isPlayerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.J))
        {
            ResetScene(); // Call the scene reset function
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Set the flag when player enters the collider
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Reset the flag when player exits the collider
        }
    }

    private void ResetScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
