using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public GameObject dialoguePanel; // Reference to the dialogue panel
    private PhoneDialogue phoneDialogue; // Reference to the Dialogue script
    private bool isPlayerInRange; // Flag to check if player is within the collider
    private bool dialogueStarted;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerInRange = false;
        dialogueStarted = false;
        phoneDialogue = dialoguePanel.GetComponent<PhoneDialogue>(); // Get the Dialogue script component
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.J))
        {
            if (!dialogueStarted) // Only set the start line if the dialogue hasn't started yet
            {
                if (StorylineState.hasCheckedNote)
                {
                    phoneDialogue.SetStartLine(1); // Start from the second line (index 1)
                }
                else
                {
                    phoneDialogue.SetStartLine(0); // Start from the first line (index 0)
                }
                dialogueStarted = true; // Mark the dialogue as started
            }
            dialoguePanel.SetActive(true); // Activate the dialogue panel
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
            dialoguePanel.SetActive(false); // Deactivate the dialogue panel when player leaves
            phoneDialogue.ResetDialogue(); // Reset the dialogue to the first line
        }
    }
}
